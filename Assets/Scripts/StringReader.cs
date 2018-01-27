using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;

public class StringReader : MonoBehaviour {

    public StringReader(string s) {
        this.s = sLeft = s;
    }
    public string s;
    private string sLeft;

    void Awake() {
		TextLevelHelper levelHelper = new TextLevelHelper (Random.Range (1, 5));
		sLeft = s = levelHelper.GetTokens()[0];
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
            }
        }
    }

	// Use this for initialization
	void Start () {
		KeyboardTyper.keyTyped.AddListener(MatchCharacter);
	}
    
}
