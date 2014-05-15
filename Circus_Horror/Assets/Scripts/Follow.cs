using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour 
{
	public AudioClip[] sounds;
	public Transform target;

	public Transform firstSpawn;
	public Transform startPosition;

	public Transform forestStartPosition;
	public Transform forestEndPosition;

	public Light slenderLight;

	[System.NonSerialized]
	public bool soundIsPlaying = false;

	[System.NonSerialized]
	public bool canChase = true;

	[System.NonSerialized]
	public bool forestTrigger = false;

	public int timeUntilSoundPlays = 30;
	public int soundDistance = 10;
	public int distanceForSoundReset = 20;
	public int timeUntilStartChase = 30;

	public Transform[] idleTargets;

	ManagerScript managerScript;

	bool billyHasPlayed = false;
	float distance;

	Animator anim;
	NavMeshAgent agent;

	float timer = 0;
	float startTimer = 0;
	bool chasingStarted = false;
	bool needNewPosition = true;

	bool forestRun = false;
	bool hasForestDestination = false;
	// Use this for initialization
	void Start () 
	{
		agent = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
		anim.SetBool("ChasingPlayer", false);
		managerScript = GameObject.Find("GameManager").GetComponent<ManagerScript>();
		slenderLight.enabled = false;
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

			if(distance < 15)
			{
				agent.speed = 4;
				anim.SetBool("CloseToPlayer", true);
			}
			else
			{
				agent.speed = 1;
				anim.SetBool("CloseToPlayer", false);
			}

			if(distance < 1.5f)
			{
				Application.LoadLevel("MainMenu");
			}
			agent.destination = target.position;
		}
		else
		{
			if(forestTrigger)
			{
				if(!forestRun)
				{
					Debug.Log("StartingForestRun");
					forestRun = true;
					agent.enabled = false;
					transform.position = forestStartPosition.position;
					agent.enabled = true;
					agent.destination = forestEndPosition.position;
					agent.speed = 8;
					anim.SetBool("ChasingPlayer", true);
					anim.SetBool("CloseToPlayer", true);

					slenderLight.enabled = true;

					StartCoroutine(GetDestination());
				}


				if(hasForestDestination && agent.remainingDistance < 4)
				{
					Debug.Log("made forest destination");

					agent.enabled = false;
					transform.position = startPosition.position;
					agent.speed = 1;
					StartCoroutine(SetAnimBackToStart());
					slenderLight.enabled = false;
					forestRun = false;
					forestTrigger = false;
				}
			}



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
					agent.enabled = true;
					slenderLight.enabled = false;
					chasingStarted = true;
				}
			}
		}
	}

	IEnumerator GetDestination ()
	{
		yield return new WaitForSeconds(1);
		hasForestDestination = true;
	}

	IEnumerator SetAnimBackToStart ()
	{
		anim.SetBool("CloseToPlayer", false);
		yield return new WaitForSeconds(2);
		anim.SetBool("ChasingPlayer", false);
		agent.speed = 1;
	}
}
