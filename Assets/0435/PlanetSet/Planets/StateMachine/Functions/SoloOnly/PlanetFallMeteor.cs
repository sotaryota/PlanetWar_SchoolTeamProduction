using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetFallMeteor : PlanetStateFanction
{
    private Vector3 startPos;//�����ʒu�iLerp�Ŏg�p�j

    [Header("�ڒn���G�t�F�N�g")]
    [SerializeField]
    private GameObject crashEffect;

    [Header("�������x")]
    [SerializeField]
    private float moveTime;
    private float startTime;

    [Header("�ڕW���W�i�u0,0,0�v�̏ꍇ�A���W�������ݒ�j")]
    [SerializeField]
    public Vector3 targetPos;
    [Header("�ڕW���W�ݒ�")]
    [SerializeField]
    private Vector2 Range_X;
    [SerializeField]
    private float posY;
    [SerializeField]
    private Vector2 Range_Z;

    private bool moveFlag;//�ړ����邩�𔻒�iY�� ���O�Ŗ������j

    private void Start()
    {
        //�������W��ݒ�(�u0,0,0�v�̏ꍇ)
        if (targetPos == Vector3.zero)
        {
            TargetPosSetting();
        }

        //�����ʒu�ۑ�
        startPos = transform.position;

        //�ړ��J�n
        moveFlag = true;

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
            transform.position = Vector3.Lerp(startPos, targetPos, moveVal * moveTime);

            //���W���O�t�߂ɂȂ�����
            if (transform.position.y <= 0)
            {
                //Y���W���O�ɂ���
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);

                //�G�t�F�N�g����
                GameObject effect = Instantiate(crashEffect);
                effect.transform.position = this.transform.position;

                //�ړ��I��
                moveFlag = false;
            }

        }

        return PlanetStateMachine.State.Now;
    }

    void TargetPosSetting()
    {
        //���W�̓����_��
        float cPosX = Random.Range(Range_X.x, Range_X.y);
        float cPosZ = Random.Range(Range_Z.x, Range_Z.y);
        targetPos = new Vector3(cPosX, posY, cPosZ);
    }
}
