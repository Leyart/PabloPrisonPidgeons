using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpeaker : MonoBehaviour {

	public string[] sentences;
	public GameObject textObject;
	// Use this for initialization
	void Start () {
		InvokeRepeating("ChangeSentence", 1, 1);
	}

	void ChangeSentence() {
		TextMesh textMesh = textObject.GetComponent<TextMesh>();
		textMesh.text = sentences[Random.Range(0, sentences.Length - 1)];
	}

}
