using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveLimit : MonoBehaviour
{
    [SerializeField] //���S�ɂȂ�I�u�W�F�N�g
    private GameObject centerObj;

    [SerializeField] //�ړ��͈͂̔��a
    private int moveLimitRadius;

    private void Update()
    {
        MoveLimit();
    }

    //--------------------------------------
    //�ړ��͈͂̐���
    //--------------------------------------

    void MoveLimit()
    {
        //�v���C���̈ʒu���ړ��\�͈͂��傫����
        if(Vector3.Distance(transform.position,centerObj.transform.position) > moveLimitRadius)
        {
            //���S�I�u�W�F�N�g����̃x�N�g�����擾
            Vector3 limitPos = transform.position - centerObj.transform.position;

            //�x�N�g���̐��K��
            limitPos.Normalize();

            //�x�N�g�������ƂɃv���C���̈ʒu���ړ������ʒu��
            transform.position = limitPos * moveLimitRadius;
        }
    }
}
