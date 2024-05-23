using UnityEngine;
using System.Collections;

namespace KayuSwiftKit.Physic
{
    public class Ky3DFieldOfView : MonoBehaviour
    {
        public float outerRadius;
        public float innerRadius;

        [Range(0,360)]
        public float angle;

        public GameObject playerRef;

        public LayerMask targetMask;
        public LayerMask obstructionMask;

        public bool canSeePlayer;

        public bool isOnOuterDetection;
        public bool isOnInnerDetection;

        private void Start()
        {
            playerRef = GameObject.FindGameObjectWithTag("Player");
            StartCoroutine(FOVRoutine());
        }

        private IEnumerator FOVRoutine()
        {
            WaitForSeconds wait = new WaitForSeconds(0.2f);

            while (true)
            {
                yield return wait;
                FieldOfViewCheck();
            }
        }

        private void FieldOfViewCheck()
        {
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, outerRadius, targetMask);
    
            if (rangeChecks.Length != 0)
            {
                Transform target = rangeChecks[0].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;
    
                if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);
    
                    if (distanceToTarget <= outerRadius && distanceToTarget > innerRadius)
                    {
                        if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                        {
                            canSeePlayer = true;
                            // in Outer Radius
                            isOnOuterDetection = true;
                            isOnInnerDetection = false;
                        }
                        else
                        {
                            canSeePlayer = false;
    
                            isOnOuterDetection = false;
                            isOnInnerDetection = false;
                        }
                    }
                    else if (distanceToTarget <= innerRadius)
                    {
                        canSeePlayer = true;
                        //In Inner Radius
                        isOnOuterDetection = false;
                        isOnInnerDetection = true;
                    }
                    else
                    {
                        canSeePlayer = false;
                        isOnOuterDetection = false;
                        isOnInnerDetection = false;
                    }
    
                    Debug.DrawLine(transform.position, target.position, Color.green);
                }
                else
                {
                    canSeePlayer = false;
                    isOnOuterDetection = false;
                    isOnInnerDetection = false;
                }
            }
            else if (canSeePlayer)
            {
                canSeePlayer = false;
                isOnOuterDetection = false;
                isOnInnerDetection = false;
            }
        }
    }
}
