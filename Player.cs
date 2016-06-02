using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float movementSpeed = 9f;
	public float sprintPower = 1.5f;
	public float jumpPower = 10f;
	public float jumpPowerForward = 1.2f;
	public float jumpCooldown = 0.2f;
	public float airControl = 0.8f;
	public float maxVelocity = 8f;
	public float groundCheckDistance = 0.01f;
	public float shellOffset = 0.01f;
	public float extraGravity = 2f;
    public int gravityRotationLimitAmount = 2;

	SphereCollider sphere;
	Rigidbody rb;
	Vector3 velocity;
	float forwardAmount;
	float nextJumpTime;
	bool previouslyGrounded;
	bool grounded;
    int gravityRotationLimit;
    

	public int GravityRotationLimit {
		get { return gravityRotationLimit; }
	}

    public int GetState () {
        return 1;
    }

    bool onUphill = false;
    Quaternion upHillAngle;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		sphere = GetComponent<SphereCollider> ();
		grounded = true;
		nextJumpTime = Time.time;
        upHillAngle = transform.rotation;
        gravityRotationLimit = gravityRotationLimitAmount;
    }



    void FixedUpdate () {
        GroundCheck ();

        if (grounded) {
            rb.MovePosition (rb.position + velocity * Time.fixedDeltaTime);
        }
        else {
            rb.AddForce (velocity * 10f / airControl * Time.fixedDeltaTime);
            rb.MovePosition (rb.position + velocity * airControl * Time.fixedDeltaTime);
        }

        if (rb.velocity.sqrMagnitude > maxVelocity) {
            rb.velocity *= 0.98f;
            if (rb.velocity.sqrMagnitude > maxVelocity * 2) {
                rb.velocity *= 0.995f;
            }
        }

        ExtraGravity ();
    }
		
	public void Move (Vector3 move, float forwardAmountFromController) {
		velocity = move;
		forwardAmount = forwardAmountFromController;
        if (onUphill) {
            velocity = upHillAngle * velocity;
        }
	}

    public void Jump(Vector3 move) {
		if (Time.time > nextJumpTime && IsGrounded ()) {
			Vector3 jumpForce = new Vector3(0, jumpPower, 0);
            jumpForce = transform.TransformDirection (jumpForce);
			jumpForce += (jumpPowerForward / 8f) * velocity;
            //rb.AddForce(jumpForce, ForceMode.VelocityChange);
            rb.velocity = jumpForce;
			nextJumpTime = Time.time + jumpCooldown;
		}
	}

	/* Applies more gravity to the player than other world objects if needed */
	void ExtraGravity() {
        if (grounded) {
            extraGravity = 0.5f;
        } else {
            extraGravity = 2.5f;
        }
        rb.AddForce((Physics.gravity * extraGravity) - Physics.gravity);
	}

    public void UphillCollision(Quaternion angle) {
        upHillAngle = angle;
        onUphill = true;
    }

    public void UphillCollisionExit() {
        onUphill = false;
    }

    public bool IsGrounded () {
		return grounded;
	}

	void GroundCheck() {

		previouslyGrounded = grounded;

		RaycastHit hitInfo;

		if (Physics.SphereCast(transform.position, sphere.radius * (1.0f - shellOffset), Physics.gravity, out hitInfo,
			sphere.radius / 2 + groundCheckDistance, ~0, QueryTriggerInteraction.Ignore))
		{

			grounded = true;
			gravityRotationLimit = gravityRotationLimitAmount; // should this be here instead?
		}
		else
		{
			grounded = false;
		}
		if (previouslyGrounded == false && grounded == true) {
			nextJumpTime = Time.time + jumpCooldown;
//            gravityRotationLimit = gravityRotationLimitAmount;
		}
	}

    public void RemoveFromGravityRotationLimit() {
        gravityRotationLimit--;
    }

    public bool GravityRotationsLeft() {
        if (gravityRotationLimit > 0) {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Rotates the player when gravity changes
    /// </summary>
    /// <param name="amount"></param>
    public void GravityRotation(Quaternion amount) {
		transform.localRotation = amount * transform.localRotation;
	}

    /// <summary>
    /// Sets rotation and stops existing forces on the player
    /// </summary>
    /// <param name="targetTransform"></param>
    public void SetRotation (Transform targetTransform) {
        transform.rotation = targetTransform.rotation;
        rb.velocity = new Vector3 (0, 0, 0);
    }
}
