using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSEManager : MonoBehaviour
{
    [Header("�s�b�`�ƃ{�����[��")]
    [SerializeField]
    private float pitch;
    [SerializeField]
    private float volume;

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    private AudioClip selectSE;
    
    [SerializeField]
    private AudioClip decisionSE;

    public void SelectSE() //�����{�C�X�Đ�
    {
        audioSource.pitch = pitch;
        audioSource.volume = volume;
        audioSource.PlayOneShot(selectSE);
    }
    public void DecisionSE() //�����{�C�X�Đ�
    {
        audioSource.pitch = pitch;
        audioSource.volume = volume;
        audioSource.PlayOneShot(decisionSE);
    }
}
