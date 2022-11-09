using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    [SerializeField]
    PlayerStatus status;
    [SerializeField] 
    PlayerAnimManeger playerAnimator;

    //���S���̔���p
    private bool firstInFlag;

    /// <summary>
    /// ���S�p�^�[��
    /// �ǉ�����ꍇ��Dead�֐�����switch���ɏ�����ǉ�
    /// </summary>
    public enum DeadState
    {
        die,       //�f���q�b�g�Ŏ��S
        exhausted, //���z�̃X���b�v�_���[�W�Ŏ��S
        non
    };

    [SerializeField]
    DeadState deadState;

    //���S��Ԃ̎擾�Ə�������
    //-----------------------------------------
    public void SetDeadState(DeadState state)
    {
        deadState = state;
    }
    public DeadState GetDead()
    {
        return deadState;
    }
    //-----------------------------------------

    private void Start()
    {
        deadState = DeadState.non;
        firstInFlag = true;
    }
    private void Update()
    {
        Dead();
    }

    void Dead()
    {
        if (status.GetState() != PlayerStatus.State.Dead) { return; }
        if (!firstInFlag) { return; }
        firstInFlag = false;

        #region ���S���̏����͂����ōs���Ă�������

        switch (deadState)
        {
            //�f���q�b�g�ł̎��S��-----------------------------------------
            case DeadState.die:
                Debug.Log("�f���q�b�g�ɂ�莀�S");

                //�A�j���[�V����
                playerAnimator.PlayAnimDie();

                //���S�{�C�X�Đ�
                this.GetComponent<PlayerSEManager>().DeathVoice();
                break;

            //���z�̃X���b�v�_���[�W�ł̎��S��-----------------------------
            case DeadState.exhausted:
                Debug.Log("�X���b�v�_���[�W�ɂ�莀�S");

                //�A�j���[�V����
                playerAnimator.PlayAnimExhausted();

                //���S�{�C�X�Đ�
                this.GetComponent<PlayerSEManager>().DeathVoice();
                break;

            default:
                firstInFlag = true;
                break;
        }
        #endregion

        print("PlayerDead:�ǉ�����");
        
    }
}

