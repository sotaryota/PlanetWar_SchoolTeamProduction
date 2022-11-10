using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    Gamepad gamepad;
    bool ispauseNow;
   
    void Start()
    {
        ispauseNow = false;
    }

    void Update()
    {
        Debug.Log("gamepadのシステムが定まったら変更");
        for (int i = 0; i < Gamepad.all.Count; ++i)
        {
            gamepad = Gamepad.all[i];
            PauseSystem();
        }
    }

    void PauseSystem()
    {
        //ポーズ中でなければ
        if (!ispauseNow)
        { 
            //押したらポーズ
            if (gamepad.buttonNorth.wasPressedThisFrame)
            {
                ispauseNow = true;
                //audioSource.PlayOneShot(se);
                Time.timeScale = 0.0f;
            }
        }
    }

    //ポーズ中かどうかを返す
    public bool PauseJudge()
    {
        return ispauseNow;
    }
}