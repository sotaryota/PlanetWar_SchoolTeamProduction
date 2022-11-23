using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using TMPro;

// �ȈՐ���
//�ESetActive�ŉ�b��ǂݍ��܂��܂��̂�"SetText(npc.tale[0])"�Ƃ����Ă��������B
//�E���̌�Anpc.flag��true�ɂ���ƒ��肾���܂��B

public class Write_Effect : MonoBehaviour
{
    //�e�L�X�g�{�b�N�X
    [SerializeField]
    Text textObject = default;
    //�\����\���̍ۂɎg�p����B�L�����o�X�Ƃ��p�l���Ƃ����w��
    [SerializeField]
    GameObject canvas = default;
    //�e�L�X�g�{�b�N�X�ɓ���镶����
    string text;
    [Header("�������莞��")]
    [SerializeField]
    float feedTime = 0.1f;
    [Header("���s����")]
    [SerializeField]
    float nTime = 0.5f;
    //�\�����镶����
    int visibleLength;
    //��b�����̃t���O
    bool isTalking;
    //��b���Ƀ{�^���������Ȃ�����t���O
    public bool buttonFlag = true;
    //�ڐG����NPC
    public GameObject npc;
    [SerializeField] PlayerStatus_Solo playerStatus;


    void Update()
    {
        if (buttonFlag) { return; }

        //��b������Ȃ��Ƃ��A�b����������
        if (!isTalking && npc.GetComponent<NPCClass>().GetTalkFlag())
        {
            //�e�L�X�g�{�b�N�X�\��
            canvas.SetActive(true);
            //��������J�n
            StartCoroutine("TextDisplay");
            isTalking = true;
        }
    }
    //����Ńe�L�X�g���X�V����
    public void SetText(string newtext)
    {
        this.text = newtext;
        visibleLength = 0;
        //���݂̃e�L�X�g������
        textObject.text = "";
    }

    IEnumerator TextDisplay()
    {
        //��x�����Ăяo������
        //-------------------------------------------------
        buttonFlag = true;
        visibleLength = 0;
        isTalking = false;
        SetText(npc.GetComponent<NPCClass>().GetTalk()[0]);
        //-------------------------------------------------
        
        for (int i = 1; i <= npc.GetComponent<NPCClass>().GetTalk().Length; ++i)
        {
            //�o�ĂȂ������������
            while (visibleLength < text.Length)
            {
                //Debug.Log(text);
                yield return new WaitForSeconds(feedTime);
                // 1���������₷
                visibleLength++;
                textObject.text = text.Substring(0, visibleLength);
            }
            //��b�I��
            if (i == npc.GetComponent<NPCClass>().GetTalk().Length)
            {
                isTalking = false;
                //�e�L�X�g�{�b�N�X��\��
                yield return new WaitForSeconds(nTime);
                canvas.SetActive(false);
                //��b�Ώۂ�null�ɂ���
                npc = null;
                //�v���C���[��ҋ@��ԂɕύX
                playerStatus.SetState(PlayerStatus_Solo.State.Stay);
                break;
            }
            else
            {
                yield return new WaitForSeconds(nTime);
                //���̃e�L�X�g��ǂݍ���
                SetText(npc.GetComponent<NPCClass>().GetTalk()[i]);
                //Debug.Log(text);
            }
        }
    }
}
