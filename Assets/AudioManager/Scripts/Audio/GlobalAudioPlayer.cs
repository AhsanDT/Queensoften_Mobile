using BeatEmUpTemplate;
using UnityEngine;

public static class GlobalAudioPlayer {

	public static BeatEmUpTemplate.AudioPlayer audioPlayer;

	public static void PlaySFX(string sfxName){
		if(AudioPlayer.Instance)
        {
			if (audioPlayer != null && sfxName != "") audioPlayer.playSFX(sfxName);
		}
		
	}

	public static void PlaySFXAtPosition(string sfxName, Vector3 position){
		if (AudioPlayer.Instance)
        {
			if (audioPlayer != null && sfxName != "") audioPlayer.playSFXAtPosition(sfxName, position);
		}
			
	}

	public static void PlaySFXAtPosition(string sfxName, Vector3 position, Transform parent){

		if (AudioPlayer.Instance)
        {
			if (audioPlayer != null && sfxName != "") audioPlayer.playSFXAtPosition(sfxName, position, parent);
		}
			
	}

	public static void PlayMusic(string musicName){
		if (AudioPlayer.Instance)
        {
			if (audioPlayer != null) audioPlayer.playMusic(musicName);
		}
			
	}
}
