using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pigeon : MonoBehaviour, IKillable, IFlyable{

	public float speed;

	// 2 elements for the rotation and 1 for the scaling.
	public Vector3 rotation;
	public string text;
	System.Guid id;
	GameObject path;
	StringReader reader;


	// Use this for initialization
	void Start () {
		Vector2 posLB = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		Vector2 posRU = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
		Vector2 startPos = new Vector2 (posLB.x,  Random.Range(posLB.y, posRU.y));
		this.transform.position = startPos;
		this.id =  System.Guid.NewGuid();
		StringReader reader = GetComponentInChildren<StringReader> ();
		TextLevelHelper levelHelper = new TextLevelHelper (Random.Range (1, 5));
		reader.Enable (levelHelper.GetTokens () [0]);
	}


	public void setPath(GameObject path){
		this.path = path;
	}
	void Update() {
		// path.getUpdatedPosition (x,y, speed);
		Fly();
		//Vector2 pos = new Vector2 (this.transform.position.x + 1 / 10, this.transform.position.y);


	}

	public void SendPigeon(string text) {
		reader.Enable (text);
	}

	public void Fly() {
		this.transform.Translate (0.01f,0,0);
		Debug.Log ("coo from "+this.id.ToString()+ "with text: "+text);
	}

	public void Kill() {
		if (this.reader.isCompleted) {
			Destroy (gameObject);
		}
	}
}
