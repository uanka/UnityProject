using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Level {
	Level1,
	Level2,
	ChooseLevel
}
public class Door : MonoBehaviour {

	public Level level;
	string scene = null;

	public SpriteRenderer doorCrystal = null;
	public SpriteRenderer doorFruit = null;
	public SpriteRenderer doorChecked = null;

	public Sprite crystal = null;
	public Sprite fruit = null;

	// Use this for initialization
	void Start () {
		if (level == Level.ChooseLevel)
			scene = "ChooseLevel";
		else {
			LevelStat stats1 = JsonUtility.FromJson<LevelStat>(PlayerPrefs.GetString ("Level1stats"));
			LevelStat stats2 = JsonUtility.FromJson<LevelStat>(PlayerPrefs.GetString ("Level2stats"));
			Debug.Log (PlayerPrefs.GetString ("Level1stats"));
			if (level == Level.Level1) {
				scene = "Level1";
				doorStats (stats1);
			} else if (level == Level.Level2 && stats1.levelPassed) {
				scene = "Level2";
				doorStats (stats2);
			}
		}
	}

	void OnTriggerEnter2D (Collider2D collider) {
		//		if (!this.hideAnimation) {
		HeroRabbit rabbit = collider.GetComponent<HeroRabbit> ();
		if (rabbit != null) {
			if (level == Level.ChooseLevel)
				LevelController.current.win ();
			else
				SceneManager.LoadScene (scene);
		}
		//		}
	}

	void doorStats (LevelStat stats) {
		if (stats == null)
			return;
		if (stats.hasCrystals) {
			doorCrystal.sprite = crystal;
			doorCrystal.transform.localScale = new Vector3 (0.65f, 0.65f, 0.65f);
		}
		if (stats.hasAllFruits) {
			doorFruit.sprite = fruit;
			doorFruit.transform.localScale = new Vector3 (0.7f, 0.7f, 0.7f);
		}
		if (stats.levelPassed)
			doorChecked.color = new Color (1f, 1f, 1f, 1f);
		
	}
}
