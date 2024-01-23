using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerViewSwitch : MonoBehaviour
{
    public Transform roomTransform; // Set this to the initial position within the room
    public Transform externalViewTransform; // Set this to the external viewing point

    private bool inRoom = true;

    // Assuming you have a button input named "SwitchViewButton". Adjust based on your setup.
    private string switchViewButton = "SwitchViewButton";

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial position to the room
        transform.position = roomTransform.position;
        transform.rotation = roomTransform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(switchViewButton))
        {
            ToggleView();
        }
    }

    void ToggleView()
    {
        if (inRoom)
        {
            SwitchToExternalView();
        }
        else
        {
            SwitchToRoomView();
        }
    }

    void SwitchToExternalView()
    {
        transform.position = externalViewTransform.position;
        transform.rotation = externalViewTransform.rotation;
        inRoom = false;
    }

    void SwitchToRoomView()
    {
        transform.position = roomTransform.position;
        transform.rotation = roomTransform.rotation;
        inRoom = true;
    }
}