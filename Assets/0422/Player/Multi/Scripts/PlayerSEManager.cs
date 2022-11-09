using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSEManager : MonoBehaviour
{
    [Header("ピッチとボリューム")]
    [SerializeField]
    private float pitch;
    [SerializeField]
    private float volume;

    [SerializeField]
    AudioSource audioSource;

    [Header("ボイス")]
    [SerializeField] //投げボイス
    AudioClip[] throwVoice;
    [SerializeField] //ダメージボイス
    AudioClip[] damageVoice;
    [SerializeField] //待機ボイス
    AudioClip[] waitVoice;
    [SerializeField] //勝ちボイス
    AudioClip[] winVoice;
    [SerializeField] //死亡ボイス
    AudioClip[] deathVoice;

    public void ThrowVoice() //投げボイス再生
    {
        audioSource.pitch  = pitch;
        audioSource.volume = volume;
        audioSource.PlayOneShot(throwVoice[Random.Range(0,throwVoice.Length)]);
    }

    public void DamageVoice() //ダメージボイス再生
    {
        audioSource.pitch = pitch;
        audioSource.volume = volume;
        audioSource.PlayOneShot(damageVoice[Random.Range(0, damageVoice.Length)]);
    }

    public void WaitVoice() //待機ボイス再生
    {
        audioSource.pitch = pitch;
        audioSource.volume = volume;
        audioSource.PlayOneShot(waitVoice[Random.Range(0, waitVoice.Length)]);
    }

    public void WinVoice() //勝ちボイス再生
    {
        audioSource.pitch = pitch;
        audioSource.volume = volume;
        audioSource.PlayOneShot(winVoice[Random.Range(0, winVoice.Length)]);
    }

    public void DeathVoice() //死亡ボイス再生
    {
        audioSource.pitch = pitch;
        audioSource.volume = volume;
        audioSource.PlayOneShot(deathVoice[Random.Range(0, deathVoice.Length)]);
    }
}
