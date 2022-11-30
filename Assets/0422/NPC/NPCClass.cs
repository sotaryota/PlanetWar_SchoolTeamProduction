using UnityEngine;

public class NPCClass : MonoBehaviour
{
    //�G�f�B�^�[�p��public�ɂ��Ă��܂�
    //�ǂݎ�菑�������͊�{getset���g��
    [Header("NPC�v���p�e�B")]
    public string    name;         //���O
    public int       id;           //ID
    public bool      talkFlag;     //�b����������t���O
    public bool      selectFlag;   //�I�����̗L��
    public bool      battleFlag;   //�퓬�̃t���O

#pragma warning disable CS8632
    public string[] normalTalkData;   //�ʏ��b���e
    public string[] battlelTalkData;  //�퓬��b���e
    public string[] friendTalkData;   //�F�D��b���e
    public string[] endTalkData;      //��b�I��
    public string[] selectTalkData = new string[2];
#pragma warning restore CS8632

    public enum NPCState
    { 
        Normal,   //�ʏ��b
        Battle,   //�o�g�����
        Friend,   //�F�D���
        End,      //��b�I��

        ENUMEND,  //��{��Ԃ�Length

        Select,   //�Z���N�g���
        NonEvent  //�C�x���g�Ȃ�

    };
    [Header("NPC�̏��")]
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
                Debug.Log("�Ȃ���Ȃ���");
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
