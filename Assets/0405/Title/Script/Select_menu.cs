using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;//

public class Select_menu : MonoBehaviour
{
    Button start;
    Button end;

    AudioSource audioSource;
    public AudioClip selectSound;

    GameObject selectedButton;//選択中のボタンを格納

    void Start()
    {
        // ボタンコンポーネントの取得
        start = GameObject.Find("/Canvas/start").GetComponent<Button>();
        end = GameObject.Find("/Canvas/end").GetComponent<Button>();
        //オーディオソースの取得
        audioSource = GetComponent<AudioSource>();
        // 最初に選択状態にしたいボタンの設定
        start.Select();
    }

    void Update()
    {
        if (selectedButton != EventSystem.current.currentSelectedGameObject)
        {
            //選択中のものが変わったら音を鳴らす
            selectedButton = EventSystem.current.currentSelectedGameObject;
            audioSource.PlayOneShot(selectSound);
        }
    }
}
