using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GravityCooldownButtonController : MonoBehaviour {

	public Image cooldownButton1;
	public Text turnsLeft;
	public Color cooldownColour;
	public Color readyColour;
	public Color offColour;


	public float cooldownTime;


	public GravityController gravityController;
	public Player player;

	// Use this for initialization
	void Start () {

//		cooldownTime = gravityController.cooldownTime;
		cooldownReady ();

	
	}
	
	// Update is called once per frame
	void Update () {



		if (player.IsGrounded()) {
			turnsLeft.text = "";

			if (gravityController.OnCooldown && player.GravityRotationLimit > 0) {
				cooldownButton1.color = Color.Lerp (offColour, cooldownColour, cooldownTime);
			} else if (!gravityController.OnCooldown && player.GravityRotationsLeft()) {
				cooldownReady ();

			}
		
		} else {
			turnsLeft.text = player.GravityRotationLimit.ToString();
			if (player.GravityRotationLimit > 0) {
				cooldownButton1.color = cooldownColour;
			} else {
				cooldownButton1.color = offColour;
			}
		}



		if (!player.GravityRotationsLeft ()) {
		
			cooldownButton1.color = offColour;
		}


		

	}

	void cooldownReady(){
	
		cooldownButton1.color = readyColour;

	}
}
