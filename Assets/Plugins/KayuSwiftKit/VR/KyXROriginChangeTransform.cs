using UnityEngine;

namespace KayuSwiftKit.VR
{
    // Code from Youtube Valem Tutorials
    public class KyXROriginChangeTransform : MonoBehaviour
    {
        [SerializeField, Tooltip("Main Camera transform")] 
        private Transform head;  // Main Camera Game Object
        
        [SerializeField, Tooltip("Xr Origin")] 
        private Transform origin; // XR Origin Game Object
        
        //Target
        private Transform target;  // Target point to Recenter
  
        private Vector3 _offset;  
        private Vector3 _targetForward;  
        private Vector3 _cameraForward;  
        private float _angle;  
           
        public void Recenter()
        {
            if (target == null)
            {
                return;
            }
            
            _offset = head.position - origin.position;
            _offset.y = 0;
            origin.position = target.position + _offset;
            _targetForward = target.forward;
            _targetForward.y = 0;
            _cameraForward = head.forward;
            _cameraForward.y = 0;
        
            _angle = Vector3.SignedAngle(_cameraForward, _targetForward, Vector3.up);
            origin.RotateAround(head.position, Vector3.up, _angle);
        }

        public void SetTargetAndTransform(Transform targetTransform)
        {
            target = targetTransform;
            Recenter();
        }
    }
}
