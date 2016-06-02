using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class timerStopper : MonoBehaviour {

    public Text timer;
    TimerScript script;

    void Start() {
       script = timer.GetComponent<TimerScript>();
    }

    void OnTriggerEnter() {
        script.StopTimer ();
        EventHandler.ExecuteEvent (Events.Type.OnLevelEndSuccess);
    }
}
