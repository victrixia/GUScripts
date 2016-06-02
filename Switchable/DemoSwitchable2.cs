using UnityEngine;
using System.Collections;
using System;
//using DG.Tweening;

public class DemoSwitchable2 : Switchable {

    [SerializeField]
    Color onColor = Color.green;
    [SerializeField]
    Color offColor = Color.red;

    Renderer rend;
    Light spotLight;

    Vector3 targetPosition;
    Vector3 startPosition;

    void Start () {
        rend = GetComponent<Renderer> ();
        spotLight = GetComponent<Light> ();

        rend.material.color = onColor;
        spotLight.enabled = true;
        startPosition = transform.position;
        targetPosition = startPosition;
    }

    void FixedUpdate () {
        transform.position = Vector3.Lerp (transform.position, targetPosition, Time.deltaTime * 1f);
    }



    public override void OnSwitch (bool on) {
        if (on) {
            rend.material.color = onColor;
            spotLight.enabled = true;
            targetPosition = startPosition + new Vector3 (0f, 0f, 10f);
            //transform.DOMove (new Vector3 (0, 0, 10), 10).SetRelative ();
        }
        if (!on) {
            rend.material.color = offColor;
            spotLight.enabled = false;
            targetPosition = startPosition;
            //transform.DOMove (new Vector3 (0, 0, -10), 10).SetRelative ();
        }
    }

    public override void Init () {
        rend.material.color = offColor;
        spotLight.enabled = false;
    }
}
