using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
//using TMPro;

// �ȈՐ���
//�ESetActive�ŉ�b��ǂݍ��܂��܂��̂�"SetText(npc.tale[0])"�Ƃ����Ă��������B
//�E���̌�Anpc.flag��true�ɂ���ƒ��肾���܂��B

public class Write_Effect : MonoBehaviour
{
    Gamepad gamepad;
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
    public bool isTalking;
    //��b���Ƀ{�^���������Ȃ�����t���O
    public bool buttonFlag = true;
    //�ڐG����NPC
    public GameObject npc;
    [SerializeField] PlayerStatus_Solo playerStatus;


    void Update()
    {
        if (buttonFlag) { return; }
        if (gamepad == null)
            gamepad = Gamepad.current;
        //��b������Ȃ��Ƃ��A�b����������
        if (!isTalking && npc.GetComponent<NPCClass>().GetTalkFlag())
        {
            //�e�L�X�g�{�b�N�X�\��
            canvas.SetActive(true);
            //��������J�n
            StartCoroutine("TextDisplay");
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
        visibleLength = 0;
        isTalking = true;
        //-------------------------------------------------

        for (int i = 0; i < npc.GetComponent<NPCClass>().GetTalk().Length; ++i)
        {
            Debug.Log("�z��ԍ�" + i);
            SetText(npc.GetComponent<NPCClass>().GetTalk()[i]);
            Debug.Log(text);
            //�o�ĂȂ������������
            while (visibleLength < text.Length)
            {
                yield return new WaitForSeconds(feedTime);
                // 1���������₷
                visibleLength++;
                textObject.text = text.Substring(0, visibleLength);
                if (gamepad.buttonWest.isPressed)
                {
                    visibleLength = text.Length - 1;
                }
            }
            //��b�I��
            if (i == npc.GetComponent<NPCClass>().GetTalk().Length - 1)
            {
                Debug.Log("��b�I��");
                //�e�L�X�g�{�b�N�X��\��
                yield return new WaitForSeconds(nTime);
                canvas.SetActive(false);
                //��b�Ώۂ�null�ɂ���
                npc.GetComponent<NPCClass>().SetTalkFlag(false);
                npc = null;
                //�{�^����������悤�ɂ���
                buttonFlag = true;
                //�v���C���[��ҋ@��ԂɕύX
                playerStatus.SetState(PlayerStatus_Solo.State.Stay);
                
                isTalking = false;
                yield break;
            }
            else
            {
                yield return new WaitForSeconds(nTime);
                //Debug.Log(text);
            }
        }
    }
}
