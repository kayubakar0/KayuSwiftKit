using System.Collections.Generic;
using KayuSwiftKit.Core;
using UnityEngine;
using UnityEngine.Events;

namespace KayuSwiftKit.Physic
{
    public class PyMultiObjectSocket : MonoBehaviour
    {
        // Collider for the socket
    public Collider socketCollider;

    // Maximum distance for attaching objects to the socket
    public float maxAttachDistance = 0.1f;

    // Objects currently attached to the socket
    private GameObject[] attachedObjects = new GameObject[0];

    // Array of custom transforms for attaching objects
    public Transform[] customTransforms;

    [SerializeField] private string[] tagName;

    private List<GameObject> _savedObjectAttach = new List<GameObject>();

    // Custom Events
    [Header("Custom Event")]
    [SerializeField] private UnityEvent OnTriggerEnterEvent;

    [SerializeField] private UnityEvent OnTriggerExitEvent;

    // Attach an object to the socket on trigger collision
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagName[0]) || other.CompareTag(tagName[1]))
        {
            AttachObject(other.gameObject);

            if (!_savedObjectAttach.Contains(other.gameObject))
            {
                _savedObjectAttach.Add(other.gameObject);
            }

            other.GetComponent<Rigidbody>().isKinematic = true;
            OnTriggerEnterEvent?.Invoke();
        }
    }

    // Detach an object from the socket when the object leaves the trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagName[0]) || other.CompareTag(tagName[1]))
        {
            other.GetComponent<Rigidbody>().isKinematic = false;

            if (_savedObjectAttach.Contains(other.gameObject))
            {
                _savedObjectAttach.Remove(other.gameObject);
            }

            DetachObject(other.gameObject);
            OnTriggerExitEvent?.Invoke();
        }
    }

    // Attach an object to the socket
    public void AttachObject(GameObject obj)
    {
        // Check if the object is not already attached
        if (!IsAttached(obj))
        {
            // Add the object to the list of attached objects
            GameObject[] newArray = new GameObject[attachedObjects.Length + 1];
            for (int i = 0; i < attachedObjects.Length; i++)
            {
                newArray[i] = attachedObjects[i];
            }
            newArray[newArray.Length - 1] = obj;
            attachedObjects = newArray;

            // Get the index of the custom transform to be used
            int index = attachedObjects.Length - 1;
            if (index < customTransforms.Length && customTransforms[index] != null)
            {
                // Set the position and rotation of the object using the custom transform
                obj.transform.position = customTransforms[index].position;
                obj.transform.rotation = customTransforms[index].rotation;
            }
            else
            {
                KyDebugLogger.LogDebugColor("Custom transform not defined for attached object index: " + index, Color.red);
            }
        }
    }

    // Detach an object from the socket
    public void DetachObject(GameObject obj)
    {
        // Check if the object is in the list of attached objects
        if (IsAttached(obj))
        {
            // Create a new array without the object to be detached
            GameObject[] newArray = new GameObject[attachedObjects.Length - 1];
            int newArrayIndex = 0;
            for (int i = 0; i < attachedObjects.Length; i++)
            {
                if (attachedObjects[i] != obj)
                {
                    newArray[newArrayIndex] = attachedObjects[i];
                    newArrayIndex++;
                }
            }
            attachedObjects = newArray;
        }
    }

    // Check if an object is attached to the socket
    public bool IsAttached(GameObject obj)
    {
        foreach (GameObject attachedObject in attachedObjects)
        {
            if (attachedObject == obj)
            {
                return true;
            }
        }
        return false;
    }

    // Update every frame
    private void FixedUpdate()
    {
        // Check interaction with attachable objects
        foreach (GameObject obj in attachedObjects)
        {
            // Get the index of the custom transform to be used
            int index = System.Array.IndexOf(attachedObjects, obj);
            if (index < customTransforms.Length && customTransforms[index] != null)
            {
                // Calculate desired position and rotation for the object
                Vector3 desiredPosition = customTransforms[index].position;
                Quaternion desiredRotation = customTransforms[index].rotation;

                // Set the position and rotation of the object
                obj.transform.position = Vector3.Lerp(obj.transform.position, desiredPosition, Time.deltaTime * 10f);
                obj.transform.rotation = Quaternion.Lerp(obj.transform.rotation, desiredRotation, Time.deltaTime * 10f);
            }
            else
            {
                // Debug.LogWarning("Custom transform not defined for attached object index: " + index);
            }
        }
    }

    // Custom script for gondola
    public void AttachParentGameObject(Transform parentTransform)
    {
        foreach (var objectSaved in _savedObjectAttach)
        {
            objectSaved.transform.SetParent(parentTransform);
        }
    }

    public void DetachParentGameObject()
    {
        foreach (var objectSaved in _savedObjectAttach)
        {
            objectSaved.transform.SetParent(null);
        }
    }

    // Method to display gizmo in the editor
#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        // Display gizmo for the socket collider
        if (socketCollider != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(socketCollider.bounds.center, socketCollider.bounds.size);
        }
    }
#endif
    }
}
