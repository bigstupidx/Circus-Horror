using UnityEngine;
using System.Collections;

public class StaminaRun : MonoBehaviour
{

    // Max running time
    public float RunningTime = 10f;

    // How fast do we regenerate available running time?
    public float RestRate = 1;

    // Player must be able to run for at least X seconds
    public float RunThreshold = 2f;

    private vp_FPPlayerEventHandler Player;
    private float availableRunTime;

	VoiceScript voiceScript;

    protected virtual void Awake()
    {

        // cache the player event handler
		Player = GetComponent<vp_FPPlayerEventHandler>();
        //Player = GameObject.FindObjectOfType(typeof(vp_FPPlayerEventHandler)) as vp_FPPlayerEventHandler;
        availableRunTime = RunningTime;
		voiceScript = GameObject.Find("Candle").GetComponent<VoiceScript>();

    }

    /// <summary>
    /// registers this component with the event handler (if any)
    /// </summary>
    protected virtual void OnEnable()
    {

        // allow this monobehaviour to talk to the player event handler
        if (Player != null)
            Player.Register(this);

    }

    /// <summary>
    /// unregisters this component from the event handler (if any)
    /// </summary>
    protected virtual void OnDisable()
    {

        // unregister this monobehaviour from the player event handler
        if (Player != null)
            Player.Unregister(this);

    }

    public void Update()
    {
        if (!Player.Run.Active)
        {
            // If we're not running, regenerate our stamina
            availableRunTime = Mathf.Min(availableRunTime + Time.deltaTime * RestRate, RunningTime);

            return;
        }

        // Decrease our running time available and see if we have to stop
        availableRunTime -= Time.deltaTime;
        if (availableRunTime <= 0)
        {
            Player.Run.TryStop();
            availableRunTime = 0;
			voiceScript.PlayBreathing();
        }
    }

    protected virtual bool CanStart_Run()
    {
        // Only enable running if we're above the running threshold
        if (availableRunTime >= RunThreshold)
            return true;

        return false;
    }

}