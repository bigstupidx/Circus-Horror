using UnityEngine;
using System.Collections;

public class FogScript : MonoBehaviour 
{
	float currentFog = 0.14f;
	float minFog = 0.02f;

	bool hasTriggered = false;

	public bool increaseFog;
	public bool closeDoor = false;

	vp_DoorInteractable doorScript;

	void Start ()
	{
		doorScript = GameObject.Find("CabinDoorTrigger").GetComponent<vp_DoorInteractable>();
	}

	void OnTriggerEnter (Collider other)
	{
		if(other.tag == "Player" && !hasTriggered)
		{
			hasTriggered = true;

			if(increaseFog)
			{
				if(closeDoor)
				{
					doorScript.CloseCabinDoor();
				}
				iTween.ValueTo(gameObject, iTween.Hash("from", minFog, "to", currentFog, "time", 3, "onupdate", "UpdateFog"));
			}
			else
			{
				iTween.ValueTo(gameObject, iTween.Hash("from", currentFog, "to", minFog, "time", 5, "onupdate", "UpdateFog"));
			}

		}


	}

	void UpdateFog (float newFogValue)
	{
		RenderSettings.fogDensity = newFogValue;
	}
}
