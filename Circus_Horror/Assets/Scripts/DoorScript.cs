using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour 
{

	bool doorOpen = false;
	bool doorIsOpening = false;
	public GameObject door;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void DoorOpening ()
	{
		if(!doorIsOpening)
		{
			doorOpen = !doorOpen;
			doorIsOpening = true;
			if(doorOpen)
			{
				iTween.RotateBy(gameObject, iTween.Hash("y", -0.25, "time", 1, "easetype", "linear", "oncomplete", "FinishedOpening"));
			}
			else
			{
				iTween.RotateBy(gameObject, iTween.Hash("y", 0.25, "time", 1, "easetype", "linear", "oncomplete", "FinishedOpening"));
			}
		}
	}

	void FinishedOpening ()
	{
		doorIsOpening = false;
	}
}
