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
        Debug.Log("gamepad�̃V�X�e������܂�����ύX");
        for (int i = 0; i < Gamepad.all.Count; ++i)
        {
            gamepad = Gamepad.all[i];
            PauseSystem();
        }
    }

    void PauseSystem()
    {
        //�|�[�Y���łȂ����
        if (!ispauseNow)
        { 
            //��������|�[�Y
            if (gamepad.buttonNorth.wasPressedThisFrame)
            {
                ispauseNow = true;
                //audioSource.PlayOneShot(se);
                Time.timeScale = 0.0f;
            }
        }
    }

    //�|�[�Y�����ǂ�����Ԃ�
    public bool PauseJudge()
    {
        return ispauseNow;
    }
}