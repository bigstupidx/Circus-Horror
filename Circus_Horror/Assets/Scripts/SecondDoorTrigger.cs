using UnityEngine;
using System.Collections;

public class SecondDoorTrigger : MonoBehaviour 
{
	vp_DoorInteractable doorScript;
	Follow followScript;


	protected vp_FPPlayerEventHandler m_Player;

	void Start ()
	{
		doorScript = GameObject.Find("SecondDoorTrigger").GetComponent<vp_DoorInteractable>();
		followScript = GameObject.Find("SlenderMan").GetComponent<Follow>();
	}

	protected void OnEnable()
	{
		
		// allow this monobehaviour to talk to the player event handler
		if (m_Player != null)
			m_Player.Register(this);
		
	}
	
	
	/// <summary>
	/// unregisters this component from the event handler (if any)
	/// </summary>
	protected void OnDisable()
	{
		
		// unregister this monobehaviour from the player event handler
		if (m_Player != null)
			m_Player.Unregister(this);
		
	}

	void OnTriggerEnter (Collider other)
	{
		if(other.tag == "Player")
		{
			doorScript.CloseDoors();
			followScript.canChase = false;
			m_Player = other.gameObject.GetComponent<vp_FPPlayerEventHandler>();
			m_Player.HUDText.Send("Go to the gate");
		}
	}
}
