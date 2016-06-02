using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlashScript : MonoBehaviour {

	public Image fallFlashImage;
	public float flashSpeed;
	public Color flashColour;

	bool fallen = false;


    void OnEnable () {
        EventHandler.RegisterEvent (Events.Type.OnTeleportTrigger, OnTeleportTrigger);
    }

    void OnDisable () {
        EventHandler.UnregisterEvent (Events.Type.OnTeleportTrigger, OnTeleportTrigger);
    }



    // Update is called once per frame
    void Update () {
		
		if (fallen) {
			fallFlashImage.color = flashColour;
		} else {

			fallFlashImage.color = Color.Lerp (fallFlashImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}

		fallen = false;
	}

	public void OnTeleportTrigger() {
	
		fallen = true;

	}
}
