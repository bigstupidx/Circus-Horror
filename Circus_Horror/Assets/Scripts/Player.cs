using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public AudioClip[] backgroundMusic;

	float maxMusicVolume = 0.4f;
	bool fadeIn = false;
	bool fadeOut = false;
	int newMusicNr;

	// Use this for initialization
	void Start () 
	{
		audio.loop = true;
		audio.clip = backgroundMusic[0];
		audio.Play();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(fadeOut)
		{
			if(audio.volume > 0)
			{
				audio.volume -= 0.2f * Time.deltaTime;
			}
			else
			{
				audio.Stop();
				audio.clip = backgroundMusic[newMusicNr];
				audio.Play();
				fadeOut = false;
				fadeIn = true;
			}
		}
		if(fadeIn)
		{
			if( audio.volume < maxMusicVolume)
			{
				audio.volume += 0.2f * Time.deltaTime;
			}
			else
			{
				fadeIn = false;
			}
		}
	}

	public void changeMusic (int musicNr)
	{
		newMusicNr = musicNr;

		if(audio.isPlaying)
		{
			fadeOut = true;
		}
	}
}
