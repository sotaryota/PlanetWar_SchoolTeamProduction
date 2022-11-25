using UnityEngine;

public class NPCClass : MonoBehaviour
{
    //�G�f�B�^�[�p��public�ɂ��Ă��܂�
    //�ǂݎ�菑�������͊�{getset���g��
    [Header("NPC�v���p�e�B")]
    public int      id;           //ID
    public bool     talkFlag;     //�b����������t���O
    public string   name;         //���O
    public string[] talk;         //��b���e

    public enum TalkProperty
    { 
        Normal, //�ʏ��b
        Select, //�Z���N�g���
        Battle, //�o�g�����
        Friend, //�F�D���
        End,    //��b�I��

        ENUMEND //enum�̍ŏI�l
    };

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
        return this.talk;
    }

    public void SetTalkFlag(bool flag)
    {
        this.talkFlag = flag;
    }
}
