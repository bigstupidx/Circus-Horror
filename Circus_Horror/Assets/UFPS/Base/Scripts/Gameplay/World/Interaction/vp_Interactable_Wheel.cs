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

public class vp_Interactable_Wheel : vp_Interactable
{
	public Light light1;

	public GameObject wheel;

	bool hasStarted = true;

	bool propMoving = true;
	
	protected override void Start()
	{
		light1.enabled = false;
	}
	
	
	/// <summary>
	/// This should be overriden and starts the interaction
	/// </param>
	public override bool TryInteract(vp_FPPlayerEventHandler player)
	{
		bool interacting = false;

		if(m_Player == null)
			m_Player = player;
		if(propMoving)
		{
			hasStarted = !hasStarted;
			TriggerCarouselMovement();
			interacting = true;
		}

		return interacting;
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

		m_Player = col.gameObject.GetComponent<vp_FPPlayerEventHandler>();

		if (m_Player == null)
			return;
		
		// calls the TryInteract method which is hopefully on the inherited class
		TryInteract(m_Player);
		
	}

	void TriggerCarouselMovement ()
	{
		if(!hasStarted)
		{
			iTween.RotateBy(wheel, iTween.Hash("z", -1, "time", 40, "easetype", "linear", "looptype", "loop"));
			light1.enabled = true;
	
		}
		else
		{
			iTween.Stop(wheel);
			light1.enabled = false;

		}
	}
}