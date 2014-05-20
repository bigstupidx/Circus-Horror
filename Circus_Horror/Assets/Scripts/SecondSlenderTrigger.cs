using UnityEngine;
using System.Collections;

public class SecondSlenderTrigger : MonoBehaviour 
{
	Follow followScript;
	ManagerScript managerScript;

	bool slenderTriggered = false;

	// Use this for initialization
	void Start () 
	{
		followScript = GameObject.Find("SlenderMan").GetComponent<Follow>();
		managerScript = GameObject.Find("GameManager").GetComponent<ManagerScript>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter (Collider other)
	{
		if(other.tag == "Player")
		{
			if(!slenderTriggered)
			{
				slenderTriggered = true;
				followScript.secondArea = true;
				managerScript.slenderActive = true;
			}
		}
	}
}
