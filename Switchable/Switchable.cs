using UnityEngine;
using System.Collections;

public abstract class Switchable : MonoBehaviour {

    public abstract void OnSwitch (bool on);
    public abstract void Init ();
}
