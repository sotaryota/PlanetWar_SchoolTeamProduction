using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GoMenu : MonoBehaviour
{
    [SerializeField]
    private Fade fadeScript;
    Gamepad gamepad;

    [SerializeField]
    AudioSource audioSource;
    public AudioClip se;
    public AudioClip voice;
    [SerializeField] private bool buttonFlag;

    void Update()
    {
        if (buttonFlag)
        {
            for (int i = 0; i < Gamepad.all.Count; ++i)
            {
                gamepad = Gamepad.all[i];
                //��������t�F�[�h�J�n
                if (gamepad.buttonEast.wasPressedThisFrame || gamepad.startButton.wasPressedThisFrame)
                {
                    buttonFlag = false;
                    fadeScript.fademode = true;
                    audioSource.PlayOneShot(se);
                    //audioSource.PlayOneShot(voice);
                }
            }
        }
        //�t�F�[�h�I�������J��
        if (fadeScript.FadeOut())
        {
            SceneManager.LoadScene("Menu");
        }

    }
}
