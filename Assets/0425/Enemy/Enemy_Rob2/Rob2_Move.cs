using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rob2_Move : MonoBehaviour
{
    [Header("�X�N���v�g")]
    [SerializeField] Rob2_Status rob2_Status;
    [SerializeField] Rob2_Sensing rob2_Sensing;

    [SerializeField] Rob2_AnimManeger rob2Animator;

    GameObject player;

    bool die = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        //�G�l�~�[�����݂��Ȃ�or����ł���Ȃ珈�������Ȃ�
        if (rob2_Status.GetState() == Rob2_Status.State.Non || rob2_Status.GetState() == Rob2_Status.State.Dead)
        {
            if (die == false)
            {
                rob2Animator.PlayRob2AnimDie();
                die = true;
            }
            return;
        }

        PlayerLook();

        if (rob2_Sensing.sensing == false)
        {   
            MoveOrStop();
        }
        else
        {
            rob2Animator.PlayRob2AnimSetRun(false);
        }
    }

    //--------------------------------------
    //�v���C���[�̕����Ɍ���
    //--------------------------------------

    private void PlayerLook()
    {
        Vector3 lookPos = player.transform.position;
        lookPos.y = this.transform.position.y;
        transform.LookAt(lookPos);
    }

    //--------------------------------------
    //�����̏���
    //--------------------------------------
    private void MoveOrStop()
    {
        if (rob2_Status.GetState() == Rob2_Status.State.Attack)
        { return; }

            //Catch��ԂłȂ��Ȃ�
            if (rob2_Status.GetState() != Rob2_Status.State.Attack)
        {
            //�v���C����Move��Ԃ�
            rob2_Status.SetState(Rob2_Status.State.Move);
        }

        //�ړ�����
        Vector3 movePos = player.transform.position;
        movePos.y = this.transform.position.y; ;
        transform.position = Vector3.MoveTowards(transform.position, movePos, rob2_Status.GetSpeed() * Time.deltaTime);

        //�A�j���[�V����
        rob2Animator.PlayRob2AnimSetRun(true);
    }
}