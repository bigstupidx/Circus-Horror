using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour 
{
	public AudioClip[] sounds;
	public Transform target;

	[System.NonSerialized]
	public bool soundIsPlaying = false;

	[System.NonSerialized]
	public bool canChase = true;
	public int timeUntilSoundPlays = 30;
	public int soundDistance = 10;
	public int distanceForSoundReset = 20;
	public int timeUntilStartChase = 30;

	bool billyHasPlayed = false;
	float distance;

	Animator anim;


	float timer = 0;
	float startTimer = 0;
	bool chasingStarted = false;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
		anim.SetBool("ChasingPlayer", false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(chasingStarted && canChase)
		{
			anim.SetBool("ChasingPlayer", true);
			distance = Vector3.Distance(transform.position, target.position);
			if(!soundIsPlaying)
			{
				if(timer < timeUntilSoundPlays)
				{
					timer += Time.deltaTime;
				}
				else
				{
					timer = 0;
					int randomNumber = Random.Range(0, sounds.Length -1);
					audio.PlayOneShot(sounds[randomNumber]);
				}
				
				if(distance < soundDistance && !billyHasPlayed)
				{
					billyHasPlayed = true;
					audio.PlayOneShot(sounds[3]);
				}
				else if(distance > distanceForSoundReset)
				{
					billyHasPlayed = false;
				}
			}
			else
			{
				Debug.Log("sound is playing");
				timer = 0;
				soundIsPlaying = false;
			}
			
			GetComponent<NavMeshAgent>().destination = target.position;
		}
		else
		{
			anim.SetBool("ChasingPlayer", false);
			GetComponent<NavMeshAgent>().destination = transform.position;
			if(startTimer < timeUntilStartChase)
			{
				startTimer += Time.deltaTime;
			}
			else
			{
				chasingStarted = true;
			}
		}
	}
}
