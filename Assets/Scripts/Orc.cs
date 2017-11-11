using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonoBehaviour {

	enum Mode {
		GoToA,
		GoToB,
		Attack
	}

	public static float speed = 1;

	public Vector3 pointA;
	public Vector3 pointB;
	public Vector3 diff = new Vector3 (5,0,0);
	Mode mode = Mode.GoToA;

	protected Rigidbody2D orcBody = null;
	protected SpriteRenderer orcBodyRenderer = null;
	protected Animator orcAnimator = null;

	bool isDead = false;
	// Use this for initialization
	void Start () {
		orcBody = this.GetComponent<Rigidbody2D> ();
		orcBodyRenderer = this.GetComponent<SpriteRenderer> ();
		orcAnimator = this.GetComponent<Animator> ();

		pointA = this.transform.position;
		pointB = pointA + diff;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		float value = this.getDirection ();

		Vector2 velocity = orcBody.velocity;
        velocity.x = speed * value;
		orcBody.velocity = velocity;

        if (value > 0)
			orcBodyRenderer.flipX = true;
        else if (value < 0)
			orcBodyRenderer.flipX = false;

		this.performAttack ();
	}

	bool isArrived (Vector3 current, Vector3 target) {
		current.z = 0;
		target.z = 0;

		current.y = 0;
		target.y = 0;

		return Vector3.Distance (current, target) < 0.2f;
	}

	protected virtual bool shouldPatrolAB () {
		return true;
	}

	protected virtual void performAttack () {
	}

	protected virtual float getAttackDirection () {
		return 1;
	}
	float getDirection () {
		if (isDead)
			return 0;
		Vector3 orc_pos = this.transform.position;

		if (shouldPatrolAB ()) {
			if (mode == Mode.GoToA && isArrived (orc_pos, pointA))
				mode = Mode.GoToB;
			else if (mode == Mode.GoToB && isArrived (orc_pos, pointB))
				mode = Mode.GoToA;
			
			Vector3 target = new Vector3 (0, 0, 0);
			if (mode == Mode.GoToA)
				target = pointA;
			else if (mode == Mode.GoToB)
				target = pointB;

			if (target.x < orc_pos.x)
				return -1;
			else if (target.x > orc_pos.x)
				return 1;
		} else {
			return getAttackDirection ();
		}
		return 0;
	}

	public bool deadOrc() {
		return isDead;
	}

	void orcDie () {
		this.orcAnimator.SetBool ("die", true);
		this.isDead = true;
		this.GetComponent<BoxCollider2D> ().enabled = false;
		this.orcBody.isKinematic = true;
		StartCoroutine (hideMeLater ());
	}

	IEnumerator hideMeLater () {
		yield return new WaitForSeconds (3);
		Destroy (this.gameObject);
	}
		
	void OnCollideWithRabbit (HeroRabbit rabbit) {
		float rabbit_y = rabbit.transform.position.y;
		float orc_y = this.transform.position.y;

		if (orc_y < rabbit_y && Mathf.Abs(rabbit_y - orc_y) > 0.5f)
			this.orcDie ();
		else
			LevelController.current.onRabbitDeath (rabbit);
	}

	public void OnTriggerEnter2D (Collider2D collider) {
		if (isDead)
			return;

		HeroRabbit rabbit = collider.GetComponent<HeroRabbit> ();

		if (rabbit != null) 
			OnCollideWithRabbit (rabbit);
	}
}
