using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour 
{
	public AudioClip[] sounds;
	public Transform target;

	public Transform firstSpawn;

	public Light slenderLight;

	[System.NonSerialized]
	public bool soundIsPlaying = false;

	[System.NonSerialized]
	public bool canChase = true;
	public int timeUntilSoundPlays = 30;
	public int soundDistance = 10;
	public int distanceForSoundReset = 20;
	public int timeUntilStartChase = 30;

	public Transform[] idleTargets;

	ManagerScript managerScript;

	bool billyHasPlayed = false;
	float distance;

	Animator anim;


	float timer = 0;
	float startTimer = 0;
	bool chasingStarted = false;
	bool needNewPosition = true;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
		anim.SetBool("ChasingPlayer", false);
		managerScript = GameObject.Find("GameManager").GetComponent<ManagerScript>();
		slenderLight.enabled = false;
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

			if(distance < 1.5f)
			{
				Application.LoadLevel("MainMenu");
			}
			GetComponent<NavMeshAgent>().destination = target.position;
		}
		else
		{
			if(chasingStarted)
			{
				if(needNewPosition)
				{
					needNewPosition = false;
					int randomNr = Random.Range (0, idleTargets.Length);
					GetComponent<NavMeshAgent>().destination = idleTargets[randomNr].position;
				}
				else
				{
					if(GetComponent<NavMeshAgent>().remainingDistance < 5)
					{
						needNewPosition = true;
					}
				}
			}


			if(managerScript.slenderActive && !chasingStarted)
			{
				transform.position = firstSpawn.position;
				slenderLight.enabled = true;
				if(startTimer < timeUntilStartChase)
				{
					startTimer += Time.deltaTime;
				}
				else
				{
					slenderLight.enabled = false;
					chasingStarted = true;
				}
			}
		}
	}
}
