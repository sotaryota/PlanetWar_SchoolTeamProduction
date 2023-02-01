using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class BackTitle : MonoBehaviour
{
    [SerializeField]
    private Fade fadeScript;
    Gamepad gamepad;

    void Update()
    {
        gamepad = Gamepad.current; 
        if (gamepad.buttonEast.isPressed)
        {
            fadeScript.fademode = true;
        }
        else if (fadeScript.FadeOut())
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
