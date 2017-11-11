using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalsController : MonoBehaviour {


	public Sprite crystal_blue = null;
	public Sprite crystal_green = null;
	public Sprite crystal_red = null;

	UI2DSprite[] crystals = null;
	// Use this for initialization
	void Start () {
		crystals = this.GetComponentsInChildren<UI2DSprite> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addColour (CrystalColour colour) {
		switch (colour) {
		case CrystalColour.Blue:
			crystals [1].sprite2D = crystal_blue;
			break;
		case CrystalColour.Green:
			crystals [2].sprite2D = crystal_green;
			break;
		case CrystalColour.Red:
			crystals [3].sprite2D = crystal_red;
			break;

		}
	}
}
