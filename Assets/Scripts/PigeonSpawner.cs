using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PigeonSpawner : MonoBehaviour {
	List<Pigeon> pigeons = new List<Pigeon>();
	public Pigeon pigeon;
	List<string> tokens;
	public bool isGameOver;
	public GameObject explosion;
	string senderName = "@PabloChannel";
	string FIXED_PABLO_NAME = "@PabloChannel";
	string FIXED_PABLO_IMAGE = "http://pbs.twimg.com/profile_images/957266931555491841/pLkxocd2_normal.jpg";
	string imgUrl = "";


	public void loadGameLevel(int level ) {
		senderName = FIXED_PABLO_NAME;
		imgUrl = FIXED_PABLO_IMAGE;
		bool isLiveFeed = false;
		TwitterController controller = GetComponent<TwitterController> ();
		TextLevelHelper levelHelper = new TextLevelHelper (level,senderName,FIXED_PABLO_IMAGE);
		if (!controller.isTweetsLoaded()) {
			controller.LoadTweets ();
		}
		if (controller.isAuthenticated && level % 2 == 1) {
			if (controller.tweets.Count > 0) {
				isLiveFeed = true;
				int index = Random.Range (0, controller.tweets.Count - 1);
				senderName = controller.tweets [index].username;
				imgUrl = controller.tweets [index].image;
				imgUrl = imgUrl.Replace ("\\", "");
				tokens = new List<string> (levelHelper.GetTokens (controller.tweets[index].text));
				levelHelper.setUserId (senderName);
				controller.tweets.RemoveAt (index);
			} else {
				string[] newTokens = levelHelper.GetTokens ();
				tokens = new List<string> (newTokens);
			}
		} else {
			string[] newTokens = levelHelper.GetTokens ();
			if (newTokens != null) {
				tokens = new List<string> (newTokens);
			}
		}
		if (tokens == null || tokens.Count <= 0) {
			GetComponent<GameControler> ().Winning ();
		} else {
			StartCoroutine(GetComponent<GameControler> ().UpdateTwitterFeed (senderName, imgUrl,isLiveFeed));
			isGameOver = false;
			SpawnNextPigeon ();
		}
	}

	private IEnumerator waitThenCallback(int time, System.Action callback) {
		yield return new WaitForSeconds(time);
		callback();
	}

	void SpawnNextPigeon() {
		if (tokens.Count > 0) {
			string word = tokens[0];
			tokens.RemoveAt(0);
			if (!string.IsNullOrEmpty (word)) {
				Pigeon pigeon = Instantiate<Pigeon> (this.pigeon);

				// events
				pigeon.PigeonArrived.AddListener ((id) => {
					RemovePigeon (pigeon);
					GetComponent<GameControler> ().UpdateTransmissionCount ();
				});
				pigeon.PigeonKilled.AddListener ((id) => {
					GameObject e = Instantiate<GameObject> (this.explosion);
					e.transform.position = pigeon.transform.position;
					StartCoroutine (waitThenCallback (2, () => {
						Destroy (e);
					}));

					RemovePigeon (pigeon);
					GetComponent<GameControler> ().UpdateScoreCount ();
				});
				pigeon.PigeonHit.AddListener ((life) => {

					if (life == 2 && !isGameOver) {
						SpawnNextPigeon ();
					}
				});

				// tracking
				pigeons.Add (pigeon);

				pigeon.SendPigeon (word);

				GetComponent<AudioSource> ().Play ();
			}
		}
	}

	public void GameOver(){
		isGameOver = true;
	}

	void RemovePigeon(Pigeon pigeon) {
		pigeons.Remove(pigeon);
		SpawnNextPigeon();
	}

	public bool noMorePigeon(){
		return pigeons.Count <= 0;
	}
		
}
