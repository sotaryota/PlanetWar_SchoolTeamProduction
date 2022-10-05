using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial_Button : OnClickBase
{
    [SerializeField]
    private Fade fadeScript;
    [SerializeField]
    AudioSource audioSource;
    public AudioClip se;
    public AudioClip voice;

    // Update is called once per frame
    void Update()
    {
        if (fadeScript.FadeOut())
        {
            SceneManager.LoadScene("Tutorial");
        }
    }
    public override void OnClick()
    {
        fadeScript.fademode = true;
        audioSource.PlayOneShot(se);
        audioSource.PlayOneShot(voice);
    }
}
