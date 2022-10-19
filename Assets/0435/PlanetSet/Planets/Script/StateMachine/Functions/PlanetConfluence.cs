using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetConfluence : PlanetStateFanction
{
    [Header("�������x���X�N���v�g�̈ړ����x�ɓK�p����i�Ȃ��Ă��j")]
    [SerializeField]
    private PlanetRotateAround PRA;
    private Vector3 startPos;//�����ʒu�iLerp�Ŏg�p�j
    private Vector3 posBefore;//���x�v�Z�Ɏg�p

    [Header("�������x")]
    [SerializeField]
    private float moveTime;
    private float startTime;

    [Header("�����_���͈�")]
    [SerializeField]
    private Vector2 minMax;
    [Header("�ڕW���W�i�u0,0,0�v�̏ꍇ�A���W�������ݒ�j")]
    [SerializeField]
    private Vector3 targetPos;
    private bool moveFlag;//�ړ����邩�𔻒�iY�� ���O�Ŗ������j

    private void Start()
    {
        //�������W��ݒ�(�u0,0,0�v�̏ꍇ)
        if(targetPos == Vector3.zero)
        {
            //���W�̓����_��
            float cPosX = 0;
            float cPosZ = 0;
            if (Random.Range(0, 2) == 0)
            {
                //X�������R
                cPosX = Random.Range(-minMax.y, minMax.y);
                cPosZ = Random.Range(minMax.x, minMax.y);
            }
            else
            {
                //Z�������R
                cPosX = Random.Range(minMax.x, minMax.y);
                cPosZ = Random.Range(-minMax.y, minMax.y);
            }
            targetPos = new Vector3(cPosX, 0, cPosZ);
            moveFlag = true;
            startPos = transform.position;

        }

        //�J�n���Ԃ𓾂�
        startTime = Time.time;
    }


    public override PlanetStateMachine.State Fanction(float deltaTime)
    {
        if (moveFlag)
        {
            //Y�����O�܂ňړ��i���Ԋ����j
            float distance = Vector3.Distance(startPos, targetPos);
            float moveVal = (Time.time - startTime) / distance;
            transform.position = Vector3.Slerp(startPos, targetPos, moveVal * moveTime);

            //�ړ����x���v�Z���K��
            if (PRA)
            {
                //���x�v�Z
                float planetVelocity = (transform.position - posBefore).magnitude / deltaTime;
                //���݂̍��W�ۑ�
                posBefore = transform.position;
                //�K�p
                PRA.SetMoveSpeed(planetVelocity);
            }

            //���W���O�t�߂ɂȂ�����
            if (transform.position.y <= 0.1f && transform.position.y >= -0.1f)
            {
                //Y���W���O�ɂ���
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                //�ړ��I��
                moveFlag = false;
            }
        }

        return PlanetStateMachine.State.Now;
    }
}
