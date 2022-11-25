using UnityEngine;

public class NPCClass : MonoBehaviour
{
    //エディター用にpublicにしています
    //読み取り書き換えは基本getsetを使え
    [Header("NPCプロパティ")]
    public int      id;           //ID
    public bool     talkFlag;     //話しかけられるフラグ
    public string   name;         //名前
    public string[] talk;         //会話内容

    public enum TalkProperty
    { 
        Normal, //通常会話
        Select, //セレクト状態
        Battle, //バトル状態
        Friend, //友好状態
        End,    //会話終了

        ENUMEND //enumの最終値
    };

    //--------------------------------------
    //ゲッター
    //--------------------------------------
    public int GetID()
    {
        return this.id;
    }
    public bool GetTalkFlag()
    {
        return this.talkFlag;
    }
    public string GetName()
    {
        return this.name;
    }
    public string[] GetTalk()
    {
        return this.talk;
    }

    public void SetTalkFlag(bool flag)
    {
        this.talkFlag = flag;
    }
}
