using UnityEngine;
using System.Collections;

public class SecondDoorTrigger : MonoBehaviour 
{
	vp_DoorInteractable doorScript;
	Follow followScript;

	void Start ()
	{
		doorScript = GameObject.Find("SecondDoorTrigger").GetComponent<vp_DoorInteractable>();
		followScript = GameObject.Find("SlenderMan").GetComponent<Follow>();
	}

	void OnTriggerEnter (Collider other)
	{
		if(other.tag == "Player")
		{
			doorScript.CloseDoors();
			followScript.canChase = false;
		}
	}
}
