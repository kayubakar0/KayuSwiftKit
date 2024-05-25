using TMPro;
using UnityEngine;

namespace KayuSwiftKit.Canvas_UI
{
    public class KySimpleFpsDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text fpsText;
        private float deltaTime;

        private void LateUpdate()
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
            fpsText.text = "FPS: " + Mathf.Round(fps);
        }
    }
}
