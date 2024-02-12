using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomGrab : MonoBehaviour
{
    CustomGrab otherHand = null;
    public List<Transform> nearObjects = new List<Transform>();
    public Transform grabbedObject = null;
    public InputActionReference action;
    bool grabbing = false;

    Vector3 initialControllerPosition;
    Quaternion initialControllerRotation;

    private void Start()
    {
        action.action.Enable();

        // Trouver l'autre main
        foreach (CustomGrab c in transform.parent.GetComponentsInChildren<CustomGrab>())
        {
            if (c != this)
                otherHand = c;
        }
    }

    void Update()
    {
        grabbing = action.action.IsPressed();

        if (grabbing)
        {
            if (!grabbedObject)
                grabbedObject = nearObjects.Count > 0 ? nearObjects[0] : otherHand.grabbedObject;

            if (grabbedObject)
            {
                // Suivre la position et la rotation delta du contrôleur
                Vector3 deltaPosition = transform.position - initialControllerPosition;
                Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(initialControllerRotation);

                // Messages de log pour le débogage
                Debug.Log($"Delta Position: {deltaPosition}, Delta Rotation: {deltaRotation.eulerAngles}");

                // Appliquer les changements combinés à l'objet saisi
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

            // Sauvegarder la position et la rotation actuelles pour la prochaine frame
            initialControllerPosition = transform.position;
            initialControllerRotation = transform.rotation;
        }
        else if (grabbedObject)
        {
            grabbedObject = null;
        }
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