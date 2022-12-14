using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRob_Move : MonoBehaviour
{
    [Header("�X�N���v�g")]
    [SerializeField] BoxRob_Status boxRob_Status;
    [SerializeField] BoxRob_Sensing boxRob_Sensing;

    [SerializeField] BoxRob_AnimManeger boxRob_Animator;

    GameObject player;

    bool die = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        //�G�l�~�[�����݂��Ȃ�or����ł���Ȃ珈�������Ȃ�
        if (boxRob_Status.GetState() == BoxRob_Status.State.Non || boxRob_Status.GetState() == BoxRob_Status.State.Dead)
        {
            if (die == false)
            {
                boxRob_Animator.PlayBoxRobAnimDie();
                die = true;
            }
            return;
        }

        PlayerLook();

        if (boxRob_Sensing.sensing == false)
        {
            MoveOrStop();
        }
        else
        {
            boxRob_Animator.PlayBoxRobAnimSetRun(false);
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
        //Catch��ԂłȂ��Ȃ�
        if (boxRob_Status.GetState() != BoxRob_Status.State.Attack)
        {
            //�v���C����Move��Ԃ�
            boxRob_Status.SetState(BoxRob_Status.State.Move);
        }

        //�ړ�����
        Vector3 movePos = player.transform.position;
        movePos.y = this.transform.position.y; ;
        transform.position = Vector3.MoveTowards(transform.position, movePos, boxRob_Status.GetSpeed() * Time.deltaTime);

        //�A�j���[�V����
        boxRob_Animator.PlayBoxRobAnimSetRun(true);
    }
}