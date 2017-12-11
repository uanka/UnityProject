using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class LevelStat {
	public bool hasCrystals = false;
	public bool hasAllFruits = false;
	public bool levelPassed = false;
	public List<int> collectedFruits = new List<int>();
}

public class LevelController : MonoBehaviour {

	public static LevelController current = null;
	public Level level = Level.Level1;

	public UILabel coinsLabel;
	public UILabel fruitsLabel;
	public MyButton pauseButton;

	LifesController lifesController;
	public CrystalsController crystalController;

	public GameObject settingsPrefab = null; 
	public GameObject winPrefab = null; 
	public GameObject losePrefab = null; 

	int coins = 0;
	int fruits = 0;
	int maxfruits = 10;
	int lifes = 3;

	LevelStat stats;

	// Use this for initialization
	void Awake () {
		current = this;
		lifesController = UIRoot.FindObjectOfType<LifesController> ();
		//crystalController = UIRoot.FindObjectOfType<CrystalsController> ();

		pauseButton.signalOnClick.AddListener (this.settingsMenu);

		string str = PlayerPrefs.GetString (getSceneName() + "stats", null);
		this.stats = JsonUtility.FromJson<LevelStat> (str);
		if (this.stats == null) {
			this.stats = new LevelStat ();
		}
		fruits = stats.collectedFruits.Count;
		maxfruits = FindObjectsOfType<Fruit> ().Length;


	}

	Vector3 startPosition;

	public void setStartPosition (Vector3 pos) {
		this.startPosition = pos;
	}
	public void onRabbitDeath (HeroRabbit rabbit) {
		//При смерті кролика повертаємо на початкову позицію
		rabbit.die ();
		//change amount iof lifes on screen
		lifesController.die (lifes);
		lifes--;


		if (lifes != 0)
			StartCoroutine (returnRabbit (rabbit));
		else
			lose();
			
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
		stats.collectedFruits.Add(fruit);
		this.fruits += 1;
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

	void settingsMenu () {
		GameObject parent = UICamera.first.transform.gameObject;
		settingsPrefab.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		GameObject obj = NGUITools.AddChild (parent, settingsPrefab);
		SettingsPopUp popup = obj.GetComponent<SettingsPopUp> ();
		//give params
	}

	public void win () {
		stats.hasAllFruits = stats.collectedFruits.Count == maxfruits;
		stats.hasCrystals = crystalController.allCrystalsGathered ();
		stats.levelPassed = true;

		string str = JsonUtility.ToJson(this.stats);
		PlayerPrefs.SetString (getSceneName() + "stats", str);

		int lastCoins = PlayerPrefs.GetInt("coins");
		PlayerPrefs.SetInt ("coins", lastCoins + this.coins);
		PlayerPrefs.Save ();

		StartCoroutine(winMenu ());
	}

	IEnumerator winMenu () {
		yield return new WaitForSeconds (2);
		GameObject parent = UICamera.first.transform.gameObject;
		GameObject obj = NGUITools.AddChild (parent, winPrefab);
		//WinPopUp popup = obj.GetComponent<WinPopUp> ();
	}

	public string getSceneName () {
		if (level == Level.Level2)
			return "Level2";
		return "Level1";
	}

	public void lose () {
		StartCoroutine(loseMenu ());
	}

	IEnumerator loseMenu () {
		yield return new WaitForSeconds (2);

		GameObject parent = UICamera.first.transform.gameObject;
		losePrefab.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
		GameObject obj = NGUITools.AddChild (parent, losePrefab);
		//LosePopUp popup = obj.GetComponent<LosePopUp> ();
	}
}
