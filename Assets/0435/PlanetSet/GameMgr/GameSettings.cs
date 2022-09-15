using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [System.Serializable]
    public struct Data
    {
        public Material skybox;
        public AudioClip BGM;
    }

    [Header("���}��")]
    [SerializeField]
    private Data[] data = new Data[1];

    [Header("AudioSource")]
    [SerializeField]
    private AudioSource audioSource; 

    private void Awake()
    {
        //�����_���ŃZ�b�g��I��
        int num = Random.Range(0, data.Length);
        //�X�J�C�{�b�N�X�ύX
        RenderSettings.skybox = data[num].skybox;
        //BGM��炷
        audioSource.clip = data[num].BGM;
        audioSource.Play();
    }

}
