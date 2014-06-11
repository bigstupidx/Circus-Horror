using UnityEngine;
using System.Collections;

public class StartControl : MonoBehaviour {

	public Texture blinkUp;
	public Texture blinkDown;

	public GameObject tutorialPicture;

	protected vp_FPPlayerEventHandler m_Player;

	vp_FPController m_Controller;

	VoiceScript voiceScript;
	

	float blinkUpPos;
	float blinkDownPos;

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

	// Use this for initialization
	void Start () 
	{
		voiceScript = GameObject.Find("PlayerCamera").GetComponent<VoiceScript>();
		tutorialPicture.SetActive(false);
		blinkUpPos = Screen.height * 0.5f;
		blinkDownPos = Screen.height * 0.4f;
		m_Player = GetComponent<vp_FPPlayerEventHandler>();
		m_Controller = GetComponent<vp_FPController>();
		m_Player.AllowGameplayInput.Set(false);
		m_Player.Attack.Stop();
		m_Controller.Stop();
		StartCoroutine(BlinkSequence());

	}

	void OpenEyes ()
	{
		iTween.ValueTo(gameObject, iTween.Hash("from", Screen.height * 0.5f, "to", Screen.height * 0.1f, "time", 2, "onupdate", "UpdateUpBlink", "oncomplete", "CloseEyes"));
		iTween.ValueTo(gameObject, iTween.Hash("from", Screen.height * 0.4f, "to", Screen.height * 0.8f, "time", 2, "onupdate", "UpdateDownBlink"));
	}

	void CloseEyes ()
	{
		iTween.ValueTo(gameObject, iTween.Hash("from", Screen.height * 0.1f, "to", Screen.height * 0.5f, "time", 0.6, "onupdate", "UpdateUpBlink"));
		iTween.ValueTo(gameObject, iTween.Hash("from", Screen.height * 0.8f, "to", Screen.height * 0.4f, "time", 0.6, "onupdate", "UpdateDownBlink"));
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void OnGUI ()
	{
		GUI.DrawTexture(new Rect(0, 0, Screen.width, blinkUpPos), blinkUp);
		GUI.DrawTexture(new Rect(0, blinkDownPos, Screen.width, Screen.height), blinkDown);
	}

	IEnumerator BlinkSequence ()
	{
		yield return new WaitForSeconds(5);
		AudioClip fileToPlay = Resources.Load("WakeUp") as AudioClip;
		audio.volume = 1;
		audio.PlayOneShot(fileToPlay);
		iTween.ValueTo(gameObject, iTween.Hash("from", Screen.height * 0.5f, "to", Screen.height * 0.1f, "time", 2, "onupdate", "UpdateUpBlink", "oncomplete", "CloseEyes"));
		iTween.ValueTo(gameObject, iTween.Hash("from", Screen.height * 0.4f, "to", Screen.height * 0.8f, "time", 2, "onupdate", "UpdateDownBlink"));
		yield return new WaitForSeconds(4);
		iTween.ValueTo(gameObject, iTween.Hash("from", Screen.height * 0.5f, "to", Screen.height * 0.1f, "time", 1, "onupdate", "UpdateUpBlink", "oncomplete", "CloseEyes"));
		iTween.ValueTo(gameObject, iTween.Hash("from", Screen.height * 0.4f, "to", Screen.height * 0.8f, "time", 1, "onupdate", "UpdateDownBlink"));
		yield return new WaitForSeconds(2);
		iTween.ValueTo(gameObject, iTween.Hash("from", Screen.height * 0.6f, "to", Screen.height * 0.0f, "time", 0.3, "onupdate", "UpdateUpBlink"));
		iTween.ValueTo(gameObject, iTween.Hash("from", Screen.height * 0.3f, "to", Screen.height * 1.0f, "time", 0.3, "onupdate", "UpdateDownBlink"));
		yield return new WaitForSeconds(2);
		voiceScript.setVolume(0.4f);
		voiceScript.PlayFile("Hello");
		yield return new WaitForSeconds(1);
		voiceScript.PlayFile("Who are you");
		yield return new WaitForSeconds(1f);
		voiceScript.motherVoiceSegmentSingle = "Listen to me";
		voiceScript.PlayMotherVoiceSingle();
		yield return new WaitForSeconds(10);
		voiceScript.setVolume(1);
		m_Player.AllowGameplayInput.Set(true);
		tutorialPicture.SetActive(true);
	}

	void UpdateUpBlink (float upPos)
	{
		blinkUpPos = upPos;
	}

	void UpdateDownBlink (float downPos)
	{
		blinkDownPos = downPos;
	}
}
