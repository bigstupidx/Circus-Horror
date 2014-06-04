using UnityEngine;
using System.Collections;

public class WindTrigger : MonoBehaviour 
{
	bool hasTriggered = false;

	CandleScript candleScript;
	VoiceScript voiceScript;

	protected vp_FPPlayerEventHandler m_Player;

	// Use this for initialization
	void Start () 
	{
		voiceScript = GameObject.Find("PlayerCamera").GetComponent<VoiceScript>();
		m_Player = GameObject.Find("Player").GetComponent<vp_FPPlayerEventHandler>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if(!hasTriggered)
		{
			hasTriggered = true;
			StartCoroutine(TriggerVoice());
			audio.Play();
			candleScript = GameObject.Find("Arm").GetComponent<CandleScript>();
			candleScript.CandleTrigger();
		}

	}

	IEnumerator TriggerVoice ()
	{
		yield return new WaitForSeconds(3);
		voiceScript.PlayFile("Too Dark 1");
		m_Player.HUDText.Send("You can't be in the dark too long \n Lite the candle soon");
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
