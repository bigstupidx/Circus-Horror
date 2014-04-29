﻿using UnityEngine;
using System.Collections;

public class CandleScript : MonoBehaviour 
{
	public ParticleSystem candleParticles;
	public Light candleLight;
	public GameObject candleBottom;
	public Transform candleTopPosition;
	public Transform candleTop;
	public int torchTime = 30;

	[System.NonSerialized]
	public bool canShowClownImage = false;
	
	bool torchOn = true;
	bool nearLightSource = false;
	bool blowOutAnim = false;
	
	ManagerScript managerScript;
	
	Follow followScript;
	Player playerScript;

	void Awake ()
	{

	}
	// Use this for initialization
	void Start () 
	{
		followScript = GameObject.Find("SlenderMan").GetComponent<Follow>();
		playerScript = GameObject.Find("PlayerCamera").GetComponent<Player>();
		managerScript = GameObject.Find("GameManager").GetComponent<ManagerScript>();
		iTween.ScaleTo(candleBottom, iTween.Hash("z", 0.2, "time", torchTime, "easetype", "linear"));
	}
	
	// Update is called once per frame
	void Update () 
	{
		candleTop.position = candleTopPosition.position;

		if(Input.GetKey(KeyCode.E))
		{
			if(torchOn && !nearLightSource && !blowOutAnim)
			{
				iTween.Pause(candleBottom);
				followScript.canChase = false;
				torchOn = false;
				blowOutAnim = true;
				iTween.RotateBy(gameObject, iTween.Hash("y", 0.055, "x", 0.07, "time", 0.5, "easetype", "easeOutQuad", "oncomplete", "blowCandleOut"));
			}
		}
		
		if(!torchOn && nearLightSource && !blowOutAnim)
		{
			if(managerScript.slenderActive)
			{
				playerScript.changeMusic(2);
			}
			else
			{
				playerScript.changeMusic(0);
			}

			iTween.Resume(candleBottom);
			followScript.canChase = true;
			torchOn = true;
			candleLight.enabled = true;
			candleParticles.enableEmission = true;
			canShowClownImage = false;
		}
	}
	
	void blowCandleOut ()
	{
		playerScript.changeMusic(1);
		candleLight.enabled = false;
		candleParticles.enableEmission = false;
		canShowClownImage = true;
		candleParticles.simulationSpace = ParticleSystemSimulationSpace.World;
		iTween.RotateBy(gameObject, iTween.Hash("y", -0.055, "x", -0.07, "time", 0.5, "delay", 0.4, "easetype", "easeOutQuad", "oncomplete", "animStopped"));
		
	}
	
	void animStopped ()
	{
		blowOutAnim = false;
		candleParticles.simulationSpace = ParticleSystemSimulationSpace.Local;
		
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
