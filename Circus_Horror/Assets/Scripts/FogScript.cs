using UnityEngine;
using System.Collections;

public class FogScript : MonoBehaviour 
{
	public float MaxFog = 0.14f;
	float minFog = 0.02f;

	bool hasTriggered = false;

	public bool increaseFog;
	public bool closeDoor = false;

	vp_DoorInteractable doorScript;
	CandleScript candleScript;

	void Start ()
	{
		doorScript = GameObject.Find("CabinDoorTrigger").GetComponent<vp_DoorInteractable>();


	}

	void OnTriggerEnter (Collider other)
	{
		if(other.tag == "Player" && !hasTriggered)
		{
			hasTriggered = true;
			candleScript = GameObject.Find("Arm").GetComponent<CandleScript>();
			candleScript.canTurnCandleOff = true;

			if(increaseFog)
			{
				if(closeDoor)
				{
					doorScript.CloseCabinDoor();
				}
				iTween.ValueTo(gameObject, iTween.Hash("from", minFog, "to", MaxFog, "time", 1, "onupdate", "UpdateFog"));
			}
			else
			{
				iTween.ValueTo(gameObject, iTween.Hash("from", MaxFog, "to", minFog, "time", 5, "onupdate", "UpdateFog"));
			}

		}


	}

	void UpdateFog (float newFogValue)
	{
		RenderSettings.fogDensity = newFogValue;
	}
}
