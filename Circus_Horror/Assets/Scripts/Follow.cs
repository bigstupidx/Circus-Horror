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

	public bool secondSlenderTriggered = false;

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
	public Transform[] secondIdleTargets;

	ManagerScript managerScript;
	EndCameraScript endScript;
	VoiceScript voiceScript;
	SlenderVoices slenderVoiceScript;

	bool billyHasPlayed = false;
	bool runHasPlayed = false;
	float distance;

	Animator anim;
	NavMeshAgent agent;

	float teleportTimer = 0;
	float teleportTime = 20;
	bool canTeleport = true;

	float timer = 0;
	float startTimer = 0;
	public bool chasingStarted = false;
	bool needNewPosition = true;

	public bool secondArea = false;

	float defaultAgentSpeed = 1.3f;

	bool forestRun = false;
	bool hasForestDestination = false;

	bool startedSecondArea = false;
	Transform secondSpawn;

	// Use this for initialization
	void Start () 
	{
		agent = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
		anim.SetBool("ChasingPlayer", false);
		managerScript = GameObject.Find("GameManager").GetComponent<ManagerScript>();
		endScript = GameObject.Find("EndCamera").GetComponent<EndCameraScript>();
		voiceScript = GameObject.Find("PlayerCamera").GetComponent<VoiceScript>();
		slenderVoiceScript = GetComponent<SlenderVoices>();
		slenderLight.enabled = false;
		agent.speed = defaultAgentSpeed;
	}
	
	// Update is called once per frame
	void Update () 
	{
		distance = Vector3.Distance(transform.position, target.position);


		if(secondArea && chasingStarted)
		{
			if(distance > 75 && canTeleport)
			{
				canTeleport = false;
				float shortestDistance = 500;
				float positionDistance;
				Vector3 closestPosition = transform.position;
				for(int i = 0; i < secondIdleTargets.Length; i++)
				{
					positionDistance = Vector3.Distance(target.position, secondIdleTargets[i].position);
					if(positionDistance < shortestDistance)
					{
						shortestDistance = positionDistance;
						closestPosition = secondIdleTargets[i].position;
					}
				}

				if(closestPosition != transform.position)
				{
					agent.enabled = false;
					transform.position = closestPosition;
					agent.enabled = true;
				}

			}
		}

		if(!canTeleport)
		{
			if(teleportTimer < teleportTime)
			{
				teleportTimer += Time.deltaTime;
			}
			else
			{
				timer = 0;
				canTeleport = true;
			}
		}

		if(chasingStarted && canChase)
		{
			anim.SetBool("ChasingPlayer", true);

			if(!soundIsPlaying)
			{
				if(timer < timeUntilSoundPlays)
				{
					timer += Time.deltaTime;
				}
				else
				{

					timer = 0;
					slenderVoiceScript.PlayTeasing();
					timeUntilSoundPlays = Random.Range(10, 30);
				}

				if(distance < 15 && !runHasPlayed)
				{
					runHasPlayed = true;
					voiceScript.PlayFile("Hes Coming RUN 2");
				}
				
				if(distance < soundDistance && !billyHasPlayed)
				{
					billyHasPlayed = true;
					slenderVoiceScript.PlayFound();
				}
				else if(distance > distanceForSoundReset)
				{
					billyHasPlayed = false;
					runHasPlayed = false;
				}
			}
			else
			{
				Debug.Log("sound is playing");
				timer = 0;
				soundIsPlaying = false;
			}

			if(distance < 10)
			{
				agent.speed = 2;
				anim.SetBool("CloseToPlayer", true);
			}
			else
			{
				agent.speed = defaultAgentSpeed;
				anim.SetBool("CloseToPlayer", false);
			}

			if(distance < 1.5f)
			{
				canChase = false;
				endScript.gameOver = true;
				voiceScript.PlayFile("Scream");
			}
			if(agent.enabled)
			{
				agent.destination = target.position;
			}

		}
		else
		{
			if(forestTrigger)
			{
				if(!forestRun)
				{
					forestRun = true;
					agent.enabled = false;
					transform.position = forestStartPosition.position;
					agent.enabled = true;
					agent.destination = forestEndPosition.position;
					agent.speed = 6;
					anim.SetBool("ChasingPlayer", true);
					anim.SetBool("CloseToPlayer", true);

					//slenderLight.enabled = true;

					StartCoroutine(GetDestination());
				}


				if(hasForestDestination && agent.remainingDistance < 4)
				{
					Debug.Log("made forest destination");

					agent.enabled = false;
					transform.position = startPosition.position;
					agent.speed = defaultAgentSpeed;
					StartCoroutine(SetAnimBackToStart());
					//slenderLight.enabled = false;
					forestRun = false;
					forestTrigger = false;
				}
			}




			if(chasingStarted)
			{
				agent.speed = defaultAgentSpeed;
				anim.SetBool("CloseToPlayer", false);
				                     
				if(needNewPosition)
				{
					needNewPosition = false;
					if(secondArea)
					{
						int randomNr = Random.Range(0, secondIdleTargets.Length);
						agent.destination = secondIdleTargets[randomNr].position;
					}
					else
					{
						int randomNr = Random.Range (0, idleTargets.Length);
						agent.destination = idleTargets[randomNr].position;
					}

				}
				else
				{
					if(agent.remainingDistance < 5)
					{
						needNewPosition = true;
					}
				}

				if(distance < 1.2f)
				{
					canChase = false;
					endScript.gameOver = true;
					voiceScript.PlayFile("Scream");
				}
			}


			if(managerScript.slenderActive && !chasingStarted)
			{
				if(secondArea)
				{
					if(!startedSecondArea)
					{
						anim.SetBool("ChasingPlayer", false);
						startedSecondArea = true;
						canChase = true;
						agent.enabled = false;
						transform.position = secondSpawn.position;
						startTimer = 0;
					}
				}
				else
				{
					transform.position = firstSpawn.position;
				}

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

	public void StopChase ()
	{
		canChase = false;
		chasingStarted = false;
		managerScript.slenderActive = false;
		agent.enabled = false;

	}

	public void SecondTrigger (Transform newSpawn)
	{
		secondSpawn = newSpawn;
		secondArea = true;
	}

	public void PauseAgent ()
	{
		agent.enabled = false;
	}

	public void ResumeAgent ()
	{
		agent.enabled = true;
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
		agent.speed = defaultAgentSpeed;
	}
}
