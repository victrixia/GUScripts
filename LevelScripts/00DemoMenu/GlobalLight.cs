using UnityEngine;
using System.Collections;

namespace Level01 {
    public class GlobalLight : MonoBehaviour {
        [SerializeField]
        Light globalLight;
        [SerializeField]
        float targetIntensity;
        [SerializeField]
        AnimationCurve curve;

        float currentTarget;

        float timeTemp;

        bool turnOn = false;
 
        void Start () {
            timeTemp = 0f;
            currentTarget = globalLight.intensity;
        }

        void Update () {
            if (turnOn) {
                timeTemp += Time.deltaTime;
                globalLight.intensity = Mathf.Lerp (globalLight.intensity, currentTarget, curve.Evaluate (timeTemp));
            }
        }

        void OnTriggerEnter (Collider collider) {
            if (collider.gameObject.GetComponent<Player> ()) {
                currentTarget = targetIntensity;
                timeTemp = 0f;
                turnOn = true;
            }
        }
    }

}