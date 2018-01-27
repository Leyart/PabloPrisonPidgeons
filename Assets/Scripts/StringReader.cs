using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;

public class StringReader : MonoBehaviour {

    public class WordCompletedEvent : UnityEvent {}

    public WordCompletedEvent WordCompleted = new WordCompletedEvent();

    private static readonly Regex allButTextRegex = new Regex("[^a-zA-Z]");
    private string s;
    private string sLeft;

    public string Word {
        get {
            return s;
        }
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
                WordCompleted.Invoke();
            }
        }
    }

	public void Enable (string text) {
        s = sLeft = allButTextRegex.Replace(text.ToLower(),"");;
        GetComponentInChildren<TextMesh>().text = sLeft;
		KeyboardTyper.keyTyped.AddListener(MatchCharacter);
	}
    
}
