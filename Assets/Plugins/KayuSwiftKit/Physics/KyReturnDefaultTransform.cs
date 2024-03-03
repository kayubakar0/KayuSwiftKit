using UnityEngine;

namespace Plugins.KayuSwiftKit.Physics
{
    public class KyReturnDefaultTransform : MonoBehaviour
    {
        [SerializeField] private Transform defaultPosition;  
  
        private bool _isSnap; 
        
        public void ReturnTransform()
        {
            if (_isSnap) return;
            
            transform.position = defaultPosition.position;
            transform.rotation = defaultPosition.rotation;
            transform.localScale = defaultPosition.localScale;
        }
    
        public void EnableSnap()  
        {  
            _isSnap = true;  
        }
    }
}
