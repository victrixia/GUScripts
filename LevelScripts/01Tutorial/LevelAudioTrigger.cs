using UnityEngine;
using System.Collections;

namespace Level01 {
    [RequireComponent(typeof(BoxCollider))]
    public class LevelAudioTrigger : MonoBehaviour {
        [SerializeField]
        string soundNameToPlay;

        bool isFirstTrigger = true;

        void OnTriggerEnter (Collider collider) {
            if (isFirstTrigger) {
                isFirstTrigger = false;
                AudioManager.Instance.PlaySound2D (soundNameToPlay);
            }
        }
    }
}
