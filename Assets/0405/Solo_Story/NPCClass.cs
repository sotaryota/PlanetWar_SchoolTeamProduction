using UnityEngine;

public class NPCClass : MonoBehaviour
{
    //エディター用にpublicにしています
    //読み取り書き換えは基本getsetを使え
    [Header("NPCプロパティ")]
    public int       id;           //ID
    public bool      talkFlag;     //話しかけられるフラグ
    public bool      selectFlag;   //選択肢の有無
    public string    name;         //名前

#pragma warning disable CS8632 // '#nullable' 注釈コンテキスト内のコードでのみ、Null 許容参照型の注釈を使用する必要があります。
    public string?[] normalTalkData;   //通常会話内容
    public string?[] battlelTalkData;  //戦闘会話内容
    public string?[] friendTalkData;   //友好会話内容
    public string?[] endTalkData;      //会話終了
    public string?[] selectTalkData = new string?[2];
#pragma warning restore CS8632 // '#nullable' 注釈コンテキスト内のコードでのみ、Null 許容参照型の注釈を使用する必要があります。

    public enum NPCState
    { 
        Normal,   //通常会話
        Battle,   //バトル状態
        Friend,   //友好状態
        End,      //会話終了

        ENUMEND,  //基本状態のLength

        Select,   //セレクト状態
        NonEvent  //イベントなし

    };
    [Header("NPCの状態")]
    public NPCState firstState;
    public NPCState nowState;

    private void OnEnable()
    {
       nowState = firstState;
    }
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
    public string[] GetTalk(NPCState state)
    {
        switch (state)
        {
            case NPCState.Normal:
                return this.normalTalkData;
            case NPCState.Battle:
                return this.battlelTalkData;
            case NPCState.Friend:
                return this.friendTalkData;
            case NPCState.End:
                return this.endTalkData;
            default:
                Debug.Log("なんもないよ");
                break;
        }
        return null;
    }

    public void SetTalkFlag(bool flag)
    {
        this.talkFlag = flag;
    }
}
