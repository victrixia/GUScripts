using UnityEngine;
using System.Collections;

public class UphillCollision : MonoBehaviour {

    Player player;

    void Awake () {
        player = FindObjectOfType<Player> ();
    }

	void OnTriggerEnter(Collider col) {
        if (col.gameObject.GetComponent<Player> ()) {
            player.UphillCollision(transform.rotation);
            Debug.Log("Uphill entered.");
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.gameObject.GetComponent<Player> ()) {
            player.UphillCollisionExit();
            Debug.Log("Upill exited.");
        }
    }
}
