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
		score = 0;
		transmittedMessagesCount = 0;
		UpdateScoreView ();
		UpdateTransmissionView ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void UpdateScoreView(){
		scoreText.GetComponent<TextMesh>().text =  "Points : " + score;
	}
	public void UpdateScoreCount(){
		score++;
		UpdateScoreView ();
	}


	private void UpdateTransmissionView(){
		
		transmittedMessages.GetComponent<TextMesh>().text =  "Transmitted Messages: " + transmittedMessagesCount;
	}
	public void UpdateTransmissionCount(){
		transmittedMessagesCount++;
		UpdateTransmissionView ();
	}
}
