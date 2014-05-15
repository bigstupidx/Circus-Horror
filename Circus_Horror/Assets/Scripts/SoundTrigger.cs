using UnityEngine;
using System.Collections;

public class SoundTrigger : MonoBehaviour 
{
	bool hasPlayed = false;
	
	Transform player;

	Transform _transform;

	float currentDistance;
	public float maxDistance = 10;

	void Start ()
	{
		player = GameObject.Find("Player").GetComponent<Transform>();
		_transform = GetComponent<Transform>();
	}

	void Update ()
	{
		currentDistance = Vector3.Distance(player.position, _transform.position);
		if(currentDistance > maxDistance)
		{
			hasPlayed = false;
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if(other.tag == "Player" && !hasPlayed)
		{
			hasPlayed = true;
			audio.Play();
		}
	}
}
