using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabbit : MonoBehaviour {

	public float speed = 1;
	bool isGrounded = false;

	float jumpTime = 0f;
	bool jumpActive = false;

	public float maxJumpTime = 2f;
	public float jumpSpeed = 2f;

	Rigidbody2D myBody = null;
	SpriteRenderer myBodyRenderer = null;
	Animator myAnimator = null;
	// Use this for initialization
	void Start () {
		myBody = this.GetComponent<Rigidbody2D> ();
		myBodyRenderer = this.GetComponent<SpriteRenderer> ();
		myAnimator = this.GetComponent<Animator> ();

		LevelController.current.setStartPosition (this.transform.position);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Ground Check
		Vector3 from = this.transform.position + Vector3.up * 0.3f;
		Vector3 to = this.transform.position + Vector3.down * 0.01f;
		int layer_id = 1 << LayerMask.NameToLayer ("Ground"); // | побітове

		RaycastHit2D hit = Physics2D.Linecast (from, to, layer_id);
		//Jump check
		if (hit) {
			isGrounded = true;
			myAnimator.SetBool("jump", false);
		} else {
			isGrounded = false;
			myAnimator.SetBool ("jump", true);
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
			}
		}
			
		//[-1, 1]
		float value = Input.GetAxis ("Horizontal");

		if (Mathf.Abs (value) > 0) {
			myAnimator.SetBool ("run", true);
			Vector2 vel = myBody.velocity;
			vel.x = value * speed;
			myBody.velocity = vel;
		} else {
			myAnimator.SetBool ("run", false);
		}
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		if (value < 0) {
			sr.flipX = true;
		} else if(value > 0) {
			sr.flipX = false;
		}
	}

}
