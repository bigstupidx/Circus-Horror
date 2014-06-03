/////////////////////////////////////////////////////////////////////////////////
//
//	vp_Interactable.cs
//	© VisionPunk. All Rights Reserved.
//	https://twitter.com/VisionPunk
//	http://www.visionpunk.com
//
//	description:	a generic interact base class which can be inherited to create
//					various ways of interacting with objects. An interactable can
//					be of two types, vp_InteractType.Normal or vp_InteractType.trigger.
//					Normal interactables require input for interaction whereas trigger 
//					interactables are trigger by the character controller on the player.
//					Typically Normal interactables require the vp_FPInteract manager
//					to fire while the trigger interactables do not.
//					
//					NOTES:
//					This script can not be added to a gameobject directly.
//					instead, you must create a class derived from this one, with
//					an overridden 'TryInteract' method in it, and add that script instead
//
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class vp_Interactable_Canon : vp_Interactable
{
	bool gotCanonball = false;
	bool gotKeg = false;
	bool gotFuse = false;

	string message;

	public GameObject canonCollider;

	bool hasShot = false;
	public bool isShooting = false;

	protected override void Start()
	{

	}


	
	/// <summary>
	/// This should be overriden and starts the interaction
	/// </param>
	public override bool TryInteract(vp_FPPlayerEventHandler player)
	{
		bool interacting = false;
		if(m_Player == null)
			m_Player = player;

		//if(gotCanonball && gotKeg && gotFuse && !hasShot)
		if( !hasShot)
		{
			hasShot = true;
			isShooting = true;
			interacting = true;
			canonCollider.collider.enabled = false;
			//canonballScript.Shoot();
		}
		else
		{
			interacting = true;
			CheckForItems ();

			m_Player.HUDText.Send(message);
		}

		return interacting;
	}

	public void PlayCanonAudio ()
	{
		audio.Play();
	}
	
	
	/// <summary>
	/// this is triggered when an object enters the collider and
	/// InteractType is set to trigger
	/// </summary>
	protected override void OnTriggerEnter(Collider col)
	{
		
		/*// only do something if the trigger is of type Trigger
		if (InteractType != vp_InteractType.Trigger)
			return;*/

		if(col.tag == "CanonballPickup")
		{
			Debug.Log("canon ball");
			gotCanonball = true;
			Destroy(col.gameObject);
		}

		if(col.tag == "PowderPickup")
		{
			Debug.Log("powder");
			gotKeg = true;
			Destroy(col.gameObject);
		}

		if(col.tag == "FusePickup")
		{
			Debug.Log("fuse");
			gotFuse = true;
			Destroy(col.gameObject);
		}
		// see if the colliding object was a valid recipient
		foreach(string s in RecipientTags)
		{
			if(col.gameObject.tag == s)
				goto isRecipient;
		}
		return;
		isRecipient:

		m_Player = col.gameObject.GetComponent<vp_FPPlayerEventHandler>();

		if (m_Player == null)
			return;
		
		// calls the TryInteract method which is hopefully on the inherited class
		TryInteract(m_Player);
		
	}

	void CheckForItems ()
	{
		message = "You need the ";

		if(!gotCanonball)
		{
			string canonMessage = "Canon Ball ";
			message += canonMessage;
		}

		if(!gotKeg)
		{
			string kegMessage = "Gun Powder ";
			message += kegMessage;
		}

		if(!gotFuse)
		{
			string fuseMessage = "Fuse ";
			message += fuseMessage;
		}

		message += "to fire the canon";
	}
}