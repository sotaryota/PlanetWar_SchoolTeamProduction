using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("�p�����[�^")]
    [SerializeField]
    private int playerID;     //�v���C���[��ID
    [SerializeField]
    [Range(0,100)]
    private float hp;         //HP
    [SerializeField]
    private float power;      //�U����
    [SerializeField]
    private float defense;    //�h���
    [SerializeField]
    private float speed;      //�ړ��X�s�[�h

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
        return this.playerID;
    }
    public float GetHp()
    {
        return this.hp;
    }
    public float GetPower()
    {
        return this.power;
    }
    public float GetDefense()
    {
        return this.defense;
    }
    public float GetSpeed()
    {
        return this.speed;
    }

    //--------------------------------------
    //�X�e�[�^�X�̕ω�
    //--------------------------------------

    public void PowerUp(float powerUpPoint)
    {
        this.power += powerUpPoint;
    }
    public void DefenseUp(float defenseUpPoint)
    {
        this.defense += defenseUpPoint;
    }
    public void SpeedUp(float speedUpPoint)
    {
        this.speed += speedUpPoint;
    }
    public void Damage(float damage)
    {
        this.hp -= damage;
    }
    public void Escape(float escapeCost)
    {
        this.defense -= escapeCost;
    }
}
