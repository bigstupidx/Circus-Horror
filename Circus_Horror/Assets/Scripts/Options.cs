using UnityEngine;
using System.Collections;

public class Options : MonoBehaviour {

	public Texture background;
	public GUIStyle backButton;
	public Texture button;
	public float sliderValue;

	void OnGUI()
	{
		//GUI.WhatToDo (new Rect( X Position, Y Position, X Pixel Scale, Y Pixel Scale), texture reference / text in quotes );
		GUI.DrawTexture(new Rect( 0, 0, background.width, background.height), background);

		sliderValue = GUI.HorizontalSlider(new Rect(Screen.width / 2 - button.width / 2 , Screen.height / 2 - 300f, 240, 40), sliderValue, 0.0F, 10.0F);
		sliderValue = GUI.HorizontalSlider(new Rect(Screen.width / 2 - button.width / 2 , Screen.height / 2 - 200f, 240, 40), sliderValue, 0.0F, 10.0F);
		sliderValue = GUI.HorizontalSlider(new Rect(Screen.width / 2 - button.width / 2 , Screen.height / 2 - 100f, 240, 40), sliderValue, 0.0F, 10.0F);

		if(GUI.Button(new Rect( Screen.width / 2 - button.width / 2, Screen.height / 2, button.width, button.height), "", backButton ))
		{
			Application.LoadLevel("MainMenu");
		}
	}
}
