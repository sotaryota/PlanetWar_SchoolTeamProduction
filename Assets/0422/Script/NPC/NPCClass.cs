using UnityEngine;

public class NPCClass : MonoBehaviour
{
    //エディター用にpublicにしています
    //読み取り書き換えは基本getsetを使え
    [Header("NPCプロパティ")]
    public string    name;         // 名前
    public int       eventId;      // イベントのID
    public bool      talkFlag;     // 話しかけられるフラグ
    public bool      selectFlag;   // 選択肢の有無
    public EnemyCreater_Data.EnemyName enemyName; // バトル発展先の敵の種類

    public string[] normalTalkData;    // 通常会話内容
    public string[] battlelTalkData;   // 戦闘会話内容
    public string[] friendTalkData;    // 友好会話内容
    public string[] BattleEndTalkData; // バトル終了
    public string[] FriendEndTalkData; // 友好状態で会話終了
    public string[] selectTalkData = new string[2];
    
    // 分岐数
    public enum SelectNom
    {
        First,
        Second,
    }

    public enum NPCState
    { 
        Normal,         // 通常会話
        Battle,         // バトル状態
        Friend,         // 友好状態
        BattleEventEnd, // バトル終了
        FriendEventEnd, // 友好状態で会話終了

        ENUMEND,  // 基本状態のLength

        Select,   // セレクト状態
        NonEvent  // イベントなし

    };
    // NPCの状態
    public NPCState nowState;

    // 選択肢の分岐先
    public NPCState firstSelectState;  
    public NPCState secondSelectState;

    public NPCState GetState()
    {
        return this.nowState;
    }
    public void SetState(NPCState state)
    {
        this.nowState = state;
    }

    // 選択肢ごとの状態の取得
    //--------------------------------------
    public NPCState GetFirstSelectState()
    {
        return this.firstSelectState;
    }
    public NPCState GetSecondSelectState()
    {
        return this.secondSelectState;
    }

    //--------------------------------------
    // ゲッター
    //--------------------------------------
    public int GetEventID()
    {
        return this.eventId;
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
            case NPCState.BattleEventEnd:
                return this.BattleEndTalkData;
            case NPCState.FriendEventEnd:
                return this.FriendEndTalkData;
            case NPCState.Select:
                return this.selectTalkData;
            default:
                Debug.Log("なんもないよ");
                break;
        }
        return null;
    }
    public EnemyCreater_Data.EnemyName GetEnemyName()
    {
        return this.enemyName;
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
