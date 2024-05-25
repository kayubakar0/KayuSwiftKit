using UnityEngine;
using UnityEngine.UI;

namespace KayuSwiftKit.Canvas_UI
{
    public class KyCameraZoomUsingSlider : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Slider zoomSlider;
        private float minFOV = 20f;
        private float maxFOV = 60f;

        void Update()
        {
            float zoomValue = 1 - zoomSlider.value;
            mainCamera.fieldOfView = Mathf.Lerp(minFOV, maxFOV, zoomValue);
        }
    }
}
