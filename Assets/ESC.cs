using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ESC : MonoBehaviour
{
    Keyboard key;
    void Update()
    {
        key = Keyboard.current;
        if(key.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
        }
    }
}
