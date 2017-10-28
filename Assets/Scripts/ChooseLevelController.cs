using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;
public class ChooseLevelController : MonoBehaviour {

	public static ChooseLevelController current = null;

	public UILabel coinsLabel;

	// Use this for initialization
	void Awake () {
		current = this;
		coinsLabel = UIRoot.FindObjectOfType<UILabel> ();
		if (LevelController.current)
			coinsLabel.text = LevelController.current.getCoins ().ToString ("D4");
	}

	Vector3 startPosition = new Vector3 (0f, 0.5f, -6f);

	public void onRabbitDeath (HeroRabbit rabbit) {
		//При смерті кролика повертаємо на початкову позицію
		rabbit.die ();
		StartCoroutine (returnRabbit (rabbit));
	}

	IEnumerator returnRabbit (HeroRabbit rabbit) {
		yield return new WaitForSeconds (1);
		rabbit.restore ();
		rabbit.transform.position = this.startPosition;
	}


	//SceneManager.LoadScene ("ChooseLevelScene");

}
