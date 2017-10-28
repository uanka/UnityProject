﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Collectable {

	protected override void OnRabbitHit (HeroRabbit rabbit) {

		if (rabbit.isBig) {
			rabbit.bigTime = 0;
		} else {
			LevelController.current.onRabbitDeath(rabbit);
		}

		this.CollectedHide ();
	}
}
