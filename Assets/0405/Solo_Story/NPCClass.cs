using UnityEngine;

public class NPCClass : MonoBehaviour
{
    //エディター用にpublicにしています
    //読み取り書き換えは基本getsetを使え
    [Header("NPCプロパティ")]
    public int       id;           //ID
    public bool      talkFlag;     //話しかけられるフラグ
    public string    name;         //名前

#pragma warning disable CS8632 // '#nullable' 注釈コンテキスト内のコードでのみ、Null 許容参照型の注釈を使用する必要があります。
    public string?[] normalTalk;   //通常会話内容
    public string?[] battlelTalk;  //戦闘会話内容
    public string?[] friendTalk;   //友好会話内容
#pragma warning restore CS8632 // '#nullable' 注釈コンテキスト内のコードでのみ、Null 許容参照型の注釈を使用する必要があります。

    public enum NPCState
    { 
        Normal,   //通常会話
        Select,   //セレクト状態
        Battle,   //バトル状態
        Friend,   //友好状態
        End,      //会話終了

        NonEvent, //イベントなし

        ENUMEND   //enumの最終値
    };
    [Header("NPCの状態")]
    public NPCState firstState;
    public NPCState nowState;

    public NPCState GetState()
    {
        return nowState;
    }

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
        return this.normalTalk;
    }

    public void SetTalkFlag(bool flag)
    {
        this.talkFlag = flag;
    }
}
