using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;

public class KeyboardTyper : MonoBehaviour {

	public class KeyTypedEvent : UnityEvent<char> {}

	public static KeyTypedEvent keyTyped = new KeyTypedEvent();
	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			char[] characters = Input.inputString.ToCharArray();
			foreach(char c in characters) {
				if (Regex.Match(c.ToString(), "[a-zA-Z]").Success) {
					keyTyped.Invoke(c);
				}
			}
		}
	}

}
