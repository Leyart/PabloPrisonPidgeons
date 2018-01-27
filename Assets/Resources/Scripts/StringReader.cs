using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;

public class StringReader : MonoBehaviour {

    private string s;
    private string sLeft;
	public bool isCompleted {
		get;
		set;
	}

    protected void MatchCharacter(char c) {
        if (sLeft.StartsWith(c.ToString())) {
            // Trigger success
            if (sLeft.Length > 1) {
                sLeft = sLeft.Substring(1);
                Debug.Log("Remaining: " + sLeft);
            } else {
                // Trigger completed
                Debug.Log("word " + s + " completed!");
				isCompleted = true;
            }
        }
    }

	// Use this for initialization
	void Start () {
		isCompleted = false;
	}

	public void Enable (string text) {
		s = sLeft = text.ToLower();
		KeyboardTyper.keyTyped.AddListener(MatchCharacter);
	}
    
}
