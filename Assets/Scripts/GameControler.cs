﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<PigeonSpawner> ().loadGameLevel (1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}