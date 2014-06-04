using UnityEngine;
using System.Collections;

public class ManagerScript : MonoBehaviour 
{
	[System.NonSerialized]
	public bool slenderActive = false;

	public Transform[] firstKeyPositions;
	public GameObject firstKey;

	public Transform[] canonBallPositions;
	public GameObject canonBallPickup;

	public Transform[] powderPositions;
	public GameObject powderPickup;

	public Transform[] fusePositions;
	public GameObject fusePickup;

	// Use this for initialization
	void Start () 
	{
		int firstKeyPosition = Random.Range(0, firstKeyPositions.Length);
		firstKey.transform.position = firstKeyPositions[firstKeyPosition].position;
		firstKey.transform.rotation = firstKeyPositions[firstKeyPosition].rotation;

		int canonballPosition = Random.Range(0, canonBallPositions.Length);
		canonBallPickup.transform.position = canonBallPositions[canonballPosition].position;

		int powderPosition = Random.Range(0, powderPositions.Length);
		powderPickup.transform.position = powderPositions[powderPosition].position;

		int fusePosition = Random.Range(0, fusePositions.Length);
		fusePickup.transform.position = fusePositions[fusePosition].position;
	}
}
