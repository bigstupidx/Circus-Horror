using UnityEngine;
using System.Collections;

public class TorchScript : MonoBehaviour 
{
	public ParticleSystem torchParticles;
	public Light torchLight;

	bool torchOn = true;
	bool nearLightSource = false;
	bool blowOutAnim = false;

	Follow followScript;
	// Use this for initialization
	void Start () 
	{
		followScript = GameObject.Find("SlenderMan").GetComponent<Follow>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKey(KeyCode.Q))
		{
			if(torchOn && !nearLightSource && !blowOutAnim)
			{
				followScript.canChase = false;
				torchOn = false;
				blowOutAnim = true;
				iTween.RotateBy(gameObject, iTween.Hash("y", -0.055, "x", 0.06, "time", 0.5, "easetype", "easeOutQuad", "oncomplete", "blowCandleOut"));
			}
		}

		if(!torchOn && nearLightSource && !blowOutAnim)
		{
			followScript.canChase = true;
			torchOn = true;
			torchLight.enabled = true;
			torchParticles.enableEmission = true;
		}
	}

	void blowCandleOut ()
	{
		torchLight.enabled = false;
		torchParticles.enableEmission = false;
		torchParticles.simulationSpace = ParticleSystemSimulationSpace.World;
		iTween.RotateBy(gameObject, iTween.Hash("y", 0.055, "x", -0.06, "time", 0.5, "delay", 0.4, "easetype", "easeOutQuad", "oncomplete", "animStopped"));

	}

	void animStopped ()
	{
		blowOutAnim = false;
		torchParticles.simulationSpace = ParticleSystemSimulationSpace.Local;

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
