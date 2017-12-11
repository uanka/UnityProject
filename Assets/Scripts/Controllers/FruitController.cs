using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour {

	// Use this for initialization
	SpriteRenderer[] fruits = null;
	List<int> gathered = null;
	void Awake () {
		gathered = JsonUtility.FromJson<LevelStat> (PlayerPrefs.GetString ("Level1stats")).collectedFruits;
		fruits = this.GetComponentsInChildren<SpriteRenderer> ();
		StartCoroutine(hide ());
	}

	IEnumerator hide () {
		yield return new WaitForSeconds (1);
		int amount = LevelController.current.getMaxFruits ();
		for (int i = 1; i <= amount; i++) {
			if (gathered.Contains (i)) {
				fruits [i-1].color = new Color (1f, 1f, 1f, 0.5f);
				Fruit fruit = fruits [i-1].GetComponentInParent<Fruit> ();
				fruit.gathered = true;
			}
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
