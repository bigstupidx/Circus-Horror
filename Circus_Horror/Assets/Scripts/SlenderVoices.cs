using UnityEngine;
using System.Collections;

public class SlenderVoices : MonoBehaviour 
{

	public AudioClip[] breathing;
	public AudioClip[] candleOff;
	public AudioClip[] found;
	public AudioClip[] teasing;

	bool playedFirstSound = false;

	public void PlayFile (string fileName)
	{
		audio.Stop();
		AudioClip clipToPlay = Resources.Load(fileName) as AudioClip;
		audio.PlayOneShot(clipToPlay);

		if(!playedFirstSound)
		{
			playedFirstSound = true;
			StartCoroutine(ChangeMaxDistance());
		}
	}

	public void PlayCandleOff ()
	{
		audio.Stop();
		int soundNumber = Random.Range(0, candleOff.Length);
		audio.PlayOneShot(candleOff[soundNumber]);
	}

	public void PlayFound ()
	{
		audio.Stop();
		int soundNumber = Random.Range(0, found.Length);
		audio.PlayOneShot(found[soundNumber]);
	}

	public void PlayTeasing ()
	{
		audio.Stop();
		int soundNumber = Random.Range(0, teasing.Length);
		audio.PlayOneShot(teasing[soundNumber]);
	}

	public void StopvoiceSource ()
	{
		audio.Stop();
	}

	IEnumerator ChangeMaxDistance ()
	{
		yield return new WaitForSeconds(3);
		audio.maxDistance = 400;
	}
}
