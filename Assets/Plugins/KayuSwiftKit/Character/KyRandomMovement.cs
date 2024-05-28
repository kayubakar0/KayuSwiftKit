using System.Collections;
using UnityEngine;

namespace KayuSwiftKit.Character
{
    public class KyRandomMovement : MonoBehaviour
    {
        [SerializeField] private float moveRange = 5f;
        [SerializeField] private float moveDuration = 3f;
        [SerializeField] private float waitTime = 4f;

        private Vector3 startPosition;

        private bool _isStop;

        private void Start() {
            startPosition = transform.position;

            StartCoroutine(MoveRandom());
        }

        public void ActiveRandomMovement(bool isStop)
        {
            if (isStop)
            { 
                _isStop = true;
            }
            else
            {
                _isStop = false;
                StartCoroutine(MoveRandom());
            }
        }

        IEnumerator MoveRandom()
        {
            while (true)
            {
                Vector3 targetPosition = startPosition + new Vector3(Random.Range(-moveRange, moveRange), 0, Random.Range(-moveRange, moveRange));

                Vector3 initialPosition = transform.position;

                float elapseTime = 0;

                Vector3 direction = (targetPosition - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

                while(elapseTime < moveDuration)
                {
                    transform.position = Vector3.Lerp(initialPosition, targetPosition, elapseTime / moveDuration);

                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, elapseTime / moveDuration);
                    elapseTime += Time.deltaTime;
                    yield return null;
                }

                transform.position = targetPosition;
                transform.rotation = lookRotation;

                yield return new WaitForSeconds(waitTime);
                
                if (_isStop)
                {
                    yield break;
                }
            }
        }
    }
}
