using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	public MyButton playButton = null;
	private UnityAction playAction;
	// Use this for initialization
	void Awake () {
		playAction += onPlay;
		playButton.signalOnClick.AddListener (playAction);
		Debug.Log("Hello from MainMenuController");
	//	playButton.signalOnClick.AddListener( () => { this.onPlay ();});
	}

	public void onPlay () {
		SceneManager.LoadScene ("ChooseLevel");
		Debug.Log ("onPlay");
	}

}
