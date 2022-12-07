using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("パラメータ")]
    [SerializeField]
    private int playerID;     //プレイヤーのID
    [SerializeField]
    [Range(0,100)]
    private float hp;         //HP
    [SerializeField]
    private float power;      //攻撃力
    [SerializeField]
    private float defense;    //防御力
    [SerializeField]
    private float speed;      //移動スピード

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
    //ステータスの変化
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
