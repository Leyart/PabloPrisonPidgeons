using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardTyper : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			string[] characters = Input.inputString.Split();
			foreach(string c in characters) {
				Debug.Log("Pressed " + c);
			}
		}
	}
}
