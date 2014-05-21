﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public AudioClip[] backgroundMusic;

	public Texture clownPicture;
	public Texture black;
	public float minTimeToNextImage = 3;
	public float maxTimeToNextImage = 15;

	public float maxTimeUntilDark = 90;

	protected vp_FPPlayerEventHandler m_Player;
	//float maxMusicVolume = 0.4f;

	//bool fadeIn = false;
	//bool fadeOut = false;
	bool showImage = false;

	bool showObjective = false;

	string objective;

	//int newMusicNr;

	float imageTimer = 0;
	float timeToNextImage;

	bool startedToBlackout = false;
	float blackAlphaStart = 0.0f;
	float currentAlpha;

	CandleScript candleScript;
	vp_DoorInteractable doorScript;
	EndCameraScript endScript;
	Follow followScritp;
	// Use this for initialization
	void Start () 
	{
		candleScript = GameObject.Find("Arm").GetComponent<CandleScript>();
		doorScript = GameObject.Find("CabinDoorTrigger").GetComponent<vp_DoorInteractable>();
		endScript = GameObject.Find("EndCamera").GetComponent<EndCameraScript>();
		followScritp = GameObject.Find("SlenderMan").GetComponent<Follow>();
		timeToNextImage = Random.Range(minTimeToNextImage, maxTimeToNextImage);
		audio.loop = true;
		audio.clip = backgroundMusic[0];
		audio.Play();
		currentAlpha = blackAlphaStart;
		objective = "Find the candle";
	}
	
	// Update is called once per frame
	void Update () 
	{
		/*if(fadeOut)
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
		}*/

		if(Input.GetKeyDown(KeyCode.O))
		{
			showObjective = !showObjective;
		}

		if(candleScript.canShowClownImage)
		{
			if(!startedToBlackout)
			{
				startedToBlackout = true;
				iTween.ValueTo(gameObject, iTween.Hash("from", blackAlphaStart, "to", 0.6f, "time", maxTimeUntilDark, "onupdate", "UpdateBlack", "oncomplete", "GameOver"));

			}
			if(imageTimer < timeToNextImage)
			{
				imageTimer += Time.deltaTime;
			}
			else
			{
				imageTimer = 0;
				timeToNextImage = Random.Range(minTimeToNextImage, maxTimeToNextImage);
				StartCoroutine(ShowImage());
			}


		}
		else
		{
			if(startedToBlackout)
			{
				startedToBlackout = false;
				iTween.Stop ();
				currentAlpha = blackAlphaStart;
			}
		}
	}

	public void changeMusic (int musicNr)
	{
		//newMusicNr = musicNr;

		if(audio.isPlaying)
		{
			audio.Stop();
			audio.clip = backgroundMusic[musicNr];
			audio.Play();
		}
	}

	void OnEnable()
	{
		
		// allow this monobehaviour to talk to the player event handler
		if (m_Player != null)
			m_Player.Register(this);
		
	}
	
	
	/// <summary>
	/// unregisters this component from the event handler (if any)
	/// </summary>
	void OnDisable()
	{
		
		// unregister this monobehaviour from the player event handler
		if (m_Player != null)
			m_Player.Unregister(this);
		
	}

	void OnGUI ()
	{
		if(showObjective)
		{
			GUI.Label(new Rect(20,20, 200, 60), objective);
		}
		GUI.color = new Color(1.0f, 1.0f, 1.0f, currentAlpha);
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), black);

		if(showImage)
		{
			GUI.color = new Color(1.0f, 1.0f, 1.0f, currentAlpha + 0.1f);
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), clownPicture);
		}
	}

	IEnumerator ShowImage ()
	{
		showImage = true;
		yield return new WaitForSeconds(0.1f);
		showImage = false;
	}

	void UpdateBlack (float blackness)
	{
		currentAlpha = blackness;
	}

	void GameOver ()
	{
		followScritp.canChase = false;
		endScript.gameOver = true;
		//Application.LoadLevel("MainMenu");
	}

	void OnTriggerEnter (Collider other)
	{
		if(other.tag == "CandlePickup")
		{
			doorScript.cabinDoorUnlocked = true;
		}
	}
}
