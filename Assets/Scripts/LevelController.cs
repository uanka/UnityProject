using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

	public static LevelController current = null;

	public UILabel coinsLabel;
	public UILabel fruitsLabel;


	LifesController lifesController;
	CrystalsController crystalController;

	int coins = 0;
	int fruits = 0;
	int maxfruits = 11;
	int lifes = 3;

	// Use this for initialization
	void Awake () {
		current = this;
		coinsLabel = UIRoot.FindObjectsOfType<UILabel> ()[1];
		fruitsLabel = UIRoot.FindObjectOfType<UILabel> ();
		lifesController = UIRoot.FindObjectOfType<LifesController> ();
		crystalController = UIRoot.FindObjectOfType<CrystalsController> ();
	}

	Vector3 startPosition;

	public void setStartPosition (Vector3 pos) {
		this.startPosition = pos;
	}
	public void onRabbitDeath (HeroRabbit rabbit) {
		//При смерті кролика повертаємо на початкову позицію
		rabbit.die ();
		lifes--;
		lifesController.die ();
		//chane amount iof lifes on screen

		if (lifes != 0)
			StartCoroutine (returnRabbit (rabbit));
		else
			SceneManager.LoadScene ("ChooseLevel");
			
	}

	IEnumerator returnRabbit (HeroRabbit rabbit) {
		yield return new WaitForSeconds (1);

		rabbit.transform.position = this.startPosition;
		rabbit.restore ();
	}
		
	public void addCoins (int coin) {
		this.coins += coin;
		coinsLabel.text = coins.ToString ("D4");
	}

	public void addFruits (int fruit) {
		this.fruits += fruit;
		fruitsLabel.text = fruits + "/" + maxfruits;
	}

	public void addCrystal (HeroRabbit rabbit, CrystalColour colour) {
		this.startPosition = rabbit.transform.position;
		crystalController.addColour (colour);
	}

	public int getCoins () {
		return coins;
	}

	public int getFruits () {
		return fruits;
	}

	public int getMaxFruits () {
		return maxfruits;
	}

	public int getLifes () {
		return lifes;
	}
}
