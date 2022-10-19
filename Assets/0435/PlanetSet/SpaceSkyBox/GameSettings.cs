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

    [Header("情報挿入")]
    [SerializeField]
    private Data[] data = new Data[1];

    [Header("AudioSource")]
    [SerializeField]
    private AudioSource audioSource; 

    private void Awake()
    {
        //ランダムでセットを選択
        int num = Random.Range(0, data.Length);
        //スカイボックス変更
        RenderSettings.skybox = data[num].skybox;
        //BGMを鳴らす
        audioSource.clip = data[num].BGM;
        audioSource.Play();
    }

}
