/////////////////////////////////////////////////////////////////////////////////
//
//	vp_Switch.cs
//	© VisionPunk. All Rights Reserved.
//	https://twitter.com/VisionPunk
//	http://www.visionpunk.com
//
//	description:	This class will allow the player to interact with an object
//					in the world by input or by a trigger. The script takes a target
//					object and a message can be sent to that target object.
//
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class vp_DoorInteractable : vp_Interactable
{
	
	//public GameObject Target = null;
	public string TargetMessage = "";
	public AudioSource audioSource = null;
	public Vector2 SwitchPitchRange = new Vector2(1.0f, 1.5f);
	public List<AudioClip> SwitchSounds = new List<AudioClip>();		// list of sounds to randomly play when switched

	public GameObject leftDoor;
	public GameObject rightDoor;
	public GameObject mazeBarrier;

	public bool canBeLocked;
	public bool cabinDoor;
	[System.NonSerialized]
	public bool cabinDoorUnlocked = true;

	public bool endDoor = false;

	public AudioClip doorSound;

	bool doorOpen = false;
	bool doorIsMoving = false;
	public bool unlocked = true;

	bool slenderTriggered = false;

	bool gateFound = false;

	ManagerScript managerScript;
	vp_DoorInteractable doorScript;
	Player playerScript;
	CandleScript candleScript;
	SlenderVoices slenderVoiceScript;
	VoiceScript voiceScript;

	protected override void Start()
	{
		managerScript = GameObject.Find("GameManager").GetComponent<ManagerScript>();
		doorScript = GameObject.Find("FirstDoorTrigger").GetComponent<vp_DoorInteractable>();
		playerScript = GameObject.Find("PlayerCamera").GetComponent<Player>();
		slenderVoiceScript = GameObject.Find("SlenderMan").GetComponent<SlenderVoices>();
		voiceScript = GameObject.Find("PlayerCamera").GetComponent<VoiceScript>();

		if(canBeLocked)
		{
			unlocked = false;
		}

		if(cabinDoor)
		{
			cabinDoorUnlocked = false;
		}

		base.Start();
		
		if(audioSource == null)
			audioSource = audio == null ? gameObject.AddComponent<AudioSource>() : audio;
		audioSource.volume = 0.1f;
	}
	
	
	/// <summary>
	/// try to interact with this object
	/// </summary>
	public override bool TryInteract(vp_FPPlayerEventHandler player)
	{
		
		/*if(Target == null)
			return false;*/
		
		if(m_Player == null)
			m_Player = player;


		
		PlaySound();

		DoorOpening ();

		/*Target.SendMessage(TargetMessage, SendMessageOptions.DontRequireReceiver);*/
		
		return true;
		
	}


	/// <summary>
	/// 
	/// </summary>
	public virtual void PlaySound()
	{
		
		if(audioSource == null)
			return;
		
		if( SwitchSounds.Count == 0 )
			return;
		
		AudioClip soundToPlay = SwitchSounds[ Random.Range( 0, SwitchSounds.Count ) ];

		if(soundToPlay == null)
			return;
		
		audioSource.pitch = Random.Range(SwitchPitchRange.x, SwitchPitchRange.y);
		audioSource.PlayOneShot( soundToPlay );
		
	}
	
	
	/// <summary>
	/// this is triggered when an object enters the collider and
	/// InteractType is set to trigger
	/// </summary>
	protected override void OnTriggerEnter(Collider col)
	{
		// only do something if the trigger is of type Trigger
		if (InteractType != vp_InteractType.Trigger)
			return;

		// see if the colliding object was a valid recipient
		foreach(string s in RecipientTags)
		{
			if(col.gameObject.tag == s)
				goto isRecipient;
		}
		return;
		isRecipient:

		if (m_Player == null)
			m_Player = GameObject.FindObjectOfType(typeof(vp_FPPlayerEventHandler)) as vp_FPPlayerEventHandler;
		
		// calls the TryInteract method which is hopefully on the inherited class
		TryInteract(m_Player);
	}

	void DoorOpening ()
	{
		if (m_Player == null)
			m_Player = GameObject.FindObjectOfType(typeof(vp_FPPlayerEventHandler)) as vp_FPPlayerEventHandler;

		if(endDoor)
		{
			if(endDoor)
			{
				if(!gateFound)
				{
					gateFound = true;
					voiceScript.repeatVoice = false;
				}
				m_Player.HUDText.Send("The Door is Locked. Maybe you can break it with something");
				voiceScript.PlayFile("Cannon will open");
			}
			return;
		}
		if(!doorOpen && !doorIsMoving && unlocked && cabinDoorUnlocked)
		{
			audio.PlayOneShot(doorSound);
			this.collider.enabled = false;
			doorOpen = true;
			doorIsMoving = true;
			if(leftDoor != null)
			{
				iTween.RotateBy(leftDoor, iTween.Hash("y", -0.25, "time", 1, "easetype", "linear", "oncomplete", "FinishedOpening"));
			}
			if(rightDoor != null)
			{
				iTween.RotateBy(rightDoor, iTween.Hash("y", 0.25, "time", 1, "easetype", "linear"));
			}

		}

		if(cabinDoor && !cabinDoorUnlocked)
		{
			m_Player.HUDText.Send("It's too dark outside. You need to get a candle");

			voiceScript.motherVoiceSegmentSingle = "Get the candle 2";
			voiceScript.PlayMotherVoiceSingle();
		}

		if(!unlocked)
		{
			m_Player.HUDText.Send("You need the key to unlock this door");
			StartCoroutine(KeyVoice());
			if(!slenderTriggered)
			{
				StartCoroutine(GetOutVoice());
				doorScript.CloseDoors ();
				slenderTriggered = true;
				Destroy(mazeBarrier);
				managerScript.slenderActive = true;
			}
		}
	}
	
	void FinishedOpening ()
	{
		doorIsMoving = false;
	}

	public void CloseDoors ()
	{
		doorIsMoving = true;
		audio.Play();
		playerScript.changeMusic(2);
		if(leftDoor != null)
		{
			iTween.RotateBy(leftDoor, iTween.Hash("y", 0.25, "time", 1, "easetype", "linear", "oncomplete", "FinishedOpening"));
		}
		if(rightDoor != null)
		{
			iTween.RotateBy(rightDoor, iTween.Hash("y", -0.25, "time", 1, "easetype", "linear"));
		}
	}

	public void CloseCabinDoor ()
	{
		iTween.RotateBy(rightDoor, iTween.Hash("y", -0.25, "time", 1, "easetype", "linear"));
		candleScript = GameObject.Find("Arm").GetComponent<CandleScript>();
		candleScript.CandleTrigger();
		m_Player.HUDText.Send("The dark is scary, you can re-lite the candle at a fire");
		StartCoroutine(CandleVoice());
	}

	IEnumerator GetOutVoice ()
	{
		yield return new WaitForSeconds(0.5f);
		slenderVoiceScript.PlayFile("Howdidyougetout2");
	}

	IEnumerator CandleVoice()
	{
		yield return new WaitForSeconds(3);
		if(candleScript.canShowClownImage)
		{
			voiceScript.motherVoiceSegmentSingle = "Light the candle with 1";
			voiceScript.PlayMotherVoiceSingle();
		}

	}

	IEnumerator KeyVoice ()
	{
		yield return new WaitForSeconds(5);
		voiceScript.motherVoiceSegmentRepeat = "Find the key 2";
		voiceScript.PlayMotherVoiceRepeat();
		yield return new WaitForSeconds(15);
		voiceScript.motherVoiceSegmentSingle = "He can see the candle 2";
		voiceScript.PlayMotherVoiceSingle();
		m_Player.HUDText.Send("Press F to put the candle out \n He will stop chasing you if he can't see you");
	}

}
