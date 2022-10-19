using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    [SerializeField]
    PlayerStatus status;
    [SerializeField]
    Animator animator;

    public enum DeadState
    {
        Hit,      //�f���q�b�g
        die,      //�f���q�b�g�Ŏ��S
        exhausted, //���z�̃X���b�v�_���[�W�Ŏ��S
        non
    };

    [SerializeField]
    DeadState deadState;

    //�ǉ�
    private bool firstInFlag;

    public void SetDeadState(DeadState state)
    {
        deadState = state;
    }

    public DeadState GetDead()
    {
        return deadState;
    }

    private void Start()
    {
        deadState = DeadState.non;
        firstInFlag = true;
    }

    private void Update()
    {
        Dead();
    }

    bool DeadCheck()
    {
        return status.GetState() == PlayerStatus.State.Dead;
    }

    void Dead()
    {
        if (!DeadCheck()) { return; }
        if (!firstInFlag) { return; }
        firstInFlag = false;

        switch (deadState)
        {
            //�f���q�b�g�ł̎��S��-----------------------------------------
            case DeadState.die:
                Debug.Log("�f���q�b�g�ɂ�莀�S");

                //�A�j���[�V����
                animator.SetTrigger("die");

                //���S�{�C�X�Đ�
                this.GetComponent<PlayerSEManager>().DeathVoice();
                break;

            //���z�̃X���b�v�_���[�W�ł̎��S��-----------------------------
            case DeadState.exhausted:
                Debug.Log("�X���b�v�_���[�W�ɂ�莀�S");

                //�A�j���[�V����
                animator.SetTrigger("exhausted");

                //���S�{�C�X�Đ�
                this.GetComponent<PlayerSEManager>().DeathVoice();
                break;

            default:
                firstInFlag = true;
                break;
        }

        print("PlayerDead:�ǉ�����");
        
    }
}

