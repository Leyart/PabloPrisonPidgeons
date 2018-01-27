using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControler : MonoBehaviour {


	public GameObject scoreText;
	private int score;
	public GameObject transmittedMessages;
	private int transmittedMessagesCount;
	// Use this for initialization
	void Start () {
		GetComponent<PigeonSpawner> ().loadGameLevel (1);
		score = 4;
		transmittedMessagesCount = 1;
		UpdateScore ();
		UpdateTransmissionCount ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void UpdateScore(){
		score++;
		scoreText.GetComponent<TextMesh>().text =  "Points : " + score;
	}

	public void UpdateTransmissionCount(){
		transmittedMessagesCount++;
		transmittedMessages.GetComponent<TextMesh>().text =  "Transmitted Messages: " + transmittedMessagesCount;
	}
}
