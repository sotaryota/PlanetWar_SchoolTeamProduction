using UnityEngine;

public class NPCClass : MonoBehaviour
{
    //�G�f�B�^�[�p��public�ɂ��Ă��܂�
    //�ǂݎ�菑�������͊�{getset���g��
    [Header("NPC�v���p�e�B")]
    public int       id;           //ID
    public bool      talkFlag;     //�b����������t���O
    public string    name;         //���O

#pragma warning disable CS8632 // '#nullable' ���߃R���e�L�X�g���̃R�[�h�ł̂݁ANull ���e�Q�ƌ^�̒��߂��g�p����K�v������܂��B
    public string?[] normalTalk;   //�ʏ��b���e
    public string?[] battlelTalk;  //�퓬��b���e
    public string?[] friendTalk;   //�F�D��b���e
#pragma warning restore CS8632 // '#nullable' ���߃R���e�L�X�g���̃R�[�h�ł̂݁ANull ���e�Q�ƌ^�̒��߂��g�p����K�v������܂��B

    public enum NPCState
    { 
        Normal,   //�ʏ��b
        Select,   //�Z���N�g���
        Battle,   //�o�g�����
        Friend,   //�F�D���
        End,      //��b�I��

        NonEvent, //�C�x���g�Ȃ�

        ENUMEND   //enum�̍ŏI�l
    };
    [Header("NPC�̏��")]
    public NPCState firstState;
    public NPCState nowState;

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
    public string[] GetTalk()
    {
        return this.normalTalk;
    }

    public void SetTalkFlag(bool flag)
    {
        this.talkFlag = flag;
    }
}
