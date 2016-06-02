using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class Checkpoint : MonoBehaviour {

    TeleportManager manager;
    BoxCollider coll;

    void Start () {
        manager = FindObjectOfType<TeleportManager> ();
    }

#if UNITY_EDITOR
    void OnDrawGizmos () {
        Vector3 forward = transform.TransformDirection (Vector3.forward) * 4f;
        Vector3 up = transform.TransformDirection (Vector3.up) * 4f;
        Debug.DrawRay (transform.position, forward, Color.blue);
        Debug.DrawRay (transform.position, up, Color.green);
    }
#endif

    void OnTriggerEnter (Collider collider) {
        if (collider.GetComponent<Player> ()) {
            manager.CurrentTarget = transform;
        }
    }
}
