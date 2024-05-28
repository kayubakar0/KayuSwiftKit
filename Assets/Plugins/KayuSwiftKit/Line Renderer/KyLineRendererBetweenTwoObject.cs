using UnityEngine;

namespace KayuSwiftKit.Line_Renderer
{
    public class KyLineRendererBetweenTwoObject : MonoBehaviour
    {
        public Transform object1;  // First object
        public Transform object2;  // Second object
        public LineRenderer lineRenderer;
        
        private bool _isobject1Null;
        private bool _isobject2Null;

        private void Start()
        {
            _isobject1Null = object2 == null;
            _isobject2Null = object1 == null;
            
            if (lineRenderer == null)
            {
                // If the Line Renderer doesn't already exist, add it
                lineRenderer = gameObject.AddComponent<LineRenderer>();
                lineRenderer.positionCount = 2;
            }
        }

        private void Update()
        {
            //Check if null or not
            if (_isobject1Null && _isobject2Null) return;
            
            // Update the Line Renderer position according to the position of object 1 and object 2
            lineRenderer.SetPosition(0, object1.position);
            lineRenderer.SetPosition(1, object2.position);
        }
    }
}
