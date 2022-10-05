using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSEManager : MonoBehaviour
{
    [Header("�s�b�`�ƃ{�����[��")]
    [SerializeField]
    private float pitch;
    [SerializeField]
    private float volume;

    [SerializeField]
    AudioSource audioSource;

    [Header("�{�C�X")]
    [SerializeField] //�����{�C�X
    AudioClip[] throwVoice;
    [SerializeField] //�_���[�W�{�C�X
    AudioClip[] damageVoice;
    [SerializeField] //�ҋ@�{�C�X
    AudioClip[] waitVoice;
    [SerializeField] //�����{�C�X
    AudioClip[] winVoice;
    [SerializeField] //���S�{�C�X
    AudioClip[] deathVoice;

    public void ThrowVoice() //�����{�C�X�Đ�
    {
        audioSource.pitch  = pitch;
        audioSource.volume = volume;
        audioSource.PlayOneShot(throwVoice[Random.Range(0,throwVoice.Length)]);
    }

    public void DamageVoice() //�_���[�W�{�C�X�Đ�
    {
        audioSource.pitch = pitch;
        audioSource.volume = volume;
        audioSource.PlayOneShot(damageVoice[Random.Range(0, damageVoice.Length)]);
    }

    public void WaitVoice() //�ҋ@�{�C�X�Đ�
    {
        audioSource.pitch = pitch;
        audioSource.volume = volume;
        audioSource.PlayOneShot(waitVoice[Random.Range(0, waitVoice.Length)]);
    }

    public void WinVoice() //�����{�C�X�Đ�
    {
        audioSource.pitch = pitch;
        audioSource.volume = volume;
        audioSource.PlayOneShot(winVoice[Random.Range(0, winVoice.Length)]);
    }

    public void DeathVoice() //���S�{�C�X�Đ�
    {
        audioSource.pitch = pitch;
        audioSource.volume = volume;
        audioSource.PlayOneShot(deathVoice[Random.Range(0, deathVoice.Length)]);
    }
}
