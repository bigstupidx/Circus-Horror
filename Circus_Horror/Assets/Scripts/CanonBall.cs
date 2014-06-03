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

	public ParticleSystem Explosion;
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	public void Shoot ()
	{
		if(!hasTriggered)
		{
			Explosion.Play();
			StartCoroutine(FireCanon());
		}
	}

	IEnumerator FireCanon ()
	{
		yield return new WaitForSeconds(0.1f);
		Destroy(leftDoor);
		Destroy(rightDoor);
		Instantiate(brokenDoor, doorPosition.position, doorPosition.rotation);
		
		wall.collider.enabled = false;
		hasTriggered = true;
		rigidbody.isKinematic = false;
		rigidbody.AddForce(new Vector3(-5000, 0, 0));
	}


}
