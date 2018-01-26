using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {
	List<GameObject> pidgeons = new List<GameObject>();
	public GameObject pidgeon;
	public GameObject pidgeonHolder;
	 void Start() {
		loadGameLevel (1);
	}

  
	void loadGameLevel(int level ) {
		TextLevelHelper levelHelper = new TextLevelHelper(level);
		string [] tokens =  levelHelper.GetTokens ();
		for (int i = 0; i < tokens.Length; i++) {
			//Transform identity = new Transform ();
			Vector3 position = new Vector3 (i, i, 0);
			GameObject instance = Instantiate(pidgeon, position,  Quaternion.identity);
			instance.transform.SetParent (pidgeonHolder.transform);
			pidgeons.Add (instance);

		}
	}
}
