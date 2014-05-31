using UnityEngine;
using System.Collections;

public class VoiceScript : MonoBehaviour 
{
	public AudioClip[] breathing;
	public AudioClip[] darkness;
	public AudioClip[] scared;
	public AudioClip[] blowOut;
	// Use this for initialization

	public void PlayBreathing ()
	{
		audio.Stop();
		int soundNumber = Random.Range(0, breathing.Length);
		audio.PlayOneShot(breathing[soundNumber]);
	}

	public void PlayDarkness ()
	{
		audio.Stop();
		int soundNumber = Random.Range(0, darkness.Length);
		audio.PlayOneShot(darkness[soundNumber]);
	}

	public void PlayScared ()
	{
		audio.Stop();
		int soundNumber = Random.Range(0, scared.Length);
		audio.PlayOneShot(scared[soundNumber]);
	}

	public void PlayBlowOut ()
	{
		audio.Stop();
		int soundNumber = Random.Range(0, blowOut.Length);
		audio.PlayOneShot(blowOut[soundNumber]);
	}

	public void PlayFile (string fileName)
	{
		audio.Stop();
		AudioClip clipToPlay = Resources.Load(fileName) as AudioClip;
		audio.PlayOneShot(clipToPlay);
	}

	public void StopAudio ()
	{
		audio.Stop();
	}
}
