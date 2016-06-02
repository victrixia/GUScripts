using UnityEngine;
using System.Collections;
using System;
//using DG.Tweening;

public class DemoSwitchable : Switchable {

    [SerializeField]
    Color onColor = Color.green;
    [SerializeField]
    Color offColor = Color.red;

    Renderer rend;
    Light spotLight;

    void Start () {
        rend = GetComponent<Renderer> ();
        spotLight = GetComponent<Light> ();

        rend.material.color = onColor;
        spotLight.enabled = true;
        //transform.DOMove (new Vector3 (0, 10, 0), 1).SetRelative ();
    }



    public override void OnSwitch (bool on) {
        if (on) {
            rend.material.color = onColor;
            spotLight.enabled = true;
            //transform.DOMove (new Vector3 (0, 0, 10), 10).SetRelative ();
        }
        if (!on) {
            rend.material.color = offColor;
            spotLight.enabled = false;
            //transform.DOMove (new Vector3 (0, 0, -10), 10).SetRelative ();
        }
    }

    public override void Init () {
        rend.material.color = offColor;
        spotLight.enabled = false;
    }
}
