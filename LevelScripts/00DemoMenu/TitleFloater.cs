using UnityEngine;
using System.Collections;

namespace DemoMenu {
    public class TitleFloater : MonoBehaviour {
        [SerializeField]
        bool vertical = false;

        Vector3 startPos;

        public float amplitude = 10f;
        public float period = 5f;

        protected void Start () {
            startPos = transform.position;
        }

        protected void Update () {
            Vector3 direction = Vector3.up;
            if (vertical) direction = Vector3.right;

            float theta = Time.timeSinceLevelLoad / period;
            float distance = amplitude * Mathf.Sin (theta);
            transform.position = startPos + direction * distance;
        }
    }
}