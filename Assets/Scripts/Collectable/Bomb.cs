using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Collectable {

	protected override void OnRabbitHit (HeroRabbit rabbit) {

		if (rabbit.isBig) {
			rabbit.bigTime = 0;
		} else {
			rabbit.die ();
		}

		this.CollectedHide ();
	}
}
