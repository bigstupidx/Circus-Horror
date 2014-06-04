using UnityEngine;
using System.Collections;

public class WindTrigger : MonoBehaviour 
{
	bool hasTriggered = false;

	CandleScript candleScript;
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if(!hasTriggered)
		{
			hasTriggered = true;
			audio.Play();
			candleScript = GameObject.Find("Arm").GetComponent<CandleScript>();
			candleScript.CandleTrigger();
		}

	}
}
