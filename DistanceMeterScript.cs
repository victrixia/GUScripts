using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DistanceMeterScript : MonoBehaviour {

	public Text distanceMeterText;
	public Transform playerPosition;
	public Transform finishLine;

	float distanceFromEnd;


	void Update () {

		distanceFromEnd = Vector3.Distance (finishLine.position, playerPosition.position) / 2.5f;
		distanceMeterText.text = string.Format ("{0}m", (int) distanceFromEnd);

	}
}
