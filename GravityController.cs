using UnityEngine;
using System.Collections;

public class GravityController : MonoBehaviour {

    [SerializeField] float gravitySpeed = 9.81F;
    [SerializeField] float cooldownTime = 2;
    [SerializeField] float rotationSpeed = 500.0F;
    [SerializeField] Player player;
    [SerializeField] PlayerController playerController;

	KeyCode clockwiseRotationKey;
	KeyCode counterClockwiseRotationKey;

    bool onCooldown = false;

    Vector3 currentGravity;
    Vector3 targetGravity;

    Quaternion clockwiseRotation = Quaternion.AngleAxis(-90, Vector3.forward);
    Quaternion counterclockwiseRotation = Quaternion.AngleAxis(90, Vector3.forward);

    Quaternion clockwisePlayerRotation = Quaternion.AngleAxis(-3, Vector3.forward);
    Quaternion counterclockwisePlayerRotation = Quaternion.AngleAxis(3, Vector3.forward);

    Quaternion targetPlayerRotation;


    int rotationsRemaining = 0;

    float nextCooldownTime;

    bool keysFlipped = false;

    bool gravitySwitchEnabled = true;

    public float autoInvertAngle;

    public bool OnCooldown {
		get { return onCooldown; }
	}

    public bool AutoInvertEnabled { get; set; }

    void Start() {
        currentGravity = new Vector3(0, -gravitySpeed, 0);
        targetGravity = new Vector3(0, -gravitySpeed, 0);
        nextCooldownTime = Time.time;
        Physics.gravity = currentGravity;
		clockwiseRotationKey = KeyCode.Q;
		counterClockwiseRotationKey = KeyCode.E;

        AutoInvertEnabled = true;
        autoInvertAngle = 0.4f;

        StartCoroutine (RotationHandler ());
    }

    void Update() {

        if (AutoInvertEnabled) {
            FlipKeysWhenBackwards ();
        }

		if (!onCooldown && gravitySwitchEnabled) {
			if (Input.GetKeyDown (counterClockwiseRotationKey)) {
                CounterclockwiseLerp ();
				InitiateCooldown ();
				nextCooldownTime = Time.time + cooldownTime;
            }
			if (Input.GetKeyDown (clockwiseRotationKey)) {
                ClockwiseLerp ();
				InitiateCooldown ();
				nextCooldownTime = Time.time + cooldownTime;
            }
		}

        if (nextCooldownTime < Time.time) {
			TerminateCooldown ();
		}
    }

    void OnEnable () {
        EventHandler.RegisterEvent (Events.Type.OnTeleportTrigger, OnTeleportTrigger);
        EventHandler.RegisterEvent (Events.Type.OnGravityToolEnable, OnGravityToolEnable);
        EventHandler.RegisterEvent (Events.Type.OnGravityToolDisable, OnGravityToolDisable);
    }

    void OnDisable () {
        EventHandler.UnregisterEvent (Events.Type.OnTeleportTrigger, OnTeleportTrigger);
        EventHandler.UnregisterEvent (Events.Type.OnGravityToolEnable, OnGravityToolEnable);
        EventHandler.UnregisterEvent (Events.Type.OnGravityToolDisable, OnGravityToolDisable);
    }

    void FixedUpdate() {
        currentGravity = Vector3.Lerp (currentGravity, targetGravity, Time.deltaTime * rotationSpeed);
        Physics.gravity = currentGravity;
    }

    void OnGravityToolEnable () {
        gravitySwitchEnabled = true;
    }

    void OnGravityToolDisable () {
        gravitySwitchEnabled = false;
    }

    void OnTeleportTrigger () {
        rotationsRemaining = 0;
    }

    IEnumerator RotationHandler () {
        while (true) {
            if (rotationsRemaining > 0) {
                playerController.GravityRotation (targetPlayerRotation);
                rotationsRemaining--;
            }
            else {
                TerminateCooldown ();
                rotationsRemaining = 0;
            }
            yield return new WaitForSeconds (0.001f);
        }
    }

    private void FlipKeysWhenBackwards () {
        //Flips gravity controls when facing opposite direction. Could be optional and customisable.
        if (!keysFlipped && Vector3.Dot (player.transform.forward, Vector3.forward) < -autoInvertAngle) {
            reverseGravityKeys ();
            keysFlipped = true;
        }
        else if (keysFlipped && Vector3.Dot (player.transform.forward, Vector3.forward) >= -autoInvertAngle) {
            reverseGravityKeys ();
            keysFlipped = false;
        }
    }

    /// <summary>
    /// Instantly change gravity and player rotation clockwise
    /// </summary>
    public void Clockwise() {
        currentGravity = clockwiseRotation * currentGravity;
        Physics.gravity = currentGravity;
		player.GravityRotation (clockwiseRotation);
    }

    /// <summary>
    /// Instantly change gravity and player rotation counterclockwise
    /// </summary>
    public void Counterclockwise() {
        currentGravity = counterclockwiseRotation * currentGravity;
        Physics.gravity = currentGravity;
		player.GravityRotation (counterclockwiseRotation);
    }

    /// <summary>
    /// Smoothly change gravity and player rotation clockwise
    /// </summary>
    public void ClockwiseLerp() {
        if (player.GravityRotationsLeft()) {
            targetGravity = clockwiseRotation * currentGravity;
            targetPlayerRotation = clockwisePlayerRotation;
            rotationsRemaining += 30;
            player.RemoveFromGravityRotationLimit();
        }
        
    }

    /// <summary>
    /// Smoothly change gravity and player rotation counterclockwise
    /// </summary>
    public void CounterclockwiseLerp() {
        if (player.GravityRotationsLeft()) {
            targetGravity = counterclockwiseRotation * currentGravity;
            targetPlayerRotation = counterclockwisePlayerRotation;
            rotationsRemaining += 30;
            player.RemoveFromGravityRotationLimit();
        }
    }

    /// <summary>
    /// Set gravity and player's rotation, stop player's speed
    /// </summary>
    /// <param name="targetTransform"></param>
    public void ResetGravity (Transform targetTransform) {
        rotationsRemaining = 0;
        targetGravity = -gravitySpeed * targetTransform.up;
        currentGravity = -gravitySpeed * targetTransform.up;
        player.SetRotation (targetTransform);
        playerController.ResetRotation(targetTransform);
    }

	public void reverseGravityKeys() {
		KeyCode oldCounterClockwiseRotationKey = counterClockwiseRotationKey;
		counterClockwiseRotationKey = clockwiseRotationKey;
		clockwiseRotationKey = oldCounterClockwiseRotationKey;

	}


	void InitiateCooldown() {
	
		onCooldown = true; 
	}

	void TerminateCooldown() {
		onCooldown = false;
	}
}