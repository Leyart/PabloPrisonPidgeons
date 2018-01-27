using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpeaker : MonoBehaviour {

	public string[] sentences;

	// Use this for initialization
	void Start () {
		InvokeRepeating("ChangeSentence", 1, 1);
	}

	void ChangeSentence() {
		GetComponentInChildren<TextMesh>().text = sentences[Random.Range(0, sentences.Length - 1)];
	}
}
