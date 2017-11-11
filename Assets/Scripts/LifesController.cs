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
	
	// Update is called once per frame
	void Update () {
		
	}

	public void die () {
		lifes [LevelController.current.getLifes ()+1].sprite2D = life_used;
	}
}
