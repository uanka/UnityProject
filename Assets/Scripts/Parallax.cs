using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

	//[0, 1] - 0-фон стоїть на місці як платформи
	//1 - фон рухається так само як кролик
	public float slowdown = 0.5f;
	Vector3 lastCameraPosition;
	void Awake() {
		lastCameraPosition = Camera.main.transform.position;
	}
	void LateUpdate() {
		Vector3 new_position = Camera.main.transform.position;
		Vector3 diff = new_position - lastCameraPosition;

		lastCameraPosition = new_position;

		Vector3 my_position = this.transform.position;
		//Рухаємо фон в туж сторону що й камера але з іншою
		//швидкістю
		my_position += slowdown * diff;
		this.transform.position = my_position;
	}
}
