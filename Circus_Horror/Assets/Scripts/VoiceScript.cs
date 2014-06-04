using UnityEngine;
using System.Collections;

public class VoiceScript : MonoBehaviour 
{
	public AudioClip[] breathing;
	public AudioClip[] darkness;
	public AudioClip[] scared;
	public AudioClip[] blowOut;

	public string motherVoiceSegmentSingle;
	public string motherVoiceSegmentRepeat;

	public bool repeatVoice = false;

	public float repeatTime = 30;
	float repeatTimer = 0;

	AudioSource voiceSource;
	// Use this for initialization

	void Update ()
	{
		if(repeatVoice)
		{
			if(repeatTimer < repeatTime)
			{
				repeatTimer += Time.deltaTime;
			}
			else
			{
				repeatTimer = 0;
				PlayMotherVoiceRepeat();
			}
		}
		else
		{
			repeatTimer = 0;
		}
	}

	void Start ()
	{
		AudioSource[] aSource = GetComponents<AudioSource>();
		voiceSource = aSource[1];
	}

	public void PlayBreathing ()
	{
		voiceSource.Stop();
		int soundNumber = Random.Range(0, breathing.Length);
		voiceSource.PlayOneShot(breathing[soundNumber]);
	}

	public void PlayDarkness ()
	{
		voiceSource.Stop();
		int soundNumber = Random.Range(0, darkness.Length);
		voiceSource.PlayOneShot(darkness[soundNumber]);
	}

	public void PlayScared ()
	{
		voiceSource.Stop();
		int soundNumber = Random.Range(0, scared.Length);
		voiceSource.PlayOneShot(scared[soundNumber]);
	}

	public void PlayBlowOut ()
	{
		voiceSource.Stop();
		int soundNumber = Random.Range(0, blowOut.Length);
		voiceSource.PlayOneShot(blowOut[soundNumber]);
	}

	public void PlayFile (string voice)
	{
		voiceSource.Stop();
		AudioClip clipToPlay = Resources.Load(voice) as AudioClip;
		voiceSource.PlayOneShot(clipToPlay);
	}

	public void PlayMotherVoiceRepeat ()
	{
		if(motherVoiceSegmentRepeat != null)
		{
			repeatVoice = true;
			voiceSource.Stop();
			AudioClip voiceClip = Resources.Load(motherVoiceSegmentRepeat) as AudioClip;
			voiceSource.PlayOneShot(voiceClip);
		}

	}

	public void PlayMotherVoiceSingle ()
	{
		if(motherVoiceSegmentSingle != null)
		{
			voiceSource.Stop();
			AudioClip voiceClip = Resources.Load(motherVoiceSegmentSingle) as AudioClip;
			voiceSource.PlayOneShot(voiceClip);
		}
		
	}

	public void setVolume (float volume)
	{
		voiceSource.volume = volume;
	}

	public void StopAudioSource ()
	{
		voiceSource.Stop();
	}
}
