using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;

public class KeyboardListener : MonoBehaviour {

    protected virtual void OnKeyTyped(char c) {
        Debug.Log("I listened to " + c);
    }

	// Use this for initialization
	void Start () {
		KeyboardTyper.keyTyped.AddListener(OnKeyTyped);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
