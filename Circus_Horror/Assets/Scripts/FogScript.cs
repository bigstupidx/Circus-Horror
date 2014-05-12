using UnityEngine;
using System.Collections;

public class FogScript : MonoBehaviour 
{
	float currentFog = 0.08f;
	float minFog = 0.02f;

	bool hasTriggered = false;

	void OnTriggerEnter (Collider other)
	{
		if(other.tag == "Player" && !hasTriggered)
		{
			hasTriggered = true;
			iTween.ValueTo(gameObject, iTween.Hash("from", currentFog, "to", minFog, "time", 5, "onupdate", "UpdateFog"));
		}


	}

	void UpdateFog (float newFogValue)
	{
		RenderSettings.fogDensity = newFogValue;
	}
}
