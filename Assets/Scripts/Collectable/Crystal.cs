using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Collectable {

	public CrystalColour colour;

	protected override void OnRabbitHit (HeroRabbit rabbit) {
		LevelController.current.addCrystal (rabbit, colour);
		this.CollectedHide ();
	}


}
