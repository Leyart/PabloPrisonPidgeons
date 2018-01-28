using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardSpeaker : MonoBehaviour {

	public GameObject textObject;


	// Use this for initialization
	void Start () {
		TextMesh textMesh = textObject.GetComponent<TextMesh>();
		textMesh.text = "";
		KeyboardTyper.keyTyped.AddListener((c) => {
			string text = textMesh.text;
			text = text + c;
			if (text.Length > 10) {
				text = text.Substring(1, 10);
			}
			textMesh.text = text;
		});

		InvokeRepeating("AddSpaces", 0.3f, 0.3f);
	}

	void AddSpaces() {
		TextMesh textMesh = textObject.GetComponent<TextMesh>();
		string text = textMesh.text + " ";
		if (text.Length > 10) {
			text = text.Substring(1, 10);
		}
		textMesh.text = text;
	}

}
