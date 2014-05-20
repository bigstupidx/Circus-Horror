using UnityEngine;
using System.Collections;

public class RotatePickup : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		iTween.RotateBy(gameObject, iTween.Hash("y", 1, "time", 6, "looptype", "loop", "easetype", "linear"));
	}
}
