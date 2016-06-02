using UnityEngine;

[RequireComponent (typeof (SpringJoint))]
public class GrappleTarget : MonoBehaviour {
    [Tooltip ("Distance from object to player")]
    [SerializeField]
    float hangDistance = 1.5f;

    SpringJoint joint;
    Player player;

    void Awake () {
        joint = GetComponent<SpringJoint> ();
        joint.autoConfigureConnectedAnchor = false;
        joint.minDistance = hangDistance;
        joint.maxDistance = hangDistance;

        player = FindObjectOfType<Player> ();

        Rigidbody rb = GetComponent<Rigidbody> ();
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    void Update () {
        transform.eulerAngles = new Vector3 (0f, 0f, player.transform.rotation.eulerAngles.z);
    }

    public void Grapple (Rigidbody rbody) {
        joint.connectedBody = null;
        joint.anchor = Vector3.zero;
        joint.connectedAnchor = Vector3.zero;
        joint.connectedBody = rbody;
    }

    public void Release () {
        joint.connectedBody = null;
    }
}
