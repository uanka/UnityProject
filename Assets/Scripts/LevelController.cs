using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	public static LevelController current = null;
	// Use this for initialization
	void Awake () {
		current = this;
	}

	Vector3 startPosition;

	public void setStartPosition (Vector3 pos) {
		this.startPosition = pos;
	}
	public void onRabbitDeath (HeroRabbit rabbit) {
		//При смерті кролика повертаємо на початкову позицію
		rabbit.transform.position = this.startPosition;
	}
}
