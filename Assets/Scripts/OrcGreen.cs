using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcGreen : Orc {

	protected override bool shouldPatrolAB () {
		float rabbit_x = HeroRabbit.current.transform.position.x;

		if (rabbit_x > pointA.x && rabbit_x < pointB.x)
			return false;
		return true;
	}

	protected override float getAttackDirection () {
		Vector3 orc_pos = this.transform.position;
		Vector3 rabbit_pos = HeroRabbit.current.transform.position;

		if (Mathf.Abs (orc_pos.x - rabbit_pos.x) < 0.1f)
			return 0;

		if (orc_pos.x < rabbit_pos.x)
			return 1;
		else if (orc_pos.x > rabbit_pos.x)
			return -1;
		return 0;
	}

	protected override void performAttack () {
		Vector3 orc_pos = this.transform.position;
		Vector3 rabbit_pos = HeroRabbit.current.transform.position;

		if (Mathf.Abs (orc_pos.x - rabbit_pos.x) < 1f)
			if (HeroRabbit.current.isDead) 
				this.orcAnimator.SetTrigger ("attack");
	}
}
