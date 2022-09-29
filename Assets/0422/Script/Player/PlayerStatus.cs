using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("パラメータ")]
    [SerializeField]
    private int playerID_;     //プレイヤーのID
    [SerializeField]
    [Range(0,100)]
    private float hp_;         //HP
    [SerializeField]
    private float power_;      //攻撃力
    [SerializeField]
    private float defense_;    //防御力
    [SerializeField]
    private float speed_;      //移動スピード

    //--------------------------------------
    //プレイヤの状態の変更と取得
    //--------------------------------------
    public enum State
    {
        Stay,　　//待機
        Move,    //移動
        Catch,   //惑星を所持
        Damage,  //ダメージ
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
    //ステータスの変化
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
