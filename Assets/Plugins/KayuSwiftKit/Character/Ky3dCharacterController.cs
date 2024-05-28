using System;
using UnityEngine;

namespace KayuSwiftKit.Character
{
    [RequireComponent(typeof(CharacterController))]
    public class Ky3dCharacterController : MonoBehaviour
    {
        [Header("Locomotion")]
        [SerializeField] private float moveSpeed = 2.0f;
        [SerializeField] private float rotationSpeed = 720f;
        private CharacterController _controller;

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
        }

        void Update()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = new Vector3(x, 0, z);

            _controller.Move(move * (moveSpeed * Time.deltaTime));

            if(move != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(move);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
