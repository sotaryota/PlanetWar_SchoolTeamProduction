using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus_Solo : MonoBehaviour
{
    [Header("�p�����[�^")]
    [SerializeField]
    private int playerID;     //�v���C���[��ID
    [SerializeField]
    [Range(0, 100)]
    private float hp;         //HP
    [SerializeField]
    private float power;      //�U����
    [SerializeField]
    private float defense;    //�h���
    [SerializeField]
    private float speed;      //�ړ��X�s�[�h
    [SerializeField]
    private float jumpPow;    //�W�����v��

    //--------------------------------------
    //�v���C���̏�Ԃ̕ύX�Ǝ擾
    //--------------------------------------
    public enum State
    {
        Stay,    //�ҋ@
        Move,    //�ړ�
        Jump,    //�W�����v
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
    public float GetJumpPower()
    {
        return this.jumpPow;
    }

    //--------------------------------------
    //�X�e�[�^�X�̕ω�
    //--------------------------------------
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    public void PowerUp(float powerUpPoint)
    {
        this.power += powerUpPoint;
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