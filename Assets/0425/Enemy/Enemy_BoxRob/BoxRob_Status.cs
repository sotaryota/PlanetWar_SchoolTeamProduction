using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRob_Status : Enemy_HpData
{
    [Header("�p�����[�^")]

    [SerializeField]
    private float power_;      //�U����
    [SerializeField]
    private float defense_;    //�h���
    [SerializeField]
    private float speed_;      //�ړ��X�s�[�h

    //--------------------------------------
    //�G�l�~�[(boxrob)�̏�Ԃ̕ύX�Ǝ擾
    //--------------------------------------
    public enum State
    {
        Stay,�@�@//�ҋ@
        Move,    //�ړ�
        Attack,   //�U��
        Damage,  //�_���[�W
        Dead,    //���S

        Non,     //���݂��Ȃ�(��{�g��Ȃ�)
    };

    [SerializeField]
    private State nowState;    //���݂̏��

    //��Ԃ̕ύX
    public void SetState(State state)
    {
        nowState = state;
    }

    //���݂̏�Ԃ̎擾
    public State GetState()
    {
        return nowState;
    }

    //--------------------------------------
    //�Q�b�^�[
    //--------------------------------------

    public float GetPower()
    {
        return this.power_;
    }
    public float GetDefense()
    {
        return this.defense_;
    }
    public float GetSpeed()
    {
        return this.speed_;
    }

    //--------------------------------------
    //�X�e�[�^�X�̕ω�
    //--------------------------------------

    public void Die()
    {
        if (JudgeDie())
        {
            nowState = State.Dead;
            StartCoroutine("DieProcces");
        }
    }
    private void Update()
    {
        Die();
    }
    IEnumerator DieProcces()
    {

        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
}