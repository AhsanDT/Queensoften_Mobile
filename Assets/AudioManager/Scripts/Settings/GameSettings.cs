using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameSettings : ScriptableObject {

	[Header ("Application Settings")]
	public float timeScale = 1f;
	public int framerate = 60;
	public bool showFPSCounter = false;

	[Header ("Audio Settings")]
	public float _MusicVolume = 0.7f;
	public float _SFXVolume = 1f;
	public float MusicVolume 
	{
		get
		{
			return PlayerPrefs.GetFloat("MusicVolume" , _MusicVolume);
		}
		set
		{
			 PlayerPrefs.SetFloat("MusicVolume", value); 
		}

	}
	public float SFXVolume
	{
		get
		{
			return PlayerPrefs.GetFloat("SFXVolume", _SFXVolume);
		}
		set
		{
			PlayerPrefs.SetFloat("SFXVolume", value);
		}

	}

}