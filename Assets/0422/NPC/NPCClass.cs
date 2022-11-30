using UnityEngine;

public class NPCClass : MonoBehaviour
{
    //エディター用にpublicにしています
    //読み取り書き換えは基本getsetを使え
    [Header("NPCプロパティ")]
    public string    name;         //名前
    public int       id;           //ID
    public bool      talkFlag;     //話しかけられるフラグ
    public bool      selectFlag;   //選択肢の有無
    public bool      battleFlag;   //戦闘のフラグ

#pragma warning disable CS8632
    public string[] normalTalkData;   //通常会話内容
    public string[] battlelTalkData;  //戦闘会話内容
    public string[] friendTalkData;   //友好会話内容
    public string[] endTalkData;      //会話終了
    public string[] selectTalkData = new string[2];
#pragma warning restore CS8632

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
    public void SetState(NPCState state)
    {
        this.nowState = state;
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
    public bool GetSelectFlag()
    {
        return this.selectFlag;
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
            case NPCState.Select:
                return this.selectTalkData;
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
    public void SetSelectFlag(bool flag)
    {
        this.selectFlag = flag;
    }
}
