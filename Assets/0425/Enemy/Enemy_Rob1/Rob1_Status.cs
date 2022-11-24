using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rob1_Status : Enemy_HpData
{
    [Header("パラメータ")]

    [SerializeField]
    private float power_;      //攻撃力
    [SerializeField]
    private float defense_;    //防御力
    [SerializeField]
    private float speed_;      //移動スピード

    //--------------------------------------
    //エネミー(rob1)の状態の変更と取得
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

    public void Die()
    {
        if(JudgeDie())
        {
            nowState = State.Dead;
        }
    }

    private void Update()
    {
        Die();
    }
}