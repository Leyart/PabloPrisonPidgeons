using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class KeyboardTyper : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			char[] characters = Input.inputString.ToCharArray();
			foreach(char c in characters) {
				if (Regex.Match(c.ToString(), "[a-zA-Z]").Success) {
					Debug.Log("Pressed " + c);
				}
			}
		}
	}
}
