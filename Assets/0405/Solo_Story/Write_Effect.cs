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
    //����ɒǉ�
    [SerializeField] NPCClass npc;

    void Start()
    {
        visibleLength = 0;
        isTalking = false;
        canvas.SetActive(false);

        //�e�X�g�p
        SetText(npc.talk[0]);
    }

    void Update()
    {
        //��b������Ȃ��Ƃ��A�b����������
        if (!isTalking && npc.flag)
        {
            //�e�X�g�p
            //SetText(npc.talk[0]);

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
        for (int i = 1; i <= npc.talk.Length; ++i)
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
            if (i == npc.talk.Length)
            {
                isTalking = false;
                npc.flag = false;
                //�e�L�X�g�{�b�N�X��\��
                yield return new WaitForSeconds(nTime);
                canvas.SetActive(false);
                break;
            }
            else
            {
                yield return new WaitForSeconds(nTime);
                SetText(npc.talk[i]);
                //Debug.Log(text);
            }
        }
    }
}
