using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rob1_Move : MonoBehaviour
{
    [Header("�J����")]
    [Header("�X�N���v�g")]
    public Rob1_Status rob1Status;

    [SerializeField] Rob1_AnimManeger rob1Animator;

    [SerializeField] GameObject player;

    [SerializeField] bool sensing = false;

    private void Update()
    {
        //�G�l�~�[�����݂��Ȃ�or����ł���Ȃ珈�������Ȃ�
        if (rob1Status.GetState() == Rob1_Status.State.Non || rob1Status.GetState() == Rob1_Status.State.Dead)
        { return; }

        PlayerLook();

        if (sensing == false)
        {
            MoveOrStop();
        }
        else
        {
            rob1Animator.PlayRob1AnimSetRun(false);
        }
    }

    //--------------------------------------
    //�v���C���[�̌���������
    //--------------------------------------

    private void PlayerLook()
    {
        transform.LookAt(player.transform);
    }

    //--------------------------------------
    //�����̏���
    //--------------------------------------
    private void MoveOrStop()
    {
            //Catch��ԂłȂ��Ȃ�
            if (rob1Status.GetState() != Rob1_Status.State.Catch)
            {
                //�v���C����Move��Ԃ�
                rob1Status.SetState(Rob1_Status.State.Move);
            }

        //�ړ�����
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, rob1Status.GetSpeed() * Time.deltaTime);

        //�A�j���[�V����
        rob1Animator.PlayRob1AnimSetRun(true);
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
            sensing = true;
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            sensing = false;
    }
}
