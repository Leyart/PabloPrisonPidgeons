using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardSpeaker : MonoBehaviour {

	// Use this for initialization
	void Start () {
		TextMesh textMesh = GetComponentInChildren<TextMesh>();
		textMesh.text = "";
		KeyboardTyper.keyTyped.AddListener((c) => {
			string text = textMesh.text;
			text = text + c;
			if (text.Length > 5) {
				text = text.Substring(1, 5);
			}
			textMesh.text = text;
		});
	}
	
}
