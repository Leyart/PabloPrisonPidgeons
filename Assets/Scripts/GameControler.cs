using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControler : MonoBehaviour
{


	public GameObject scoreText;
	private int score;
	public GameObject transmittedMessages;
	public GameObject levelText;

	public GameObject redScrollPrefab;
	private int transmittedMessagesCount;
	private int level = 1;

	private Animator scoreAnimator;
	// private Animator transmittedAnimator;
	PigeonSpawner spawner;
	private bool isGameStarted = false;
	public GameObject twitterFeed;

	private List<GameObject> scrolls = new List<GameObject> ();

	void Start ()
	{
		
		spawner = GetComponent<PigeonSpawner> ();
		level = 1;
		UpdateLevel ();
		spawner.loadGameLevel (level);
		score = 0;
		transmittedMessagesCount = 0;
		scoreAnimator = scoreText.GetComponentInChildren<Animator> ();
		scoreAnimator.SetBool ("isGreen", true);

		createRedScrolls ();

		UpdateScoreView ();
		UpdateTransmissionView ();
		isGameStarted = true;

	}

	public void GameOver ()
	{
		GetComponent<PigeonSpawner> ().GameOver ();
		SceneManager.LoadScene (2);
	}
		

	// Update is called once per frame
	void Update ()
	{
		if (isGameStarted) {
			if (spawner.noMorePigeon ()) {
				NextLevel ();
			}
		}
	}

	private void UpdateScoreView ()
	{
		scoreText.GetComponent<TextMesh> ().text = " " + score;
		scoreAnimator.SetTrigger ("trigger");
	}

	public void UpdateScoreCount ()
	{
		score += 1;
		UpdateScoreView ();
	}

	private void createRedScrolls ()
	{
		Transform firstScroll = 
			transmittedMessages.transform.GetChild (0);
		for (int i = 0; i < GamePlayConstants.instance.allowedErrors; i++) {
			
			GameObject scroll = Instantiate (redScrollPrefab,
				new Vector3 (firstScroll.localPosition.x + (i * 3),
					                    firstScroll.localPosition.y, 
					                    0),
				                    Quaternion.identity);
			scroll.transform.SetParent (transmittedMessages.transform, false);
			Animator animator = scroll.GetComponentInChildren<Animator> ();
			animator.SetBool ("isRed", true);
			scrolls.Add (scroll);
		}
		Destroy (firstScroll.gameObject);
	}

	private void UpdateTransmissionView ()
	{
		int currentScrolls = GamePlayConstants.instance.allowedErrors - transmittedMessagesCount;
		if (scrolls.Count > currentScrolls) { 
			GameObject lastScroll = scrolls [scrolls.Count - 1];
			scrolls.RemoveAt (scrolls.Count - 1);
			StartCoroutine (KillOnAnimationEnd (lastScroll));
		}

	}

	private IEnumerator KillOnAnimationEnd (GameObject scroll)
	{
		Animator animator = scroll.GetComponent<Animator> ();
		animator.SetTrigger ("trigger");
		yield return new WaitForSeconds (.5f);
		Destroy (scroll);
	} 

	public IEnumerator UpdateTwitterFeed(string userId, string imageUrl, bool isLive) {
		twitterFeed.SetActive (true);
		twitterFeed.GetComponent<TextMesh> ().text = userId;
		WWW www = new WWW(imageUrl);
		yield return www;
		twitterFeed.GetComponentInChildren<SpriteRenderer>()
			.sprite = Sprite.Create(
				www.texture,
				new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0)
			);
		twitterFeed.transform.Find("Live").gameObject.SetActive(isLive);
	}


	public void UpdateTransmissionCount ()
	{
		transmittedMessagesCount++;
		UpdateTransmissionView ();
		if (transmittedMessagesCount >= GamePlayConstants.instance.allowedErrors) {
			GameOver ();
		}
	}

	void UpdateLevel ()
	{
		levelText.GetComponent<TextMesh> ().text = level.ToString ();
	}

	public void NextLevel ()
	{
		level++;
		UpdateLevel ();
		if (level >= 10) {
			Winning ();
		} else {
			spawner.loadGameLevel (level);
		}
	}

	public void Winning ()
	{
		SceneManager.LoadScene (3);
	}


	IEnumerator LoadImage(string url, Texture2D tex)
	{
		tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
		using (WWW www = new WWW(url))
		{
			yield return www;
			www.LoadImageIntoTexture(tex);
		}
	}
}
