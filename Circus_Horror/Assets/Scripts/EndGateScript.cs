using UnityEngine;
using System.Collections;

public class EndGateScript : MonoBehaviour 
{

	public ParticleSystem particles;
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "CanonBall")
		{
			audio.Play();
			particles.Play();
		}
	}
}
