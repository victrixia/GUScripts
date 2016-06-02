using UnityEngine;
using System.Collections;

public class PlayerRaycaster : MonoBehaviour {

    public Camera cam;

    Player player;
    Rigidbody rb;

    bool onGrapple = false;
    GrappleTarget grapple = null;

    void Awake () {
        player = GetComponent<Player> ();
        rb = GetComponent<Rigidbody> ();
    }

    void GrapplingShot (GameObject target) {
        if (!onGrapple) {
            GrappleTarget grappleTarget = target.GetComponent<GrappleTarget> ();
            if (grappleTarget) {
                grapple = grappleTarget;
                grapple.Grapple (rb);

                onGrapple = true;
            }
        }
    }

    void ExitGrapple () {
        grapple.Release ();
        grapple = null;

        onGrapple = false;
    }

    void Update () {
        if (Input.GetButtonDown ("Fire1")) {
            RaycastHit hit;
            Ray ray = new Ray (cam.transform.position, cam.transform.forward);

            if (Physics.Raycast (ray, out hit)) {
                SwitchButton button = hit.collider.gameObject.GetComponent<SwitchButton> ();
                GrappleTarget grappleTarget = hit.collider.gameObject.GetComponent<GrappleTarget> ();

                if (button) button.Toggle ();

                else if (grappleTarget) GrapplingShot (hit.collider.gameObject);
            }
        }

        if (onGrapple) {
            if (Input.GetKeyDown (KeyCode.Space)) ExitGrapple ();
        }

    }
}
