using UnityEngine;
using System.Collections;

public class ManagerScript : MonoBehaviour 
{
	[System.NonSerialized]
	public bool slenderActive = false;

	public Transform[] firstKeyPositions;
	public GameObject firstKey;

	// Use this for initialization
	void Start () 
	{
		int firstKeyPosition = Random.Range(0, firstKeyPositions.Length);
		firstKey.transform.position = firstKeyPositions[firstKeyPosition].position;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
