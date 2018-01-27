using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PigeonSpawner : MonoBehaviour {
	List<Pigeon> pigeons = new List<Pigeon>();
	public Pigeon pigeon;
	public GameObject explosion;
	public GameObject pigeonHolder;
	List<string> tokens;
	bool gameOver;
	public void loadGameLevel(int level ) {
		TextLevelHelper levelHelper = new TextLevelHelper(level);
		tokens = new List<string>(levelHelper.GetTokens ());
		gameOver = false;
		SpawnNextPigeon();
	}

	private IEnumerator waitThenCallback(float time, Action callback)
	{
		yield return new WaitForSeconds(time);
		callback();
	}

	void SpawnNextPigeon() {
		if (tokens.Count > 0) {
			string word = tokens[0];
			tokens.RemoveAt(0);

			Pigeon pigeon = Instantiate<Pigeon>(this.pigeon);

			// events
			Pigeon.PigeonArrived.AddListener((id) => {
				RemovePigeon(pigeon);
				GetComponent<GameControler>().UpdateTransmissionCount();
			});
			Pigeon.PigeonKilled.AddListener((id, p) => {
				GameObject explosion = Instantiate<GameObject>(this.explosion);
				explosion.transform.position = p.transform.position;
				StartCoroutine(waitThenCallback(2, () => {
					Destroy(explosion);
				}));

				RemovePigeon(p);
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
