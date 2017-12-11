using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour {

	public MyButton background = null;
	public MyButton closeButton = null;
	// Use this for initialization
	void Awake () {
		closeButton.signalOnClick.AddListener (() => this.close ());
		background.signalOnClick.AddListener (() => this.close ());
	}

	void close () {
		NGUITools.Destroy (this.gameObject);
	}

}
