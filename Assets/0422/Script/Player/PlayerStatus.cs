using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("�p�����[�^")]
    [SerializeField]
    private int playerID_;     //�v���C���[��ID
    [SerializeField]
    [Range(0,100)]
    private float hp_;         //HP
    [SerializeField]
    private float power_;      //�U����
    [SerializeField]
    private float defense_;    //�h���
    [SerializeField]
    private float speed_;      //�ړ��X�s�[�h

    //--------------------------------------
    //�v���C���̏�Ԃ̕ύX�Ǝ擾
    //--------------------------------------
    public enum State
    {
        Stay,�@�@//�ҋ@
        Move,    //�ړ�
        Catch,   //�f��������
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

    public int GetID()
    {
        return this.playerID_;
    }
    public float GetHp()
    {
        return this.hp_;
    }
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

    public void PowerUp(float powerUpPoint)
    {
        this.power_ += powerUpPoint;
    }
    public void DefenseUp(float defenseUpPoint)
    {
        this.defense_ += defenseUpPoint;
    }
    public void SpeedUp(float speedUpPoint)
    {
        this.speed_ += speedUpPoint;
    }
    public void Damage(float damage)
    {
        this.hp_ -= damage;
    }
}
