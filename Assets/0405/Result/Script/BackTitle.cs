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
        if (gamepad.buttonSouth.isPressed)
        {
            fadeScript.fademode = true;
            SceneManager.LoadScene("Title");
        }
        else if (fadeScript.FadeOut())
        {
            SceneManager.LoadScene("Title");
        }
    }
}
