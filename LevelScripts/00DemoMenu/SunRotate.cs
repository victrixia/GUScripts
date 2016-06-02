using UnityEngine;
using System.Collections;

public class SunRotate : MonoBehaviour {

    [SerializeField]
    Vector3 speed;
	
	void Update () {
        transform.Rotate (speed * Time.deltaTime);
	}
}
