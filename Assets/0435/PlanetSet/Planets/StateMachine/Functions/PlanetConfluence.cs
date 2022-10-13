using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetConfluence : PlanetStateFanction
{
    [Header("�o����W����Y�����O�ɂȂ�܂ł̎���")]
    [SerializeField]
    private float time;

    [Header("�����_���͈�")]
    [SerializeField]
    private Vector2 minMax;
    private Vector3 targetPos;
    private bool moveFlag;

    private void Start()
    {
        //�������W��ݒ�
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
    }


    public override PlanetStateMachine.State Fanction(float deltaTime)
    {
        if (moveFlag)
        {
            //Y�����O�܂ňړ��i���Ԋ����j
            transform.position = Vector3.Slerp(transform.position, targetPos, time * deltaTime);
            if (transform.position.y <= 0.1f && transform.position.y >= -0.1f)
            {
                moveFlag = false;
            }
        }

        return PlanetStateMachine.State.Now;
    }
}
