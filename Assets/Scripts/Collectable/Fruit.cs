﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Collectable {

	protected override void OnRabbitHit (HeroRabbit rabbit) {
		LevelController.current.addFruits (1);
		this.CollectedHide ();
	}
}
