using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pidgeon : MonoBehaviour {

	public float x;
	public float y;
	public float speed;

	// 2 elements for the rotation and 1 for the scaling.
	public Vector3 rotation;
	public string text;
	public Object path;

	public Pidgeon() {

	}

	void Update() {
		// path.getUpdatedPosition (x,y, speed);
	}

}
