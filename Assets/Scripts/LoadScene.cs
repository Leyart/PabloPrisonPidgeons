using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {
	List<GameObject> pigeons = new List<GameObject>();
	public GameObject pigeon;
	public GameObject pigeonHolder;
	 void Start() {
		loadGameLevel (1);
	}

  
	void loadGameLevel(int level ) {
		TextLevelHelper levelHelper = new TextLevelHelper(level);
		string [] tokens =  levelHelper.GetTokens ();
		for (int i = 0; i < tokens.Length; i++) {
			GameObject instance = Instantiate(pigeon);
			pigeons.Add (instance);

		}
	}
}
