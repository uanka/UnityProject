﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabbit : MonoBehaviour {

	public float speed = 1;
	bool isGrounded = false;
	public bool isDead = false;
	public bool isBig = false;

	public float bigTime = 0;
	float jumpTime = 0f;
	bool jumpActive = false;

	public float maxBigTime = 8f;
	public float maxJumpTime = 2f;
	public float jumpSpeed = 2f;

	Rigidbody2D myBody = null;
	SpriteRenderer myBodyRenderer = null;
	Animator myAnimator = null;

	public Transform heroParent = null;
	public static HeroRabbit current = null;

	public AudioClip runSound = null;
	public AudioClip dieSound = null;
	public AudioClip groundingSound = null;
	protected AudioSource runSource = null;
	protected AudioSource dieSource = null;
	protected AudioSource groundingSource = null;

	void Awake () {
		current = this;
	}

	// Use this for initialization
	void Start () {
		myBody = this.GetComponent<Rigidbody2D> ();
		myBodyRenderer = this.GetComponent<SpriteRenderer> ();
		myAnimator = this.GetComponent<Animator> ();
		if (LevelController.current)
			LevelController.current.setStartPosition (this.transform.position);
		this.heroParent = this.transform.parent;

		runSource = gameObject.AddComponent<AudioSource> ();
		runSource.clip = runSound;
		runSource.loop = true;
		dieSource = gameObject.AddComponent<AudioSource> ();
		dieSource.clip = dieSound;
		groundingSource = gameObject.AddComponent<AudioSource> ();
		groundingSource.clip = groundingSound;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// if ate mushroom
		if (isDead) return;
		if (isBig) {
			bigTime -= Time.deltaTime;
			if (bigTime <= 0) {
				isBig = false;
				this.transform.localScale -= new Vector3 (1f, 1f, 0);
			}
		}
		//Ground Check
		Vector3 from = this.transform.position + Vector3.up * 0.3f;
		Vector3 to = this.transform.position + Vector3.down * 0.01f;
		int layer_id = 1 << LayerMask.NameToLayer ("Ground"); // | побітове

		RaycastHit2D hit = Physics2D.Linecast (from, to, layer_id);
		//Jump check
		if (hit) {
			isGrounded = true;
			myAnimator.SetBool("jump", false);
			if (SoundManager.manager.isSoundOn ()) {
				groundingSource.Play ();
				runSource.Play ();
			}

			//Перевіряємо чи ми опинились на платформі
			if(hit.transform != null
				&& hit.transform.GetComponent<MovingPlatform>() != null){
				//Приліпаємо до платформи
				SetNewParent(this.transform, hit.transform);
			}
		} else {
			isGrounded = false;
			runSource.Stop ();

			myAnimator.SetBool ("jump", true);
			//Ми в повітрі відліпаємо під платформи
			SetNewParent(this.transform, this.heroParent);
		}
		Debug.DrawLine (from, to, Color.red);

		if(Input.GetButton("Jump") && isGrounded){
 			//if GetKeyDown(KeyCode.Space) - не даємо вибір клавіш
			this.jumpActive = true;
		}

		if (this.jumpActive) {
			if (Input.GetButton ("Jump")) {
				this.jumpTime += Time.deltaTime;

				if (this.jumpTime < this.maxJumpTime) {
					Vector2 velocity = myBody.velocity;
					velocity.y = jumpSpeed * (1.0f - jumpTime / maxJumpTime);
					myBody.velocity = velocity;
				}
			} else {
				this.jumpActive = false;
				this.jumpTime = 0;
				//groundingSource.Play ();
			}
		}
			
		//[-1, 1]
		float value = Input.GetAxis ("Horizontal");

		if (Mathf.Abs (value) > 0) {
			myAnimator.SetBool ("run", true);
			if ((isGrounded && !jumpActive) && SoundManager.manager.isSoundOn())
				runSource.Play ();

			Vector2 vel = myBody.velocity;
			vel.x = value * speed;
			myBody.velocity = vel;
		} else {
			myAnimator.SetBool ("run", false);
			runSource.Stop ();

		}
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		if (value < 0) {
			sr.flipX = true;
		} else if(value > 0) {
			sr.flipX = false;
		}
	}

	public void eatMushroom (){
		this.isBig = true;
		this.transform.localScale += new Vector3 (1f, 1f, 0);
		bigTime = maxBigTime;
	}

	public void die () {
		if (SoundManager.manager.isSoundOn())
		dieSource.Play ();
		myAnimator.SetBool ("die", true);
		this.GetComponent<BoxCollider2D> ().enabled = false;
		this.myBody.isKinematic = true;
		isDead = true;
	}

	public void restore () {
		myAnimator.SetBool ("die", false);
		this.GetComponent<BoxCollider2D> ().enabled = true;
		this.myBody.isKinematic = false;
		isDead = false;
	}
	static void SetNewParent(Transform obj, Transform new_parent) {
		if(obj.transform.parent != new_parent) {
			//Засікаємо позицію у Глобальних координатах
			Vector3 pos = obj.transform.position;
			//Встановлюємо нового батька
			obj.transform.parent = new_parent;
			//Після зміни батька координати кролика зміняться
			//Оскільки вони тепер відносно іншого об’єкта
			//повертаємо кролика в ті самі глобальні координати
			obj.transform.position = pos;
		}
	}

}
