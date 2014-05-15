using UnityEngine;
using System.Collections;

public class ForestTrigger : MonoBehaviour 
{
	Follow followScript;


	void Start ()
	{
		followScript = GameObject.Find("SlenderMan").GetComponent<Follow>();
	}

	void OnTriggerEnter (Collider other)
	{
		if(other.collider.tag == "Player")
		{
			followScript.forestTrigger = true;
		}
	}
}
