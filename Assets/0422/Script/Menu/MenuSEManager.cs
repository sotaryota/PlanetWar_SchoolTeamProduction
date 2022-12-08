using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSEManager : MonoBehaviour
{
    [Header("ピッチとボリューム")]
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

    public void SelectSE() //投げボイス再生
    {
        audioSource.pitch = pitch;
        audioSource.volume = volume;
        audioSource.PlayOneShot(selectSE);
    }
    public void DecisionSE() //投げボイス再生
    {
        audioSource.pitch = pitch;
        audioSource.volume = volume;
        audioSource.PlayOneShot(decisionSE);
    }
}
