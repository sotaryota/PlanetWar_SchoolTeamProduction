using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Button : OnClickBase
{
    [SerializeField]
    private Fade fadeScript;
    [SerializeField]
    AudioSource audioSource;
    public AudioClip se;
    public AudioClip voice;

    void Update()
    {
        if (fadeScript.FadeOut())
        {
            SceneManager.LoadScene("Main");
        }
    }

    public override void OnClick()
    {
        fadeScript.fademode = true;
        audioSource.PlayOneShot(se);
        audioSource.PlayOneShot(voice);
    }
}
