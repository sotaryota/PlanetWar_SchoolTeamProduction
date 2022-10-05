using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pleaseStart : MonoBehaviour
{
    //���Ԃ̌v��
    float TimeCnt;
    //�\������
    [SerializeField]
    float rightTime;
    //��\������
    [SerializeField]
    float deleteTime;
    //�\�������ۂ�
    bool isrighting;

    [SerializeField]
    Image pleaseStartlogo;
    float alpha;

    void Start()
    {
        TimeCnt = 0f;
        //rightTime = 2f;
        //deleteTime = 0.5f;
        isrighting = true;
        alpha = 1.0f;
        pleaseStartlogo = GetComponent<Image>();
    }
    void Update()
    {
        //�v��
        TimeCnt += Time.deltaTime;

        //�\�����Ȃ�
        if (isrighting)
        {
            //�\�����ԕ��܂ŕ\��
            if (TimeCnt >= rightTime)
            {
                //�t���O��ς��Ĕ�\��&���Z�b�g
                isrighting = false;
                alpha = 0.0f;
                TimeCnt = 0f;
            }
        }
        //��\���Ȃ��\�����ԕ��܂ő�����
        else if (TimeCnt >= deleteTime)
        {
            //�t���O��ς��ĕ\��&���Z�b�g
            isrighting = true;
            alpha = 1.0f;
            TimeCnt = 0f;
        }
        Color colorCopy = pleaseStartlogo.color;
        pleaseStartlogo.color = new Color(colorCopy.r, colorCopy.g, colorCopy.b, alpha);
    }
}