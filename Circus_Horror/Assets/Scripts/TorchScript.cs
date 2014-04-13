using UnityEngine;
using System.Collections;

public class TorchScript : MonoBehaviour 
{
	public ParticleSystem torchParticles;
	public Light torchLight;

	bool torchOn = true;
	bool nearLightSource = false;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKey(KeyCode.Q))
		{
			if(torchOn && !nearLightSource)
			{
				torchOn = false;
				torchLight.enabled = false;
				torchParticles.enableEmission = false;
			}
			else if(!torchOn && nearLightSource)
			{
				torchOn = true;
				torchLight.enabled = true;
				torchParticles.enableEmission = true;
			}
		}
	}

	void OnTriggerEnter (Collider other)
	{

		if(other.tag == "FireTrigger")
		{
			Debug.Log("near fire");
			nearLightSource = true;
		}
	}

	void OnTriggerExit (Collider other)
	{
		if(other.tag == "FireTrigger")
		{
			Debug.Log("not near fire");
			nearLightSource = false;
		}
	}
}
