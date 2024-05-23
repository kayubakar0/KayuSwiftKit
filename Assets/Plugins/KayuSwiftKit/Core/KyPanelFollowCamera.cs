using UnityEngine;

namespace KayuSwiftKit.Core
{
    public class KyPanelFollowCamera : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera; // Camera to Follow
        
        [SerializeField] private float distance = 3.0f; //Distance Z value camera and Canvas
        [SerializeField]private float smoothTime = 0.3f; //time to recenter delay
        
        private Vector3 _velocity = Vector3.zero;
    
        void Update()  
        {       
            // Calculate the target position in front of the camera based on the camera's forward direction  
            Vector3 targetPosition = mainCamera.transform.position + mainCamera.transform.forward * distance;  
  
            // Keep the Y position of the GameObject unchanged  
            targetPosition.y = mainCamera.transform.position.y;  
  
            // Smoothly move the GameObject to the target position  
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
        
            // Calculate the target rotation without changing the Y rotation  
            Quaternion targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, mainCamera.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);  
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothTime);  
        }
    }
}
