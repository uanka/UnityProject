using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	public MyButton playButton = null;
	public MyButton settingsButton = null;

	public GameObject settingsPrefab = null; 

	public AudioClip music = null;
	AudioSource musicSource = null;

	void Awake () {
		playButton.signalOnClick.AddListener (this.onPlay);
		settingsButton.signalOnClick.AddListener (this.settingsMenu);
		PlayerPrefs.SetInt ("music", 1);
		PlayerPrefs.SetInt ("sound", 1);

		musicSource = gameObject.AddComponent<AudioSource> ();
		musicSource.clip = music;
		musicSource.loop = true;
		if (SoundManager.manager.isMusicOn ())
			musicSource.Play ();
	
	}

	void Update () {
		if (SoundManager.manager.isMusicOn ())
			musicSource.Play ();
		else
			musicSource.Stop ();
	}

	public void onPlay () {
		SceneManager.LoadScene ("ChooseLevel");
	}

	void settingsMenu () {
		GameObject parent = UICamera.first.transform.gameObject;
		GameObject obj = NGUITools.AddChild (parent, settingsPrefab);
		// SettingsPopUp popup = obj.GetComponent<SettingsPopUp> ();
	}

}
