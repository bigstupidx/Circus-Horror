using UnityEngine;
using System.Collections;

public class CanonBall : MonoBehaviour 
{
	public GameObject leftDoor;
	public GameObject rightDoor;
	public GameObject wall;
	public GameObject brokenDoor;
	public Transform doorPosition;
	bool hasTriggered = false;
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.U) && !hasTriggered)
		{
			Destroy(leftDoor);
			Destroy(rightDoor);
			Instantiate(brokenDoor, doorPosition.position, doorPosition.rotation);

			wall.collider.enabled = false;
			hasTriggered = true;
			rigidbody.isKinematic = false;
			rigidbody.AddForce(new Vector3(-8000, 0, 0));
		}
	}
}
