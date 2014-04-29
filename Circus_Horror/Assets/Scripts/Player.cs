using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public AudioClip[] backgroundMusic;

	public Texture clownPicture;
	public Texture black;
	public float minTimeToNextImage = 3;
	public float maxTimeToNextImage = 15;
	float maxMusicVolume = 0.4f;

	bool fadeIn = false;
	bool fadeOut = false;
	bool showImage = false;

	int newMusicNr;

	float imageTimer = 0;
	float timeToNextImage;

	bool startedToBlackout = false;
	float blackAlphaStart = 0.0f;

	CandleScript candleScript;
	// Use this for initialization
	void Start () 
	{
		candleScript = GameObject.Find("Arm").GetComponent<CandleScript>();
		timeToNextImage = Random.Range(minTimeToNextImage, maxTimeToNextImage);
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

		if(candleScript.canShowClownImage)
		{
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

	void OnGUI ()
	{
		if(showImage)
		{
			GUI.color = new Color(1.0f, 1.0f, 1.0f, 0.1f);
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), clownPicture);
		}

		GUI.color = new Color(1.0f, 1.0f, 1.0f, blackAlpha);
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), black);
	}

	IEnumerator ShowImage ()
	{
		showImage = true;
		yield return new WaitForSeconds(0.1f);
		showImage = false;
	}
}
