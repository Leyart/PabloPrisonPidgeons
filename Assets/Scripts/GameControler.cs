using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControler : MonoBehaviour {


	public GameObject scoreText;
	private int score;
	public GameObject transmittedMessages;
	public Canvas canvas;
	private int transmittedMessagesCount;

	private int maxNumberToFail = 3;

	public void StartGame(){
		canvas.enabled = false;
		GetComponent<PigeonSpawner> ().loadGameLevel (1);
		score = 0;
		transmittedMessagesCount = 0;
		UpdateScoreView ();
		UpdateTransmissionView ();

	}

	public void GameOver(){
		GetComponent<PigeonSpawner> ().GameOver ();
		canvas.enabled = true;
	}
		

	// Update is called once per frame
	void Update () {
		
	}
	private void UpdateScoreView(){
		scoreText.GetComponent<TextMesh>().text = " " +score;
	}
	public void UpdateScoreCount(){
		score++;
		UpdateScoreView ();
	}


	private void UpdateTransmissionView(){
		transmittedMessages.GetComponent<TextMesh>().text = " " + transmittedMessagesCount;
	}
	public void UpdateTransmissionCount(){
		transmittedMessagesCount++;
		UpdateTransmissionView ();
		if (transmittedMessagesCount >= maxNumberToFail) {
			GameOver ();
		}
	}
}
