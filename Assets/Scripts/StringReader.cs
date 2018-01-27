using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;

public class StringReader : MonoBehaviour {

    private static readonly Regex allButTextRegex = new Regex("[^a-zA-Z]");
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
                GetComponentInChildren<TextMesh>().text = sLeft;
            } else {
                // Trigger completed
                Debug.Log("word " + s + " completed!");
				isCompleted = true;
                GetComponent<Pigeon>().Kill();
            }
        }
    }

	// Use this for initialization
	void Start () {
		isCompleted = false;
	}

	public void Enable (string text) {
        s = sLeft = allButTextRegex.Replace(text.ToLower(),"");;
        GetComponentInChildren<TextMesh>().text = sLeft;
		KeyboardTyper.keyTyped.AddListener(MatchCharacter);
	}
    
}
