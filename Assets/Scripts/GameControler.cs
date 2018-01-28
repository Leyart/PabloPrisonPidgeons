using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControler : MonoBehaviour {


	public GameObject scoreText;
	private int score;
	public GameObject transmittedMessages;
	public GameObject levelText;
	private int transmittedMessagesCount;
	private int level = 1;
	//private int maxNumberToFail = 3;
	private Animator scoreAnimator;
	private Animator transmittedAnimator;
	PigeonSpawner spawner;
	private bool isGameStarted = false;
	public GameObject twitterFeed;

	void Start () {
		
		spawner = GetComponent<PigeonSpawner> ();
		level = 1;
		UpdateLevel ();
		spawner.loadGameLevel (level);
		score = 0;
		transmittedMessagesCount = 0;
		scoreAnimator = scoreText.GetComponentInChildren<Animator> ();
		scoreAnimator.SetBool ("isGreen", true);
		transmittedAnimator = transmittedMessages.GetComponentInChildren<Animator> ();
		transmittedAnimator.SetBool ("isRed", true);
		UpdateScoreView ();
		UpdateTransmissionView ();
		isGameStarted = true;

	}

	public void GameOver(){
		GetComponent<PigeonSpawner> ().GameOver ();
		SceneManager.LoadScene (2);
	}
		

	// Update is called once per frame
	void Update () {
		if (isGameStarted) {
			if (spawner.noMorePigeon ()) {
				NextLevel ();
			}
		}
	}
	private void UpdateScoreView(){
		scoreText.GetComponent<TextMesh>().text = " " +score;
		scoreAnimator.SetTrigger ("trigger");
	}
	public void UpdateScoreCount(){
		score += 1;
		UpdateScoreView ();
	}


	private void UpdateTransmissionView(){
		transmittedMessages.GetComponent<TextMesh>().text = " " + transmittedMessagesCount;

		transmittedAnimator.SetTrigger ("trigger");

	}

	public IEnumerator UpdateTwitterFeed(string userId, string imageUrl) {
		twitterFeed.SetActive (true);
		twitterFeed.GetComponent<TextMesh> ().text = userId;
		WWW www = new WWW(imageUrl);
		yield return www;
		twitterFeed.GetComponentInChildren<SpriteRenderer>()
			.sprite = Sprite.Create(
				www.texture,
				new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0)
			);
	}


	public void UpdateTransmissionCount(){
		transmittedMessagesCount++;
		UpdateTransmissionView ();
		if (transmittedMessagesCount >= GamePlayConstants.instance.allowedErrors) {
			GameOver ();
		}
	}
	void UpdateLevel(){
		levelText.GetComponent<TextMesh> ().text = level.ToString();
	}
	public void NextLevel() {
		level++;
		UpdateLevel ();
		if (level >= 10) {
			Winning ();
		} else {
			spawner.loadGameLevel (level);
		}
	}

	public void Winning() {
		SceneManager.LoadScene (3);
	}
}
