using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using TMPro;

// 簡易説明
//・SetActiveで会話を読み込ませますので"SetText(npc.tale[0])"とかしてください。
//・その後、npc.flagをtrueにすると喋りだします。

public class Write_Effect : MonoBehaviour
{
    //テキストボックス
    [SerializeField]
    Text textObject = default;
    //表示非表示の際に使用する。キャンバスとかパネルとかを指定
    [SerializeField]
    GameObject canvas = default;
    //テキストボックスに入れる文字列
    string text;
    [Header("文字送り時間")]
    [SerializeField]
    float feedTime = 0.1f;
    [Header("改行時間")]
    [SerializeField]
    float nTime = 0.5f;
    //表示する文字数
    int visibleLength;
    //会話中かのフラグ
    bool isTalking;
    //勝手に追加
    [SerializeField] NPCClass npc;

    void Start()
    {
        visibleLength = 0;
        isTalking = false;
        canvas.SetActive(false);

        //テスト用
        SetText(npc.talk[0]);
    }

    void Update()
    {
        //会話中じゃないとき、話しかけたら
        if (!isTalking && npc.flag)
        {
            //テスト用
            //SetText(npc.talk[0]);

            //テキストボックス表示
            canvas.SetActive(true);
            //文字送り開始
            StartCoroutine("TextDisplay");
            isTalking = true;
        }
    }
    //これでテキストを更新する
    public void SetText(string newtext)
    {
        this.text = newtext;
        visibleLength = 0;
        //現在のテキストを消去
        textObject.text = "";
    }
    IEnumerator TextDisplay()
    {
        for (int i = 1; i <= npc.talk.Length; ++i)
        {
            //出てない文字があれば
            while (visibleLength < text.Length)
            {
                //Debug.Log(text);
                yield return new WaitForSeconds(feedTime);
                // 1文字ずつ増やす
                visibleLength++;
                textObject.text = text.Substring(0, visibleLength);
            }
            //会話終了
            if (i == npc.talk.Length)
            {
                isTalking = false;
                npc.flag = false;
                //テキストボックス非表示
                yield return new WaitForSeconds(nTime);
                canvas.SetActive(false);
                break;
            }
            else
            {
                yield return new WaitForSeconds(nTime);
                SetText(npc.talk[i]);
                //Debug.Log(text);
            }
        }
    }
}
