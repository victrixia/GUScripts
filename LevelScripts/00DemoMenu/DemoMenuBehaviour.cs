using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace DemoMenu {
    public class DemoMenuBehaviour : MonoBehaviour {

        [SerializeField]
        GameObject loadingCanvas;

        void Start () {
            Time.timeScale = 1.0f;
        }

        public void Play () {
            loadingCanvas.SetActive (true);
            SceneManager.LoadScene (1);
        }

        public void Exit () {
            Application.Quit ();
        }
    }
}