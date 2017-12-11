using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Collectable {

	float direction = 1;
	public float speed = 3;

	void Start () {
		StartCoroutine (destroyLater ());
	}
		
	IEnumerator destroyLater () {
		yield return new WaitForSeconds (3);
		Destroy (this.gameObject);
	}

	public void launch (float direction) {
		this.direction = direction;
		if (direction < 0)
			this.GetComponent<SpriteRenderer> ().flipX = true;
	}

	void Update () {
		Vector3 pos = this.transform.position;
		this.transform.position = pos + Vector3.right * this.direction * Time.deltaTime * speed;
	}

	protected override void OnRabbitHit (HeroRabbit rabbit) {
		LevelController.current.onRabbitDeath (rabbit);
		Destroy (this.gameObject);
	}
			
}
