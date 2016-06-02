using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelEndMenu : MonoBehaviour {

    [SerializeField]
    GameObject panelHolder;
    [SerializeField]
    Text endTimeText;
    [SerializeField]
    TimerScript timer;

    void Start () {
        panelHolder.SetActive (false);
    }

    void OnEnable () {
        EventHandler.RegisterEvent (Events.Type.OnLevelEndSuccess, OnLevelEndSuccess);
    }

    void OnDisable () {
        EventHandler.UnregisterEvent (Events.Type.OnLevelEndSuccess, OnLevelEndSuccess);
    }

    void OnLevelEndSuccess () {
        panelHolder.SetActive (true);
        endTimeText.text = timer.GetTimerTime ();
        EventHandler.ExecuteEvent (Events.Type.OnMenuOpen);
    }
	

}
