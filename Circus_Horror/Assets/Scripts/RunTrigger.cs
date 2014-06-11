using UnityEngine;
using System.Collections;

public class RunTrigger : MonoBehaviour 
{
	bool hasTriggered = false;
	
	protected vp_FPPlayerEventHandler m_Player;

	// Use this for initialization
	void Start () 
	{
		m_Player = GameObject.Find("Player").GetComponent<vp_FPPlayerEventHandler>();
	}

	void OnTriggerEnter(Collider other)
	{
		if(!hasTriggered && other.tag == "Player")
		{
			hasTriggered = true;
			m_Player.HUDText.Send("Hold Left-Shift to Run");
		}
	}

	
	void OnEnable()
	{
		
		// allow this monobehaviour to talk to the player event handler
		if (m_Player != null)
			m_Player.Register(this);
		
	}
	
	
	/// <summary>
	/// unregisters this component from the event handler (if any)
	/// </summary>
	void OnDisable()
	{
		
		// unregister this monobehaviour from the player event handler
		if (m_Player != null)
			m_Player.Unregister(this);
		
	}
}
