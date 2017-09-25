using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	public Vector3 MoveBy;
	Vector3 pointA;
	Vector3 pointB;

	bool going_to_a = false;

	public float MoveSpeed = 1;
	public float time_to_wait = 1;
	public float time_to_pause = 1;

	// Use this for initialization
	void Start () {
		this.pointA = this.transform.position;
		this.pointB = this.pointA + MoveBy;
	}

	bool isArrived(Vector3 pos, Vector3 target) {
		pos.z = 0;
		target.z = 0;
		return Vector3.Distance(pos, target) < 0.02f;
	}

	void Update () {

		time_to_wait -= Time.deltaTime;
		if (time_to_wait > 0) {
			return;
		}
		
		Vector3 my_pos = this.transform.position;
		Vector3 target;

		if(going_to_a) {
			target = this.pointA;
		} else {
			target = this.pointB;
		}
		Vector3 destination = target - my_pos;
		destination.z = 0;

		if (isArrived (my_pos, target)) {
			going_to_a = !going_to_a;
			time_to_wait = time_to_pause;
		} else {
			float move = MoveSpeed = Time.deltaTime;
			float distance = Vector3.Distance (destination, my_pos);

			Vector3 move_vector = destination.normalized * Mathf.Min (move, distance);
			this.transform.position += move_vector;
		}

//		if (destination.magnitude < MoveSpeed * Time.deltaTime) {
//            this.transform.position = target;
//            this.going_to_a = !isArrived(my_pos, target);
//            pause = true;
//            time_to_wait = time_to_pause;
//        } else {
//            this.transform.position = Vector3.MoveTowards(my_pos, target, MoveSpeed * Time.deltaTime);
//        }

	}

}
