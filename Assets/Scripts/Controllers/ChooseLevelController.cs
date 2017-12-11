using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using UnityEngine.SceneManagement;
public class ChooseLevelController : MonoBehaviour {

	public static ChooseLevelController current = null;

	public UILabel coinsLabel;
	bool sound = false;
	// Use this for initialization
	void Awake () {
		current = this;
		coinsLabel = UIRoot.FindObjectOfType<UILabel> ();
		coinsLabel.text = PlayerPrefs.GetInt("coins").ToString ("D4");
		sound = SoundManager.manager.isSoundOn ();
	}

	Vector3 startPosition = new Vector3 (0f, 0.5f, -6f);

	public void onRabbitDeath (HeroRabbit rabbit) {
		//При смерті кролика повертаємо на початкову позицію
		SoundManager.manager.setSound(false);
		rabbit.die ();
		StartCoroutine (returnRabbit (rabbit));
	}

	IEnumerator returnRabbit (HeroRabbit rabbit) {
		yield return new WaitForSeconds (1);
		rabbit.restore ();
		rabbit.transform.position = this.startPosition;
		SoundManager.manager.setSound(sound);

	}

}
