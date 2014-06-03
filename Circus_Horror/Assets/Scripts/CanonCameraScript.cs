using UnityEngine;
using System.Collections;

public class CanonCameraScript : MonoBehaviour 
{

	public GameObject canonBallToShoot;

	AudioListener listener;

	CanonBall canonBallScript;

	public Camera playerCamera;
	public Camera weaponCamera;
	public Camera endCamera;

	Player playerScript;
	vp_Interactable_Canon canonScript;
	AudioListener playerListener;
	vp_SimpleCrosshair simpleHud;


	// Use this for initialization
	void Start () 
	{
		canonBallScript = GameObject.Find("CanonBall").GetComponent<CanonBall>();
		listener = GetComponent<AudioListener>();
		playerListener = GameObject.Find("PlayerCamera").GetComponent<AudioListener>();
		playerScript = GameObject.Find("PlayerCamera").GetComponent<Player>();
		canonScript = GameObject.Find("CannonS").GetComponent<vp_Interactable_Canon>();
		simpleHud = GameObject.Find("Player").GetComponent<vp_SimpleCrosshair>();

	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.LookAt(canonBallToShoot.transform.position);

		if(canonScript.isShooting)
		{

			simpleHud.enabled = false;
			canonScript.isShooting = false;
			this.camera.enabled = true;
			listener.enabled = true;
			playerListener.enabled = false;
			playerScript.DisablePlayer();
			playerCamera.enabled = false;
			weaponCamera.enabled = false;
			endCamera.enabled = false;
			StartCoroutine(SwitchCameraBack());
		}
	}


	IEnumerator SwitchCameraBack()
	{
		yield return new WaitForSeconds(2);
		canonBallScript.Shoot();
		canonScript.PlayCanonAudio();
		yield return new WaitForSeconds(4);
		simpleHud.enabled = true;
		listener.enabled = false;
		playerListener.enabled = true;
		playerScript.EnablePlayer();
		endCamera.enabled = true;
		playerCamera.enabled = true;
		weaponCamera.enabled = true;
		this.camera.enabled = false;
	}
}
