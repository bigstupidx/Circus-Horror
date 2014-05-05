using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	static public int health = 190;
	static public int ammo;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		RaycastHit hitSomething;

		if (Physics.Raycast(transform.position, transform.forward, out hitSomething, 10.0F))
		{
			Debug.DrawRay(transform.position, transform.forward, Color.cyan);

			if(hitSomething.collider.tag == "Door" && Input.GetKeyDown(KeyCode.R))
			{
				hitSomething.collider.gameObject.GetComponent<DoorScript>().DoorOpening();
			}
		}
	}
}
