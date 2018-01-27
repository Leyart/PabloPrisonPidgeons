using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pigeon : MonoBehaviour, IKillable, IFlyable{

	public class PigeonArrivedEvent : UnityEvent<string> {}
	public class PigeonKilledEvent : UnityEvent<string> {};
	public class PigeonHitEvent : UnityEvent<int> {};

	public PigeonArrivedEvent PigeonArrived = new PigeonArrivedEvent();
	public PigeonKilledEvent PigeonKilled = new PigeonKilledEvent();
	public PigeonHitEvent PigeonHit = new PigeonHitEvent();

	public float speed;

	// 2 elements for the rotation and 1 for the scaling.
	public Vector3 rotation;
	public string text;
	System.Guid id;
	GameObject path;
	StringReader reader;
	ArrayList pathPoints;

	void Awake() {
		reader = this.GetComponent<StringReader>();
		speed = Random.Range(0.10f, 0.20f);

		reader.WordCompleted.AddListener(Kill);
		reader.WordPartial.AddListener((word, partial) => {
			PigeonHit.Invoke(word.Length - partial.Length);
		});
	}

	// Use this for initialization
	void Start () {
		Vector2 posLB = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		Vector2 posRU = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
		Vector2 startPos = new Vector2 (posLB.x,  Random.Range(posLB.y, posRU.y));
		this.transform.position = startPos;
		this.id =  System.Guid.NewGuid();
		pathPoints = GetComponentInChildren<CatmullRomSpline>().path;
	}

	void Update() {
		Fly();
	}

	public void SendPigeon(string text) {
		reader.Enable (text);
	}

	public void Fly() {
		if (pathPoints != null && pathPoints.Count > 0) {
			if (Vector2.Distance(this.transform.position, (Vector2) pathPoints[0]) > 0.2f) {
				Vector2 nextPos = Vector2.MoveTowards(this.transform.position, (Vector2) pathPoints[0], speed);
				this.transform.position = nextPos;
			} else {
				pathPoints.RemoveAt(0);
			}
		} else {
			ArrivedAtTheEnd();
		}
	}

	void ArrivedAtTheEnd() {
		PigeonArrived.Invoke(reader.Word);
		Destroy (gameObject);
	}

	public void Kill() {
		PigeonKilled.Invoke(reader.Word);
		Destroy (gameObject);
	}
}
