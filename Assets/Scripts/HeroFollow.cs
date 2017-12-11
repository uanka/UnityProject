using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFollow : MonoBehaviour {

	public HeroRabbit rabbit;
	public AudioClip music = null;
	AudioSource musicSource = null;

	// Use this for initialization
	void Start () {
		musicSource = gameObject.AddComponent<AudioSource> ();
		musicSource.clip = music;
		musicSource.loop = true;
		if (SoundManager.manager.isMusicOn ()) 
			//Debug.Log( "Enabled: " + GetComponent<AudioSource> ().enabled);
			musicSource.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		//Отримуємо доступ до компонента Transform
		//це Скорочення до GetComponent<Transform>
		Transform rabbit_transform = rabbit.transform;
		//Отримуємо доступ до компонента Transform камери
		Transform camera_transform = this.transform;
		//Отримуємо доступ до координат кролика
		Vector3 rabbit_position = rabbit_transform.position;
		Vector3 camera_position = camera_transform.position;
		//Рухаємо камеру тільки по X,Y
		camera_position.x = rabbit_position.x;
		camera_position.y = rabbit_position.y;
		//Встановлюємо координати камери
		camera_transform.position = camera_position;
//		Debug.Log (SoundManager.manager.isMusicOn());
		if (SoundManager.manager.isMusicOn ())
			musicSource.Play ();
		else
			musicSource.Stop ();
	}

}
