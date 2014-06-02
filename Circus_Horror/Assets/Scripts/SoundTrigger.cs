using UnityEngine;
using System.Collections;

public class SoundTrigger : MonoBehaviour 
{
	bool hasPlayed = false;
	
	Transform player;

	Transform _transform;

	float currentDistance;
	public float maxDistance = 10;

	public bool playOnce = false;

	void Start ()
	{
		player = GameObject.Find("Player").GetComponent<Transform>();
		_transform = GetComponent<Transform>();
	}

	void Update ()
	{
		currentDistance = Vector3.Distance(player.position, _transform.position);
		if(currentDistance > maxDistance && !playOnce)
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
