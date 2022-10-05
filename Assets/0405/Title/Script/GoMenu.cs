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
    void Update()
    {
        //�t�F�[�h�I�������J��
        if (fadeScript.FadeOut())
        {
            //Menu�ɕς��Ƃ��Ă�������
            SceneManager.LoadScene("Main");
        }

        for (int i = 0; i < Gamepad.all.Count; ++i)
        {
            gamepad = Gamepad.all[i];
            //��������t�F�[�h�J�n
            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
                fadeScript.fademode = true;
                audioSource.PlayOneShot(se);
                //audioSource.PlayOneShot(voice);
            }
        }

    }
}
