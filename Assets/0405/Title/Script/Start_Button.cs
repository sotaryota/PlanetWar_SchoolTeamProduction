using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Button : MonoBehaviour
{
    [SerializeField]
    private Fade fadeScript;
    private bool clickflag;

    AudioSource audioSource;
    public AudioClip se;
    public AudioClip voice;

    void Start()
    {
        clickflag = false;
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (clickflag && fadeScript.FadeOut())
        {
            SceneManager.LoadScene("Main");
        }
    }

    public void OnClick()
    {
        //Unityè„Ç≈é¿çsÇµÇƒÇ¢ÇÈÇ©
        if (!clickflag)
        {
            clickflag = true;
            fadeScript.fademode = true;
            audioSource.PlayOneShot(se);
            audioSource.PlayOneShot(voice);
        }
    }
}
