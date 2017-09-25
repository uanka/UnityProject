using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	public static LevelController current = null;

	int coins = 0;
	int fruits = 0;

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
		rabbit.isDead = true;
		rabbit.transform.position = this.startPosition;
	}

	public void addCoins (int coin) {
		this.coins += coin;
	}

	public void addFruits (int fruit) {
		this.fruits += fruit;
	}

	public void addCrystal (HeroRabbit rabbit) {
		this.startPosition = rabbit.transform.position;
	}
}
