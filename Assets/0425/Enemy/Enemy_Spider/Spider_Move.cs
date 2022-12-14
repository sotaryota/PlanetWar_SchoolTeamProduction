using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider_Move : MonoBehaviour
{
    [Header("�X�N���v�g")]
    [SerializeField] Spider_Status spider_Status;
    [SerializeField] Spider_Sensing spider_Sensing;

    [SerializeField] Spider_AnimManeger spider_Animator;

    GameObject player;

    bool die = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        //�G�l�~�[�����݂��Ȃ�or����ł���Ȃ珈�������Ȃ�
        if (spider_Status.GetState() == Spider_Status.State.Non || spider_Status.GetState() == Spider_Status.State.Dead)
        {
            if (die == false)
            {
                spider_Animator.PlaySpiderAnimDie();
                die = true;
            }
            return;
        }

        PlayerLook();

        if (spider_Sensing.sensing == false)
        {
            MoveOrStop();
        }
        else
        {
            spider_Animator.PlaySpiderAnimSetRun(false);
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
        if (spider_Status.GetState() == Spider_Status.State.Attack)
        { return; }

        //Catch��ԂłȂ��Ȃ�
        if (spider_Status.GetState() != Spider_Status.State.Attack)
        {
            //Spider��Move��Ԃ�
            spider_Status.SetState(Spider_Status.State.Move);
        }

        //�ړ�����
        Vector3 movePos = player.transform.position;
        movePos.y = this.transform.position.y; ;
        transform.position = Vector3.MoveTowards(transform.position, movePos, spider_Status.GetSpeed() * Time.deltaTime);

        //�A�j���[�V����
        spider_Animator.PlaySpiderAnimSetRun(true);
    }
}