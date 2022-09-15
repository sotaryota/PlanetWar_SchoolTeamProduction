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
        //�t�F�[�h�I�����
        if (clickflag && fadeScript.FadeOut())
        {
            //�f�o�b�O���[�h�łȂ����
            //if (UnityEditor.EditorApplication.isPlaying) { UnityEditor.EditorApplication.isPlaying = false; }
            //�Q�[���I��
            //else { 
                Application.Quit(); 
            //}
        }
    }

    public void OnClick()
    {
        //Unity��Ŏ��s���Ă��邩
        if (!clickflag)
        {
            clickflag = true;
            fadeScript.fademode = true;
            audioSource.PlayOneShot(se);
            audioSource.PlayOneShot(voice);
        }
    }
}