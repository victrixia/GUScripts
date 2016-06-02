using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class TimerScript : MonoBehaviour {
    
    public Text timerText;

    private float startTime;
    private bool stopped = false;
	float minutes = 0;
	float seconds = 0;

    void Start () {
        stopped = true;
    }

    void OnEnable () {
        EventHandler.RegisterEvent (Events.Type.OnTimerStart, OnTimerStart);
        EventHandler.RegisterEvent (Events.Type.OnTimerStop, OnTimerStop);
        EventHandler.RegisterEvent (Events.Type.OnLevelEndSuccess, OnTimerStop);
    }

    void OnDisable () {
        EventHandler.UnregisterEvent (Events.Type.OnTimerStart, OnTimerStart);
        EventHandler.UnregisterEvent (Events.Type.OnTimerStop, OnTimerStop);
        EventHandler.UnregisterEvent (Events.Type.OnLevelEndSuccess, OnTimerStop);
    }

    void Update () {
        if (!stopped) {

            float timeSinceStart = Time.timeSinceLevelLoad;

            seconds = timeSinceStart % 60;
            minutes = (int)(timeSinceStart / 60);

            timerText.text = minutes.ToString ("00") + ":" + seconds.ToString ("00.00");
            
        }
    }

    void OnTimerStart () {
        stopped = false;
    }

    void OnTimerStop () {
        stopped = true;
    }

    public string GetTimerTime () {
        return timerText.text;
    }
    

    public void StopTimer() {
        stopped = true;
    }
}
