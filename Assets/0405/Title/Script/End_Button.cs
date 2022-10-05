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
        //�t�F�[�h�I�����
        if (fadeScript.FadeOut())
        {
            //�f�o�b�O���[�h�łȂ����
            //if (UnityEditor.EditorApplication.isPlaying) { UnityEditor.EditorApplication.isPlaying = false; }
            //�Q�[���I��
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