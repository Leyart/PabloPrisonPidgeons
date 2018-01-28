using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayConstants {




	public static GamePlayConstants easy = new GamePlayConstants("Easy", 7, 0f);
	public static GamePlayConstants normal = new GamePlayConstants("Normal",5,  0.025f);
	public static GamePlayConstants hard = new GamePlayConstants("Hard", 3, 0.05f);
	public static GamePlayConstants[] gameplayConstantsCollection = new GamePlayConstants[] {easy,normal,hard};
	public static string[] Difficulties = new string[] {easy.difficultyName, normal.difficultyName, hard.difficultyName};
	public static GamePlayConstants instance = easy;


	// FIELDS

	public float speedModifier;
	public int allowedErrors;
	public string difficultyName;

	GamePlayConstants(string name, int allowedErrors, float speedModifier) {
		this.speedModifier = speedModifier;
		this.difficultyName= name;
		this.allowedErrors = allowedErrors;
	}
	public static  void SetDifficulty(string name) {
		foreach(GamePlayConstants gameplayConstants in gameplayConstantsCollection ) {
			if( gameplayConstants.difficultyName == name) { 
				Debug.Log ("Set the GamePlayConstants to " + name);
				instance = gameplayConstants;
				return;
			}
		}
	}
}
