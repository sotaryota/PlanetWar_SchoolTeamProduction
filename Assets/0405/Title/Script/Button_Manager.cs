using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Manager : MonoBehaviour
{
    Button[] buttons;
    bool nowSelecting;
    void Start()
    {
        buttons[0] = GameObject.Find("/Canvas/Start_Button").GetComponent<Button>();
        buttons[1] = GameObject.Find("/Canvas/Tutorial_Button").GetComponent<Button>();
        buttons[2] = GameObject.Find("/Canvas/End_Button").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
