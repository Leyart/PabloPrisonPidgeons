using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PigeonSpawner : MonoBehaviour {
	List<Pigeon> pigeons = new List<Pigeon>();
	public Pigeon pigeon;
	public GameObject pigeonHolder;
	List<string> tokens;
	bool gameOver;
	public void loadGameLevel(int level ) {
		TwitterController controller = GetComponent<TwitterController> ();
		TextLevelHelper levelHelper = new TextLevelHelper (level);
		if (controller.isAuthenticated) {
			controller.LoadTweets ();
			if (controller.tweets.Length > 0) {
				tokens = new List<string> (levelHelper.GetTokens (controller.tweets [0]));
			} else {
				
				tokens = new List<string> (levelHelper.GetTokens ());
			}
		} else {
			tokens = new List<string> (levelHelper.GetTokens ());
		}
		gameOver = false;
		SpawnNextPigeon();
	}

	void SpawnNextPigeon() {
		if (tokens.Count > 0) {
			string word = tokens[0];
			tokens.RemoveAt(0);

			Pigeon pigeon = Instantiate<Pigeon>(this.pigeon);

			// events
			pigeon.PigeonArrived.AddListener((id) => {
				RemovePigeon(pigeon);
				GetComponent<GameControler>().UpdateTransmissionCount();
			});
			pigeon.PigeonKilled.AddListener((id) => {
				RemovePigeon(pigeon);
				GetComponent<GameControler>().UpdateScoreCount();
			});
			pigeon.PigeonHit.AddListener((life) => {

				if (life == 2 && !gameOver) {
					SpawnNextPigeon();
				}
			});

			// tracking
			pigeons.Add (pigeon);

			pigeon.SendPigeon(word);

			GetComponent<AudioSource>().Play();
		}
	}

	public void GameOver(){
		gameOver = true;
	}

	void RemovePigeon(Pigeon pigeon) {
		pigeons.Remove(pigeon);
		SpawnNextPigeon();
	}
}
