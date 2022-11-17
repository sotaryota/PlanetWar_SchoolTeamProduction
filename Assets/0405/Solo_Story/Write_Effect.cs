using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using TMPro;

public class Write_Effect : MonoBehaviour
{
    [SerializeField]
    Text textObject = default;

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
        
        //�e�X�g�p
        SetText(npc.talk[0]);
    }

    void Update()
    {
        //��b������Ȃ��Ƃ��A�b����������A�\���J�n
        if (!isTalking && npc.flag)
        {
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
