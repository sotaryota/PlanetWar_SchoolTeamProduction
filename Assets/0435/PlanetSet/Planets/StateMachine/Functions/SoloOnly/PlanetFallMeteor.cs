using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetFallMeteor : PlanetStateFanction
{
    private Vector3 startPos;//�����ʒu�iLerp�Ŏg�p�j

    [Header("�v���C���[�ʒu�Q��")]
    [SerializeField]
    private GameObject player;

    [Header("�ڒn���G�t�F�N�g")]
    [SerializeField]
    private GameObject crashEffect;

    [Header("�������x")]
    [SerializeField]
    private float moveTime;
    private float startTime;

    [Header("�ڕW���W�i�u0,0,0�v�̏ꍇ�A���W�������ݒ�j")]
    [SerializeField]
    private Vector3 targetPos;
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

        if (!player)
        {
            print("PlanetFallMeteor:�v���C���[���Q�Ƃ����覐΂��v���C���[���߂ɗ������Ȃ��Ȃ�܂�");
        }

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

            //�^�[�Q�b�g���W�ƃv���C���[���߂������ꍇ�ɍ��W�Đݒ�
            if (player)
            {
                if ((player.transform.position - targetPos).magnitude >= 3)
                {
                    TargetPosSetting();
                }
            }
        }

        return PlanetStateMachine.State.Now;
    }

    void TargetPosSetting()
    {
        //�����ʒu�ۑ�
        startPos = transform.position;

        //���W�̓����_��
        float cPosX = Random.Range(Range_X.x, Range_X.y);
        float cPosZ = Random.Range(Range_Z.x, Range_Z.y);
        targetPos = new Vector3(cPosX, posY, cPosZ);
    }
}
