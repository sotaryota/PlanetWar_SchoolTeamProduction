using UnityEngine;

public class NPCClass : MonoBehaviour
{
    //�G�f�B�^�[�p��public�ɂ��Ă��܂�
    //�ǂݎ�菑�������͊�{getset���g��
    [Header("NPC�v���p�e�B")]
    public int       id;           //ID
    public bool      talkFlag;     //�b����������t���O
    public bool      selectFlag;   //�I�����̗L��
    public string    name;         //���O

#pragma warning disable CS8632 // '#nullable' ���߃R���e�L�X�g���̃R�[�h�ł̂݁ANull ���e�Q�ƌ^�̒��߂��g�p����K�v������܂��B
    public string?[] normalTalkData;   //�ʏ��b���e
    public string?[] battlelTalkData;  //�퓬��b���e
    public string?[] friendTalkData;   //�F�D��b���e
    public string?[] endTalkData;      //��b�I��
    public string?[] selectTalkData = new string?[2];
#pragma warning restore CS8632 // '#nullable' ���߃R���e�L�X�g���̃R�[�h�ł̂݁ANull ���e�Q�ƌ^�̒��߂��g�p����K�v������܂��B

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
                Debug.Log("�Ȃ���Ȃ���");
                break;
        }
        return null;
    }

    public void SetTalkFlag(bool flag)
    {
        this.talkFlag = flag;
    }
}
