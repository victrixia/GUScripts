//using UnityEngine;
//using System.Collections;

///// <summary>
///// Example of Event subscription usage, logs combo events
///// </summary>
//public class ExampleComboEventSubscription : MonoBehaviour {

//    void OnEnable () {
//        EventHandler.RegisterEvent (Events.Type.OnComboStart, OnComboStart);
//		EventHandler.RegisterEvent<bool> (Events.Type.OnComboEnd, OnComboEnd);
//        EventHandler.RegisterEvent<string> (Events.Type.OnComboStepStart, OnComboStepStart);
//        EventHandler.RegisterEvent<float> (Events.Type.OnComboStepSuccess, OnComboStepSuccess);
//    }

//    void OnDisable () {
//        EventHandler.UnregisterEvent (Events.Type.OnComboStart, OnComboStart);
//		EventHandler.UnregisterEvent<bool> (Events.Type.OnComboEnd, OnComboEnd);
//        EventHandler.UnregisterEvent<string> (Events.Type.OnComboStepStart, OnComboStepStart);
//        EventHandler.UnregisterEvent<float> (Events.Type.OnComboStepSuccess, OnComboStepSuccess);
//    }

//    void OnComboStart () {
//        Debug.Log ("OnComboStart");
//    }

//	void OnComboEnd (bool success) {
//		if (success) Debug.Log ("OnComboSuccess");
//		else Debug.Log ("OnComboFail");
//    }

//    void OnComboStepStart (string button) {
//        Debug.Log ("OnComboStepStart, press: " + button);
//    }

//    void OnComboStepSuccess (float damage) {
//        Debug.Log ("OnComboStepSuccess, damage dealt: " + damage);
//    }
//}
