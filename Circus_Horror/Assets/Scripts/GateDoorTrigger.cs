using UnityEngine;
using System.Collections;

public class GateDoorTrigger : MonoBehaviour 
{

	VoiceScript voiceScript;

	bool hasBeenFound = false;

	// Use this for initialization
	void Start () 
	{
		voiceScript = GameObject.Find("PlayerCamera").GetComponent<VoiceScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player" && !hasBeenFound)
		{
			hasBeenFound = true;
			voiceScript.repeatVoice = false;
		}
	}
}
