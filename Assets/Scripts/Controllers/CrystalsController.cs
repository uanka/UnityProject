using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CrystalColour {
	Red,
	Green,
	Blue,
	None
}

public class CrystalsController : MonoBehaviour {


	public Sprite crystal_blue = null;
	public Sprite crystal_green = null;
	public Sprite crystal_red = null;

	public CrystalsController crystalController = null;
	public UI2DSprite[] crystals = null;
	// Use this for initialization
	void Start () {
		crystals = this.GetComponentsInChildren<UI2DSprite> ();
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

	public bool allCrystalsGathered () {
		if (crystals [1].sprite2D != crystal_blue)
			return false;
		if (crystals [2].sprite2D != crystal_green)
			return false;
		if (crystals [3].sprite2D != crystal_red)
			return false;
		return true;
	}
}
