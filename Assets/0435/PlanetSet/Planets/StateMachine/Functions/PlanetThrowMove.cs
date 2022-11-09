using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// 惑星が投げられている際の動作(Rigidbodyなしの場合)
/// 
/// </summary>

public class PlanetThrowMove : PlanetStateFanction
{
    [Header("惑星の投げられた際の速度")]
    [SerializeField]
    private Vector3 beThrowSpeed;

    [Header("投げられた際の方向(0~360)。惑星はその方向を向くようになる")]
    [SerializeField]
    private Vector3 beThrowAngleValue;

    [Header("投げられた際のエフェクト")]
    [SerializeField]
    private GameObject effectObject;

    private bool throwOnce;//最初の１回のみ処理を行わせるためのフラグ

    public void ThrowMoveSetting(Vector3 speed, Vector3 angle)
    {
        beThrowSpeed = speed;
        beThrowAngleValue = angle;
    }

    private void Start()
    {
        throwOnce = true;
    }

    public override PlanetStateMachine.State Fanction(float deltaTime)
    {
        //=====最初の１フレームのみ処理を行う(要するにStart()と同義)=======================================
        if (throwOnce)
        {
            //投げられる方向に向ける
            this.transform.rotation = Quaternion.Euler(beThrowAngleValue);
            //エフェクト有効化
            if(effectObject)
                effectObject.SetActive(true);

            throwOnce = false;
        }
        //================================================================================================


        //=====以下、毎フレーム処理を行う(要するにUpdate())====================================================

        //向いている方向へ移動
        this.transform.Translate(beThrowSpeed * Time.deltaTime);
        

        //================================================================================================
        //終了
        return PlanetStateMachine.State.Now;
    }
}
