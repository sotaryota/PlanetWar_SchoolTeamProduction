using UnityEngine;
public class NPCClass : MonoBehaviour
{
    [Header("NPC�v���p�e�B")]
    [SerializeField] private int      id;           //ID
    [SerializeField] private bool     talkFlag;     //�b����������t���O
    [SerializeField] private bool     battleFlag;   //�킢�̃t���O
    [SerializeField] private string   name;         //���O
    [SerializeField] private string[] talk;         //��b���e

    //--------------------------------------
    //�Q�b�^�[
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
