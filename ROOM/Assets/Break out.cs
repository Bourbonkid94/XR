using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Breakout : MonoBehaviour
{
    public InputActionReference action;

    void Start()
    {

    } 

    void Update()
    {
        action.action.Enable();
        action.action.performed += (ctx) =>
        {
            if (transform.position == new Vector3(0, 0, 0))
            {
                transform.Translate(Vector3.up * 25);
            }
            else if (transform.position == new Vector3(0, 25, 0))
            {
               transform.Translate(Vector3.down * 25); 
            }
        };
    }
}