using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
	public Texture background;
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
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background);
		if(GUI.Button(new Rect(Screen.width / 2 - 45, Screen.height * 0.3f, 90, 40), "Play"))
		{
			Application.LoadLevel("Level01");
		}
	}
}
