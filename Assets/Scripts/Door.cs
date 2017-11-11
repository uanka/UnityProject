using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Level {
	Level1,
	Level2
}
public class Door : MonoBehaviour {

	public Level level;
	string scene = null;

	// Use this for initialization
	void Start () {
		if (level == Level.Level1)
			scene = "Level1";
		if (level == Level.Level2)
			scene = "Level2";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D collider) {
		//		if (!this.hideAnimation) {
		HeroRabbit rabbit = collider.GetComponent<HeroRabbit> ();
		if (rabbit != null) {
			SceneManager.LoadScene (scene);
		}
		//		}
	}
}
