using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcBrown : Orc {

	public GameObject weapon;
	float lastCarrot = 0;

	public float minDistanceToAttack = 5;
	public float minIntervalToAttack = 1f;

	bool isTimeToAttack() {
		if (this.deadOrc())
			return false;
		Vector3 rabbit_pos = HeroRabbit.current.transform.position;
		Vector3 orc_pos = this.transform.position;

		if (HeroRabbit.current.isDead)
			return false;
		if (Mathf.Abs (orc_pos.x - rabbit_pos.x) < this.minDistanceToAttack) {
			if (Time.time - lastCarrot > this.minIntervalToAttack) {
				lastCarrot = Time.time;
				return true;
			}
		}
		return false;
	}

	protected override void performAttack() {
		if (isTimeToAttack()) {
			StartCoroutine (attackLater ());
		}
	}

	IEnumerator attackLater () {
		yield return new WaitForSeconds (0.1f);

		Vector3 rabbit_pos = HeroRabbit.current.transform.position;
		Vector3 orc_pos = this.transform.position;
		if (SoundManager.manager.isSoundOn())
			soundSource.Play ();
		this.orcAnimator.SetTrigger ("attack");
		GameObject carrot = GameObject.Instantiate (weapon);
		carrot.transform.position = this.transform.position + Vector3.up * 0.5f;
		Carrot obj = carrot.GetComponent<Carrot> ();
		if (orc_pos.x < rabbit_pos.x)
			obj.launch (1);
		else if (orc_pos.x > rabbit_pos.x)
			obj.launch (-1);
	}

	protected override bool shouldPatrolAB () {
		float orc_x = this.transform.position.x;
		float rabbit_x = HeroRabbit.current.transform.position.x;

		if (Mathf.Abs (orc_x - rabbit_x) < 3f)
			return false;
		return true;
	}

	protected override float getAttackDirection () {
		Vector3 orc_pos = this.transform.position;
		Vector3 rabbit_pos = HeroRabbit.current.transform.position;

		if (Mathf.Abs (orc_pos.x - rabbit_pos.x) < 0.5f)
			return 0;

		if (orc_pos.x < rabbit_pos.x)
			return 1;
		else if (orc_pos.x > rabbit_pos.x)
			return -1;
		return 0;
	}
}
