using UnityEngine;

namespace KayuSwiftKit.Line_Renderer
{
    [RequireComponent(typeof(LineRenderer))]
    public class KyLineRendererCone : MonoBehaviour
    {
        [SerializeField] private Transform _point;
        [SerializeField] private float angle;
        [SerializeField] private float distance;

        private LineRenderer _lineRenderer;

        private void Start() 
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.positionCount = 3;

        }

        private void Update() 
        {
            DrawShape();
        }

        private void DrawShape()
        {
            Vector3 startPosition = this.transform.position;

            Vector3 leftDirection = Quaternion.Euler(0, -angle/2, 0) * _point.forward * distance;
            Vector3 rightDirection = Quaternion.Euler(0, angle/2, 0) * _point.forward * distance;

            Vector3 leftPosition = startPosition + leftDirection;
            Vector3 rightPosition = startPosition + rightDirection;

            _lineRenderer.SetPosition(0, leftPosition);
            _lineRenderer.SetPosition(1, startPosition);
            _lineRenderer.SetPosition(2, rightPosition);
        }
    }
}
