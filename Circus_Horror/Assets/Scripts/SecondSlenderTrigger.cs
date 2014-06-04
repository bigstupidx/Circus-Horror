using UnityEngine;
using System.Collections;

public class SecondSlenderTrigger : MonoBehaviour 
{
	Follow followScript;
	ManagerScript managerScript;
	Player playerScript;
	SlenderVoices slenderVoice;

	public Transform spawn;

	// Use this for initialization
	void Start () 
	{
		followScript = GameObject.Find("SlenderMan").GetComponent<Follow>();
		managerScript = GameObject.Find("GameManager").GetComponent<ManagerScript>();
		playerScript = GameObject.Find("PlayerCamera").GetComponent<Player>();
		slenderVoice = GameObject.Find("SlenderMan").GetComponent<SlenderVoices>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter (Collider other)
	{
		if(other.tag == "Player")
		{
			if(!followScript.secondSlenderTriggered)
			{
				playerScript.changeMusic(2);
				followScript.secondSlenderTriggered = true;
				followScript.SecondTrigger(spawn);
				managerScript.slenderActive = true;
				slenderVoice.PlayFile("Where do you think 1");
			}
		}
	}
}
