using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPopUp : PopUp {

	public MyButton nextButton = null;
	public MyButton replayButton = null;

	public UILabel coinsLabel = null;
	public UILabel fruitsLabel = null;

	public CrystalsController crystalController = null;
	UI2DSprite[] crystalColours = null;

	public AudioClip sound = null;
	AudioSource soundSource = null;
	// Use this for initialization
	void Start () {
		soundSource = gameObject.AddComponent<AudioSource> ();
		soundSource.clip = sound;
		if (SoundManager.manager.isSoundOn ()) 
			//Debug.Log( "Enabled: " + GetComponent<AudioSource> ().enabled);
			soundSource.Play ();
		
		closeButton.signalOnClick.AddListener (menu);
		background.signalOnClick.AddListener (menu);
		nextButton.signalOnClick.AddListener (menu);
		replayButton.signalOnClick.AddListener (replay);

		coinsLabel.text = "+" + LevelController.current.getCoins ();
		fruitsLabel.text = LevelController.current.getFruits () + "/" + LevelController.current.getMaxFruits ();
		crystalColours = LevelController.current.crystalController.crystals;

		StartCoroutine(showCrystals ());
	}

	IEnumerator showCrystals () {
		yield return new WaitForSeconds (1);
		crystalController.crystals[1].sprite2D = crystalColours[1].sprite2D;
		crystalController.crystals[2].sprite2D = crystalColours[2].sprite2D;
		crystalController.crystals[3].sprite2D = crystalColours[3].sprite2D;
	}


	void menu () {
		SceneManager.LoadScene ("ChooseLevel");
	}

	void replay () {
		SceneManager.LoadScene (LevelController.current.getSceneName());
	}
}
