using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {
	
	 void Start() {
	//	Button button = gameObject.GetComponent<Button>();
	//	button.onClick.AddListener (OnClick);
	}

  	void OnClick() {
		SceneManager.LoadScene ("game");
		loadGameLevel (1);
	}
	void loadGameLevel(int level ) {
		//Object level = TextHelper.loadLevel(level);
		//for level.counts
		// new Pidgeon();
		// set
	}
}
