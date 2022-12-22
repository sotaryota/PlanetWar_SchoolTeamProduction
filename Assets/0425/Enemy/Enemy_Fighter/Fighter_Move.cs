using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter_Move : MonoBehaviour
{
    [Header("�X�N���v�g")]
    [SerializeField] Fighter_Status fighter_Status;
    [SerializeField] Fighter_Sensing fighter_Sensing;

    [SerializeField] Fighter_AnimManeger fighterAnimator;

    GameObject player;

    bool die = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        //�G�l�~�[�����݂��Ȃ�or����ł���Ȃ珈�������Ȃ�
        if (fighter_Status.GetState() == Fighter_Status.State.Non || fighter_Status.GetState() == Fighter_Status.State.Dead)
        {
            if (die == false)
            {
                fighterAnimator.PlayFighterAnimDie();
                die = true;
            }
            return;
        }

        PlayerLook();

        if (fighter_Sensing.sensing == false)
        {
            MoveOrStop();
        }
        else
        {
            fighterAnimator.PlayFighterAnimSetRun(false);
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
        if (fighter_Status.GetState() == Fighter_Status.State.Attack)
        { return; }

        //Catch��ԂłȂ��Ȃ�
        if (fighter_Status.GetState() != Fighter_Status.State.Attack)
        {
            //�v���C����Move��Ԃ�
            fighter_Status.SetState(Fighter_Status.State.Move);
        }

        //�ړ�����
        Vector3 movePos = player.transform.position;
        movePos.y = this.transform.position.y; ;
        transform.position = Vector3.MoveTowards(transform.position, movePos, fighter_Status.GetSpeed() * Time.deltaTime);

        //�A�j���[�V����
        fighterAnimator.PlayFighterAnimSetRun(true);
    }
}