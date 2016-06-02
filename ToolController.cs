using UnityEngine;
using System.Collections;

public class ToolController : MonoBehaviour {
    [Tooltip("Name of the animation used for putting tool away")]
    [SerializeField]
    string toolDownAnim = "ToolDown";
    [Tooltip ("Name of the animation used for picking tool up")]
    [SerializeField]
    string toolUpAnim = "ToolUp";
    [SerializeField]
    GameObject toolUI;

    Animator anim;
    Renderer[] renderers;

    void Awake () {
        anim = GetComponent<Animator> ();
        renderers = GetComponentsInChildren<Renderer> ();
	}

    void OnEnable () {
        EventHandler.RegisterEvent (Events.Type.OnGravityToolEnable, ToolUp);
        EventHandler.RegisterEvent (Events.Type.OnGravityToolDisable, ToolDown);
    }

    void OnDisable () {
        EventHandler.UnregisterEvent (Events.Type.OnGravityToolEnable, ToolUp);
        EventHandler.UnregisterEvent (Events.Type.OnGravityToolDisable, ToolDown);
    }

    public void ShowTool () {
        ShowRenderers (true);
        if (toolUI) toolUI.SetActive (true);
    }

    public void HideTool () {
        ShowRenderers (false);
        if (toolUI) toolUI.SetActive (false);
    }

    void ShowRenderers (bool value) {
        foreach (var renderer in renderers) {
            renderer.enabled = value;
        }
    }

    void ToolDown () {
        anim.Play (toolDownAnim);
    }

    void ToolUp () {
        anim.Play (toolUpAnim);
    }
}
