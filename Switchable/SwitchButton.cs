using UnityEngine;
using System.Collections;

public class SwitchButton : MonoBehaviour {

    [SerializeField]
    Switchable effectedObject;
    [SerializeField]
    bool onByDefault = false;
    [SerializeField]
    Color onColor = Color.green;
    [SerializeField]
    Color offColor = Color.red;

    bool on;

    Renderer rend;

    void Start () {
        rend = GetComponent<Renderer> ();
        on = onByDefault;
        effectedObject.Init ();
	}
	
	void Update () {
        if (on) rend.material.SetColor("_EmissionColor", onColor);
        else rend.material.SetColor ("_EmissionColor", offColor);
    }

    public void Toggle () {
        on = !on;
        effectedObject.OnSwitch (on);
    }
}
