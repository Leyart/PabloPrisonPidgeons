using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAudio : MonoBehaviour
{
	private static MainMenuAudio instance;

	void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(this.gameObject);
			return;
		}
		else
		{
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	void Start()
	{
		//if (!instance.audio.isPlaying)
		//	audio.Play();
	}

	public void turnMusicOff()
	{
		if (instance != null)
		{
			//if (instance.audio.isPlaying)
			//    instance.audio.Stop();
			Destroy(this.gameObject);
			instance = null;
		}
	}

	void OnApplicationQuit()
	{
		instance = null;
	}
}
