using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public AudioClip[] backgroundMusic;

	public Texture clownPicture;
	public Texture black;
	public float minTimeToNextImage = 3;
	public float maxTimeToNextImage = 15;

	public float minTimeToNextVoice = 8;
	public float maxTimeToNextVoice = 16;

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

	float scaredTimer = 0;
	float timeToNextScaredVoice;

	bool startedToBlackout = false;
	float blackAlphaStart = 0.0f;
	float currentAlpha;

	CandleScript candleScript;
	vp_DoorInteractable doorScript;
	EndCameraScript endScript;
	Follow followScritp;
	VoiceScript voiceScript;

	// Use this for initialization
	void Start () 
	{
		candleScript = GameObject.Find("Arm").GetComponent<CandleScript>();
		doorScript = GameObject.Find("CabinDoorTrigger").GetComponent<vp_DoorInteractable>();
		endScript = GameObject.Find("EndCamera").GetComponent<EndCameraScript>();
		followScritp = GameObject.Find("SlenderMan").GetComponent<Follow>();
		voiceScript = GameObject.Find("Candle").GetComponent<VoiceScript>();

		timeToNextImage = Random.Range(minTimeToNextImage, maxTimeToNextImage);
		timeToNextScaredVoice = Random.Range(minTimeToNextVoice, maxTimeToNextVoice);

		audio.loop = true;
		audio.clip = backgroundMusic[0];
		audio.Play();
		currentAlpha = blackAlphaStart;
		objective = "Find the candle";
	}
	
	// Update is called once per frame
	void Update () 
	{


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

			if(scaredTimer < timeToNextScaredVoice)
			{
				scaredTimer += Time.deltaTime;
			}
			else
			{
				scaredTimer = 0;
				timeToNextScaredVoice = Random.Range(minTimeToNextVoice, maxTimeToNextVoice);
				voiceScript.PlayDarkness();
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
		if(audio.isPlaying)
		{
			audio.Stop();
			audio.clip = backgroundMusic[musicNr];
			if(musicNr == 3)
			{
				audio.volume -= 0.1f;
			}
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
	}

	void OnTriggerEnter (Collider other)
	{
		if(other.tag == "CandlePickup")
		{
			doorScript.cabinDoorUnlocked = true;
		}
	}
}
