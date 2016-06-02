using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerStarter : MonoBehaviour {


    void OnTriggerEnter() {
        EventHandler.ExecuteEvent (Events.Type.OnTimerStart);
    }
}
