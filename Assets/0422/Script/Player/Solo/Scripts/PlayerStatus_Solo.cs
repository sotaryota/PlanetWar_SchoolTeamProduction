using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus_Solo : MonoBehaviour
{
    [Header("パラメータ")]
    [SerializeField]
    private int playerID;     //プレイヤーのID
    [SerializeField]
    [Range(0, 100)]
    private float hp;         //HP
    [SerializeField]
    private float power;      //攻撃力
    [SerializeField]
    private float defense;    //防御力
    [SerializeField]
    private float speed;      //移動スピード
    [SerializeField]
    private float jumpPow;    //ジャンプ力

    //--------------------------------------
    //プレイヤの状態の変更と取得
    //--------------------------------------
    public enum State
    {
        Stay,    //待機
        Move,    //移動
        Jump,    //ジャンプ
        Catch,   //惑星を所持
        Damage,  //ダメージ
        Talking, //会話中
        Dead,    //死亡

        Non,     //存在しない(基本使わない)
    };

    [SerializeField]
    private State nowState;    //現在の状態

    //状態の変更
    public void SetState(State state)
    {
        nowState = state;
    }

    //現在の状態の取得
    public State GetState()
    {
        return nowState;
    }

    //--------------------------------------
    //ゲッター
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
    //ステータスの変化
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
    public void Die()
    {
        if(this.hp <= 0)
        {
            nowState = State.Dead;
        }
    }
    public void Escape(float escapeCost)
    {
        this.defense -= escapeCost;
    }

    private void Update()
    {
        Die();
    }
}