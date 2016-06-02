using UnityEngine;
using System.Collections;

public class TeleportManager : MonoBehaviour {

    [Tooltip("Initial teleport point of level")]
    [SerializeField]
    GameObject firstTeleportTarget;

    GravityController gravityController;

    static Transform currentTarget;

    public Transform CurrentTarget {
        get { return currentTarget; }
        set { currentTarget = value; }
    }

    void Awake () {
        currentTarget = firstTeleportTarget.transform;
        gravityController = FindObjectOfType<GravityController> ();
    }

	/// <summary>
    /// Called when player hits teleport triggers. Resets player position and changes gravity to last checkpoints down direction.
	/// </summary>
	/// <param name="collider"></param>

    public void OnTeleportTrigger (GameObject collider) {
        EventHandler.ExecuteEvent (Events.Type.OnTeleportTrigger);
        gravityController.ResetGravity (currentTarget);
        collider.transform.position = currentTarget.position;
    }

	void Update () {
	}
}
