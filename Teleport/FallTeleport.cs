using UnityEngine;
using System.Collections;

public class FallTeleport : MonoBehaviour {
	
    TeleportManager manager;

    void Start () {
        manager = FindObjectOfType<TeleportManager> ();
    }

	void OnTriggerEnter(Collider collider) {
        if (collider.GetComponent<Player> ()) 
            manager.OnTeleportTrigger (collider.gameObject);
	}
}
