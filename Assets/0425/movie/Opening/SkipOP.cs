using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SkipOP : MonoBehaviour
{
    Gamepad gamepad;

    [SerializeField] GameObject skipDisplay;

    bool readySkip = false;
    float deleatTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        readySkip = false;
        skipDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gamepad == null) { gamepad = Gamepad.current; }


        if (gamepad.buttonSouth.wasPressedThisFrame && readySkip == true)
        {
            SceneManager.LoadScene("Story");
        }


        if (gamepad.buttonSouth.wasPressedThisFrame && readySkip == false)
        {
            readySkip = true;
        }


        if (readySkip == true)
        {
            skipDisplay.SetActive(true);
            deleatTime += Time.deltaTime;
        }
        else
        {
            skipDisplay.SetActive(false);
            deleatTime = 0;
        }

        if(deleatTime > 5)
        {
            readySkip = false;
        }

    }
}
