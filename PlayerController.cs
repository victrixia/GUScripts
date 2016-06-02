using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Player))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    Camera cam;
	[SerializeField]
    MouseLook mouseLook;
	[SerializeField]
	Canvas restartMenu;

    Player player;
	Vector3 move;
	float forwardAmount;

    bool onPause = false;

    bool autoJump = false;

    public MouseLook MouseLook {
        get { return mouseLook; }
    }

    public bool AutoJump {
        get { return autoJump; }
        set { autoJump = value; }
    }

    //	KeyCode clockwiseRotation;
    //	KeyCode antiClockwiseRotation;


    void Start () {
		player = GetComponent<Player> ();
        mouseLook.Init (transform, cam.transform);

        StartCoroutine (RotationHandler ());
    }

    void Update () {
        //mouseLook.LookRotation (transform, cam.transform);

        if (autoJump) player.Jump (move);

        else if (player.IsGrounded () && Input.GetKeyDown (KeyCode.Space)) {
            player.Jump (move);
        }
    }

    void OnEnable () {
        EventHandler.RegisterEvent (Events.Type.OnMenuOpen, OpenMenu);
        EventHandler.RegisterEvent (Events.Type.OnMenuClose, CloseMenu);
    }

    void OnDisable () {
        EventHandler.UnregisterEvent (Events.Type.OnMenuOpen, OpenMenu);
        EventHandler.UnregisterEvent (Events.Type.OnMenuClose, CloseMenu);
    }

    IEnumerator RotationHandler () {
        while (true) {
            mouseLook.LookRotation (transform, cam.transform, onPause);
            yield return new WaitForSeconds (0.002f);
        }
    }

	void FixedUpdate () {
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		// Non-normalized total movement amount for animation:
		forwardAmount = Mathf.Abs(h) + Mathf.Abs(v);

		// Normalized movement amount
		move = new Vector3 (h, 0, v).normalized;
        move.x *= player.movementSpeed;
        move.z *= player.movementSpeed;
        move = transform.TransformDirection (move);

		// Sprint
		if (Input.GetKey (KeyCode.LeftShift)) {
			move *= player.sprintPower;
		}
			
		player.Move (move, forwardAmount);
	}

    void OpenMenu () {
        onPause = true;
    }

    void CloseMenu() {
        onPause = false;
    }

    public void GravityRotation(Quaternion amount) {
        mouseLook.GravityRotation(amount);
    }

    void RotatePlayer(Vector3 direction) {
        mouseLook.GravityRotation (Quaternion.AngleAxis (direction.z, Vector3.forward));
    }

    public void ResetRotation(Transform targetTransform) {
        mouseLook.ResetRotation(targetTransform);
    }
		
}