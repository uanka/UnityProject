using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Collectable {

	public int number = 0;
	public bool gathered = false;
	protected override void OnRabbitHit (HeroRabbit rabbit) {
		if (!gathered) {
			LevelController.current.addFruits (number);
			this.CollectedHide ();
		}
	}
}
