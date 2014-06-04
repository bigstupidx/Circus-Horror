using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
	public Texture background;
	public Texture black;
	public MovieTexture movie;

	public bool isCredits = false;
	// Use this for initialization
	void Start () 
	{
		if(movie != null)
		{
			movie.loop = true;
			movie.Play();
		}

		if(isCredits)
		{
			StartCoroutine(ToMainMenu());
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnGUI ()
	{
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), movie);
		//GUI.DrawTexture(new Rect(Screen.width * 0.7f, Screen.height * 0.3f, Screen.width * 0.9f, Screen.height * 0.6f), black);

		if(!isCredits)
		{
			if(GUI.Button(new Rect(Screen.width * 0.7f - 45, Screen.height * 0.4f, 90, 40), "Play"))
			{
				Application.LoadLevel("Level01");
			}
		}

	}

	IEnumerator ToMainMenu ()
	{
		yield return new WaitForSeconds(33);
		Application.LoadLevel("MainMenu");
	}
}
