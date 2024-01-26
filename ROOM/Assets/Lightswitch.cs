using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightSwitch : MonoBehaviour
{
    public Light lightSource;
    
    public InputActionReference action;


    void Start()
    {
        lightSource = GetComponent<Light>();
    }

    void Update()
    {
        action.action.Enable();
        action.action.performed += (ctx) =>
        {
            lightSource.color = Color.red;
        };
    }
}
