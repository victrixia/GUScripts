using UnityEngine;
using System.Collections;

namespace Level01 {
    public class GravityToolPickup : MonoBehaviour {
        [SerializeField]
        GameObject gravityTool;
        [Header ("Screen")]
        [SerializeField]
        Renderer screenPanel;
        [SerializeField]
        Renderer screenPicture;
        [SerializeField]
        Light screenLight;
        [SerializeField]
        Color screenDimColor;
        [Tooltip ("Time it takes for screen to light up")]
        [SerializeField]
        float screenOnSpeed = 3f;

        bool pickedUp = false;

        Color screenColor;
        Color pictureColor;
        float lightAmount;

        void Start () {
            EventHandler.ExecuteEvent (Events.Type.OnGravityToolDisable);
            screenColor = screenPanel.material.GetColor ("_EmissionColor");
            pictureColor = screenPicture.material.GetColor ("_EmissionColor");
            screenPanel.material.SetColor ("_EmissionColor", screenDimColor);
            screenPicture.material.SetColor ("_EmissionColor", new Color (0f, 0f, 0f, 0f));
            lightAmount = screenLight.intensity;
            screenLight.intensity = 0f;
        }

        void Update () {

            if (pickedUp) {
                if (screenPanel)
                    screenPanel.material.SetColor ("_EmissionColor",
                        Color.Lerp (screenPanel.material.GetColor ("_EmissionColor"), screenColor, screenOnSpeed * Time.deltaTime));
                if (screenPicture)
                    screenPicture.material.SetColor ("_EmissionColor",
                        Color.Lerp (screenPicture.material.GetColor ("_EmissionColor"), pictureColor, screenOnSpeed * Time.deltaTime));
                Color.Lerp (screenPicture.material.color, pictureColor, screenOnSpeed * Time.deltaTime);
                if (screenLight)
                    screenLight.intensity = Mathf.Lerp (screenLight.intensity, lightAmount, screenOnSpeed * Time.deltaTime);
            }

        }

        void OnTriggerEnter (Collider collider) {
            if (!pickedUp) {
                if (collider.gameObject.GetComponent<Player> ()) {
                    EventHandler.ExecuteEvent (Events.Type.OnGravityToolEnable);
                }
                if (gravityTool) gravityTool.SetActive (false);
                pickedUp = true;

            }
        }
    }
	
}
