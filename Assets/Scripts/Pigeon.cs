﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pigeon : MonoBehaviour, IKillable, IFlyable{

	public float speed;

	// 2 elements for the rotation and 1 for the scaling.
	public Vector3 rotation;
	public string text;
	System.Guid id;
	GameObject path;
	StringReader reader;
	ArrayList pathPoints;


	void Awake() {
		reader = this.GetComponent<StringReader>();
		speed = Random.Range(0.05f, 0.08f);
	}

	// Use this for initialization
	void Start () {
		Vector2 posLB = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		Vector2 posRU = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
		Vector2 startPos = new Vector2 (posLB.x,  Random.Range(posLB.y, posRU.y));
		this.transform.position = startPos;
		this.id =  System.Guid.NewGuid();
		pathPoints = GetComponentInChildren<CatmullRomSpline>().path;
	}

	void Update() {
		// path.getUpdatedPosition (x,y, speed);
		Fly();
		//Vector2 pos = new Vector2 (this.transform.position.x + 1 / 10, this.transform.position.y);


	}

	public void SendPigeon(string text) {
		reader.Enable (text);
	}

	public void Fly() {
		if (pathPoints != null && pathPoints.Count > 0) {
			if (Vector2.Distance(this.transform.position, (Vector2) pathPoints[0]) > 0.2f) {
				Vector2 nextPos = Vector2.MoveTowards(this.transform.position, (Vector2) pathPoints[0], speed);
				this.transform.position = nextPos;
			} else {
				pathPoints.RemoveAt(0);
			}
		} else {
				ArrivedAtTheEnd();
				Kill();
		}
	}

	public void ArrivedAtTheEnd() {
		// Do something!
	}

	public void Kill() {
		Destroy (gameObject);
	}
}
