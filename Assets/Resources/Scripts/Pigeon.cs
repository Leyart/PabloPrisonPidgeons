﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pigeon : MonoBehaviour {

	public float speed;

	// 2 elements for the rotation and 1 for the scaling.
	public Vector3 rotation;
	public string text;
	public GameObject path;
	// Use this for initialization
	void Start () {
		Vector2 posLB = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		Vector2 posRU = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
		Vector2 startPos = new Vector2 (posLB.x,  Random.Range(posLB.y, posRU.y));
		this.transform.position = startPos;

	}



	void Update() {
		// path.getUpdatedPosition (x,y, speed);

		//Vector2 pos = new Vector2 (this.transform.position.x + 1 / 10, this.transform.position.y);
		this.transform.Translate (0.01f,0,0);



	}

}