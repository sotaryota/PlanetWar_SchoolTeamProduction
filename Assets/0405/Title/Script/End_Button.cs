using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Button : OnClickBase
{
    [SerializeField]
    private Fade fadeScript;
    [SerializeField]
    AudioSource audioSource;
    public AudioClip se;
    public AudioClip voice;

    void Update()
    {
        //フェード終わって
        if (fadeScript.FadeOut())
        {
            //デバッグモードでなければ
            //if (UnityEditor.EditorApplication.isPlaying) { UnityEditor.EditorApplication.isPlaying = false; }
            //ゲーム終了
            //else { 
            Application.Quit();
            //}
        }
    }

    public override void OnClick()
    {
        fadeScript.fademode = true;
        audioSource.PlayOneShot(se);
        audioSource.PlayOneShot(voice);
    }
}