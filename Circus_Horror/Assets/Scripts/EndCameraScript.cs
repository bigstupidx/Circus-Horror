﻿using UnityEngine;
using System.Collections;

public class EndCameraScript : MonoBehaviour 
{
	float pathPercentagePerSecond = 0.02f;
	float currentPercentage = 0;
	float currentAlpha = 1;

	public bool gameOver = false;

	bool cameraHasChanged = false;

	public Camera playerCamera;
	public Camera weaponCamera;
	public Camera canonCamera;
	public Texture black;

	Player playerScript;

	public GUIText gameOverText;
	public GUISkin horrorSkin;
	// Use this for initialization
	void Start () 
	{
		playerScript = GameObject.Find("PlayerCamera").GetComponent<Player>();
		gameOverText.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(gameOver)
		{
			if(!cameraHasChanged)
			{
				GameObject.Find("Player").GetComponent<vp_SimpleHUD>().ShowHUD = false;
				playerScript.changeMusic(3);
				cameraHasChanged = true;
				weaponCamera.enabled = false;
				playerCamera.enabled = false;
				canonCamera.enabled = false;
				this.enabled = true;
				iTween.ValueTo(gameObject, iTween.Hash("from", 1, "to", 0, "time", 2, "onupdate", "UpdateBlack"));

			}
			currentPercentage += pathPercentagePerSecond * Time.deltaTime;
			
			if(currentPercentage < 1)
			{
				iTween.PutOnPath(gameObject, iTweenPath.GetPath("EndPath"), currentPercentage);
			}
			else
			{
				Application.LoadLevel("MainMenu");
			}
		}

	}

	void UpdateBlack (float blackness)
	{
		currentAlpha = blackness;
	}

	void OnGUI ()
	{

		GUI.skin = horrorSkin;
		if(cameraHasChanged)
		{
			gameOverText.enabled = true;
			GUI.enabled = true;
			/*if(GUI.Button(new Rect(Screen.width / 2 - 45, Screen.height * 0.6f, 90, 40), "Restart"))
			{
				Debug.Log("button clicked");
				Application.LoadLevel("Level01");
			}*/
			GUI.color = new Color(1.0f, 1.0f, 1.0f, currentAlpha);
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), black);
		}
	}
}
