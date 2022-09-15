using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Button : MonoBehaviour
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
        //フェード終わって
        if (clickflag && fadeScript.FadeOut())
        {
            //デバッグモードでなければ
            //if (UnityEditor.EditorApplication.isPlaying) { UnityEditor.EditorApplication.isPlaying = false; }
            //ゲーム終了
            //else { 
                Application.Quit(); 
            //}
        }
    }

    public void OnClick()
    {
        //Unity上で実行しているか
        if (!clickflag)
        {
            clickflag = true;
            fadeScript.fademode = true;
            audioSource.PlayOneShot(se);
            audioSource.PlayOneShot(voice);
        }
    }
}