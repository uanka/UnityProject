using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifesController : MonoBehaviour {

	public Sprite life;
	public Sprite life_used;

	UI2DSprite[] lifes = null;
	// Use this for initialization
	void Start () {
		lifes = this.GetComponentsInChildren<UI2DSprite> ();
	}

	public void die (int lifesCount) {
		lifes [lifesCount].sprite2D = life_used;
	}
}
