using UnityEngine;

public class NPCClass : MonoBehaviour
{
    //�G�f�B�^�[�p��public�ɂ��Ă��܂�
    //�ǂݎ�菑�������͊�{getset���g��
    [Header("NPC�v���p�e�B")]
    public string    name;         // ���O
    public int       eventId;      // �C�x���g��ID
    public bool      talkFlag;     // �b����������t���O
    public bool      selectFlag;   // �I�����̗L��
    public EnemyCreater_Data.EnemyName enemyName; // �o�g�����W��̓G�̎��

    public string[] normalTalkData;    // �ʏ��b���e
    public string[] battlelTalkData;   // �퓬��b���e
    public string[] friendTalkData;    // �F�D��b���e
    public string[] BattleEndTalkData; // �o�g���I��
    public string[] FriendEndTalkData; // �F�D��Ԃŉ�b�I��
    public string[] selectTalkData = new string[2];
    
    // ����
    public enum SelectNom
    {
        First,
        Second,
    }

    public enum NPCState
    { 
        Normal,         // �ʏ��b
        Battle,         // �o�g�����
        Friend,         // �F�D���
        BattleEventEnd, // �o�g���I��
        FriendEventEnd, // �F�D��Ԃŉ�b�I��

        ENUMEND,  // ��{��Ԃ�Length

        Select,   // �Z���N�g���
        NonEvent  // �C�x���g�Ȃ�

    };
    // NPC�̏��
    public NPCState nowState;

    // �I�����̕����
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

    // �I�������Ƃ̏�Ԃ̎擾
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
    // �Q�b�^�[
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
                Debug.Log("�Ȃ���Ȃ���");
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
