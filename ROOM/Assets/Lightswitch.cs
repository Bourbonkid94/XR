using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public Light Lightswitch; 

    // Start is called before the first frame update
    void Start()
    {
        Lightswitch = GetComponent<Light>(); // Corrected variable name
        if (Input.GetKeyDown("l"))
        {
            Lightswitch.color = new Color(1f, 0f, 1f, 1f); 
        }
    }
}