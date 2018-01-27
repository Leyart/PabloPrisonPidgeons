using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;

public class StringReader : MonoBehaviour {

    public class WordCompletedEvent : UnityEvent {}
    public class WordPartialEvent : UnityEvent<string, string> {};

    public WordCompletedEvent WordCompleted = new WordCompletedEvent();
    public WordPartialEvent WordPartial = new WordPartialEvent();


    private static readonly Regex allButTextRegex = new Regex("[^a-zA-Z]");
    private string originalWord;
    private string remainingWord;

    public string Word {
        get {
            return originalWord;
        }
    }

    protected void MatchCharacter(char c) {
        if (remainingWord.StartsWith(c.ToString())) {
            // Trigger success
            if (remainingWord.Length > 1) {
                remainingWord = remainingWord.Substring(1);
                Debug.Log("Remaining: " + remainingWord);
                GetComponentInChildren<TextMesh>().text = remainingWord;
                WordPartial.Invoke(originalWord, remainingWord);
            } else {
                // Trigger completed
                Debug.Log("Word " + originalWord + " completed!");
                WordCompleted.Invoke();
            }
        }
    }

	public void Enable (string text) {
        originalWord = remainingWord = allButTextRegex.Replace(text.ToLower(),"");;
        GetComponentInChildren<TextMesh>().text = remainingWord;
		KeyboardTyper.keyTyped.AddListener(MatchCharacter);
	}
    
}
