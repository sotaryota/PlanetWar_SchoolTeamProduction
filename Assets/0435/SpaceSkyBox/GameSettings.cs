using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [System.Serializable]
    public struct Data
    {
        [Header("�X�J�C�{�b�N�X�i�K�{�j")]
        public Material skybox;
        [Header("BGM�i�Ȃ��Ă��j")]
        public AudioClip BGM;
    }

    [Header("���}��")]
    [SerializeField]
    private Data[] data = new Data[1];

    [Header("AudioSource�i�Ȃ��Ă��j")]
    [SerializeField]
    private AudioSource audioSource; 

    private void Awake()
    {
        //�����_���ŃZ�b�g��I��
        int num = Random.Range(0, data.Length);
        //�X�J�C�{�b�N�X�ύX
        RenderSettings.skybox = data[num].skybox;
        //BGM��炷
        if (audioSource)
        {
            if (data[num].BGM)
            {
                audioSource.clip = data[num].BGM;
            }
            audioSource.Play();
        }
    }
}
