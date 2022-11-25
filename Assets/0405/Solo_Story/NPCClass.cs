using UnityEngine;
public class NPCClass : MonoBehaviour
{
    [Header("NPCプロパティ")]
    [SerializeField] private int      id;           //ID
    [SerializeField] private bool     talkFlag;     //話しかけられるフラグ
    [SerializeField] private bool     battleFlag;   //戦いのフラグ
    [SerializeField] private string   name;         //名前
    [SerializeField] private string[] talk;         //会話内容

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
    public bool GetBattleFlag()
    {
        return this.battleFlag;
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
