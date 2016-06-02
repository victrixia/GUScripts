using UnityEngine;
using System.Collections;

public class SettingsManager : MonoBehaviour {

    public static SettingsManager instance;

    // TODO Replace with real settings manager

    public float MasterVolume { get { return 1.0f; } }
    public float SfxVolume { get { return 1.0f; } }
    public float MusicVolume { get { return 1.0f; } }
    public float DialogueVolume { get { return 1.0f; } }

    public static SettingsManager Instance {
        get {
            return instance;
        }
    }

    void Awake () {
        if (instance != null) {
            Destroy (gameObject);
        }
        else if (instance == null) {
            instance = this;
        }
    }

}
