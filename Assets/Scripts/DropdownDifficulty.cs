using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownDifficulty : MonoBehaviour {



	// Use this for initialization
	void Start () {
		Dropdown dropdown = gameObject.GetComponent<Dropdown> ();
		dropdown.ClearOptions (); 
		dropdown.AddOptions (new List<string>(GamePlayConstants.Difficulties));
		dropdown.RefreshShownValue ();


		dropdown.onValueChanged.AddListener(delegate {
			DropdownValueChanged(dropdown);
		});	}
	
	void DropdownValueChanged(Dropdown dropdown)
	{
		//string difficultyName = change.value as string;
		string difficultyName = GamePlayConstants.Difficulties[dropdown.value];
		if (difficultyName != null) {
			GamePlayConstants.SetDifficulty (difficultyName);
		} else {
			Debug.LogWarning ("The dropdown value is not a string");
		}
	}
}
