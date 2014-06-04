using UnityEngine;
using System.Collections;

public class SecondSlenderTrigger : MonoBehaviour 
{
	Follow followScript;
	ManagerScript managerScript;
	Player playerScript;

	bool slenderTriggered = false;

	// Use this for initialization
	void Start () 
	{
		followScript = GameObject.Find("SlenderMan").GetComponent<Follow>();
		managerScript = GameObject.Find("GameManager").GetComponent<ManagerScript>();
		playerScript = GameObject.Find("PlayerCamera").GetComponent<Player>();
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
				playerScript.changeMusic(2);
				slenderTriggered = true;
				followScript.secondArea = true;
				managerScript.slenderActive = true;
			}
		}
	}
}
