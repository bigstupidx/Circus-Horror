using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnGUI ()
	{
		if(GUI.Button(new Rect(Screen.width / 2 - 45, Screen.height / 2, 90, 40), "Play"))
		{
			Application.LoadLevel("Level01");
		}
	}
}
