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

	private bool gameOver;

    public string Word {
        get {
            return originalWord;
        }
    }
	public void GameOver(){
		gameOver = true;
	}
    protected void MatchCharacter(char c) {
		if (gameOver) {
			return;
		}
        if (remainingWord.StartsWith(c.ToString())) {
            // Trigger success
            if (remainingWord.Length > 1) {
                remainingWord = remainingWord.Substring(1);
                GetComponentInChildren<TextMesh>().text = remainingWord;
                WordPartial.Invoke(originalWord, remainingWord);
            } else {
                // Trigger completed
                WordCompleted.Invoke();
            }
        }
    }

	public void Enable (string text) {
        originalWord = remainingWord = allButTextRegex.Replace(text.ToLower(),"");;
        GetComponentInChildren<TextMesh>().text = remainingWord;
		GetComponentInChildren<TextMesh> ().fontSize = 20;
		List<UnityEngine.Color> colors = new List<Color> ();
		colors.Add (Color.red);
		colors.Add (Color.black);
		colors.Add (Color.blue);
		GetComponentInChildren<TextMesh> ().color = colors[Random.Range(1,colors.Count)];
		KeyboardTyper.keyTyped.AddListener(MatchCharacter);
	}
    
}
