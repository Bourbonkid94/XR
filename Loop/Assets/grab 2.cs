using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace YourNamespace1
{
    public class CustomGrab : MonoBehaviour
    {
        CustomGrab otherHand = null;
        public List<Transform> nearObjects = new List<Transform>();
        public Transform grabbedObject = null;
        public InputActionReference action;
        bool grabbing = false;

        Vector3 initialControllerPosition;
        Quaternion initialControllerRotation;

        void Start()
        {
            action.action.Enable();
            FindOtherHand();
        }

        void FindOtherHand()
        {
            foreach (CustomGrab c in transform.parent.GetComponentsInChildren<CustomGrab>())
            {
                if (c != this)
                {
                    otherHand = c;
                    otherHand.action.action.Enable();
                }
            }
        }

        void Update()
        {
            grabbing = action.action.IsPressed();

            if (grabbing)
            {
                grabbedObject = GetNearestObject();

                if (grabbedObject)
                {
                    Vector3 deltaPosition = transform.position - initialControllerPosition;
                    Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(initialControllerRotation);

                    ApplyTransformChanges(deltaPosition, deltaRotation);
                    // Commented out for testing without the second hand
                    // UpdateOtherHand();
                }

                SaveControllerTransform();
            }
            else if (grabbedObject)
            {
                grabbedObject = null;
            }
        }

        void ApplyTransformChanges(Vector3 deltaPosition, Quaternion deltaRotation)
        {
            if (grabbedObject)
            {
                Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.MovePosition(grabbedObject.position + deltaPosition);
                    rb.MoveRotation(deltaRotation * grabbedObject.rotation);
                }
                else
                {
                    grabbedObject.position += deltaPosition;
                    grabbedObject.rotation = deltaRotation * grabbedObject.rotation;
                }
            }
        }

        // Commented out for testing without the second hand
        /*
        void UpdateOtherHand()
        {
            if (otherHand)
            {
                otherHand.grabbedObject = grabbedObject;
                otherHand.ApplyTransformChanges(Vector3.zero, Quaternion.identity); // Reset other hand's delta changes
            }
        }
        */

        void SaveControllerTransform()
        {
            initialControllerPosition = transform.position;
            initialControllerRotation = transform.rotation;
        }

        Transform GetNearestObject()
        {
            if (nearObjects.Count > 0)
                return nearObjects[0];
            else if (otherHand && otherHand.grabbedObject)
                return otherHand.grabbedObject;
            else
                return null;
        }

        private void OnTriggerEnter(Collider other)
        {
            Transform t = other.transform;
            if (t && t.CompareTag("Grabbable"))
                nearObjects.Add(t);
        }

        private void OnTriggerExit(Collider other)
        {
            Transform t = other.transform;
            if (t && t.CompareTag("Grabbable"))
                nearObjects.Remove(t);
        }
    }
}