using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public AudioClip[] backgroundMusic;

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

	}
}
