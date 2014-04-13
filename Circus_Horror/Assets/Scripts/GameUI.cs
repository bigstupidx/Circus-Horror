using UnityEngine;
using System.Collections;

public class GameUI : MonoBehaviour {

	public float halfScreenWidth;
	public float halfScreenHeight;

	public GUISkin mySkin;

	public Texture healthBackground, healthBar;
	public float healthXPos, healthYPos;
	public int health = 190;

	public Texture crosshair;

	public Texture ammoBackground;
	public float ammoBackXPos, ammoBackYPos;
	public float ammoXPos, ammoYPos;
	public int ammo = 20;

	public Texture journal;
	public bool showJournal;
	public string journalText;

	public Texture button;
	public GUIStyle mainMenu;
	public bool showMenu;

	void Start()
	{
		halfScreenWidth = Screen.width / 2;
		halfScreenHeight = Screen.height / 2;
	}

	void OnGUI()
	{
		GUI.skin = mySkin;

		GUI.DrawTexture(new Rect(0, 0, healthBackground.width, healthBackground.height), healthBackground);
		GUI.DrawTexture(new Rect(healthXPos, healthYPos, health, healthBar.height), healthBar);

		GUI.DrawTexture(new Rect(halfScreenWidth - crosshair.width / 2, halfScreenHeight - crosshair.height / 2, crosshair.width, crosshair.height), crosshair);

		GUI.DrawTexture(new Rect(ammoBackXPos, ammoBackYPos, ammoBackground.width, ammoBackground.height), ammoBackground);
		GUI.Label (new Rect(ammoXPos, ammoYPos, 100, 100), ammo.ToString("0"));

		if(showJournal)
		{
			GUI.BeginGroup(new Rect(halfScreenWidth - journal.width / 2, halfScreenHeight - journal.height / 2, journal.width, journal.height));
			GUI.DrawTexture(new Rect(0, 0, journal.width, journal.height), journal);
			GUI.Label (new Rect( 20, 20, journal.width, journal.height), journalText);
			GUI.EndGroup();
		}

		if(showMenu)
		{
			Screen.showCursor = true;
			if(GUI.Button(new Rect( halfScreenWidth - button.width / 2, halfScreenHeight - button.height / 2, button.width, button.height), "", mainMenu ))
			{
				Application.LoadLevel("MainMenu");

			}
		}
		else
		{
			Screen.showCursor = false;
		}
	}

	void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			ammo--;
		}
		if(Input.GetKeyDown (KeyCode.H))
		{
			health -= 10;
		}
		if(Input.GetKeyDown (KeyCode.J))
		{
			showJournal = !showJournal;
		}
		if(Input.GetKeyDown (KeyCode.Escape))
		{
			showMenu = !showMenu;
		}
	}
}
