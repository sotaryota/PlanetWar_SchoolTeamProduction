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
        //フェード終わったら遷移
        if (fadeScript.FadeOut())
        {
            //Menuに変えといてください
            SceneManager.LoadScene("Main");
        }

        for (int i = 0; i < Gamepad.all.Count; ++i)
        {
            gamepad = Gamepad.all[i];
            //押したらフェード開始
            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
                fadeScript.fademode = true;
                audioSource.PlayOneShot(se);
                //audioSource.PlayOneShot(voice);
            }
        }

    }
}
