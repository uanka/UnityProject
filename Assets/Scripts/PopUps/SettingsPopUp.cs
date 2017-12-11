using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPopUp : PopUp {
	UI2DSprite[] buttons = null;

	public MyButton soundButton = null;
	public MyButton musicButton = null;

	public Sprite soundOn, soundOff;
	public Sprite musicOn, musicOff;

	bool music = true;
	bool sound = true;
	// Use this for initialization
	void Awake () {
		buttons = this.GetComponentsInChildren<UI2DSprite> ();
		soundButton.signalOnClick.AddListener (this.soundClick);
		musicButton.signalOnClick.AddListener (this.musicClick);

		sound = (SoundManager.manager.isSoundOn ());
		music = (SoundManager.manager.isMusicOn ());

		buttons [3].sprite2D = (sound? soundOn : soundOff);
		buttons [2].sprite2D = (music ? musicOn : musicOff);
	}

	void Update () {
		buttons [3].sprite2D = (sound? soundOn : soundOff);
		buttons [2].sprite2D = (music ? musicOn : musicOff);
	}

	void soundClick () {
		SoundManager.manager.setSound (!sound);
		sound = !sound;
		buttons [3].sprite2D = (sound? soundOn : soundOff);
	}

	void musicClick () {
		SoundManager.manager.setMusic (!music);
		music = !music;
		buttons [2].sprite2D = (music ? musicOn : musicOff);			
	}

}
