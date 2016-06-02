using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	[SerializeField]
	Canvas restartMenu;
	[SerializeField]
	PlayerController playerController;
	[SerializeField]
	GravityController gravityController;

    bool menuOpen = false;

    void Awake () {
        Time.timeScale = 1f;
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (menuOpen) ClosePauseMenu ();
            else OpenPauseMenu ();
        }

        if (menuOpen && Input.GetKeyDown(KeyCode.J)) {
            playerController.AutoJump = !playerController.AutoJump;
        }
    }

    void OnEnable () {
        EventHandler.RegisterEvent (Events.Type.OnMenuOpen, OpenMenu);
        EventHandler.RegisterEvent (Events.Type.OnMenuClose, CloseMenu);
    }

    void OnDisable () {
        EventHandler.UnregisterEvent (Events.Type.OnMenuOpen, OpenMenu);
        EventHandler.UnregisterEvent (Events.Type.OnMenuClose, CloseMenu);
    }

    public void OpenMenu () {
        playerController.MouseLook.HideCursor (false);
        Time.timeScale = 0.0f;
    }

    public void CloseMenu () {
        playerController.MouseLook.HideCursor (true);
        Time.timeScale = 1f;
    }


    void OpenPauseMenu () {
        restartMenu.gameObject.SetActive (true);
        menuOpen = true;
        OpenMenu ();
    }

    void ClosePauseMenu () {
        restartMenu.gameObject.SetActive (false);
        menuOpen = false;
        CloseMenu ();
    }

	public void RestartGame() {
		Time.timeScale = 1f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void CancelMenu() {
        ClosePauseMenu ();
	}

	public void ExitGame() {
        SceneManager.LoadScene (0);
	}

	public void ReverseGravityControls() {
		gravityController.reverseGravityKeys ();
	}
}
