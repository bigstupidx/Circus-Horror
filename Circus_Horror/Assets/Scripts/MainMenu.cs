using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public Texture background;
	public Texture logo;
	public Texture button;
	public float logoYOffset;
	public float buttonYOffset;
	public GUIStyle startButton, optionsButton, quitButton;

	void OnGUI()
	{
		//GUI.WhatToDo (new Rect( X Position, Y Position, X Pixel Scale, Y Pixel Scale), texture reference / text in quotes );
		GUI.DrawTexture(new Rect( 0, 0, background.width, background.height), background);
		GUI.DrawTexture(new Rect( Screen.width / 2 - logo.width / 2, Screen.height / 2 * logoYOffset, logo.width, logo.height), logo);

		if(GUI.Button(new Rect( Screen.width / 2 - button.width / 2, Screen.height / 2 * buttonYOffset, button.width, button.height), "", startButton ))
		{
			Application.LoadLevel("Level01");
		}
		if(GUI.Button(new Rect( Screen.width / 2 - button.width / 2, Screen.height / 2 * buttonYOffset +100f, button.width, button.height), "", optionsButton ))
		{
			Application.LoadLevel("Options");
		}
		if(GUI.Button(new Rect( Screen.width / 2 - button.width / 2, Screen.height / 2 * buttonYOffset +200f, button.width, button.height), "", quitButton ))
		{
			Application.Quit();
		}
	}
}
