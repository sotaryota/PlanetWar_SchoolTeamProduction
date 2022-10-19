using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetConfluence : PlanetStateFanction
{
    [Header("合流速度をスクリプトの移動速度に適用する（なくても可）")]
    [SerializeField]
    private PlanetRotateAround PRA;
    private Vector3 startPos;//初期位置（Lerpで使用）
    private Vector3 posBefore;//速度計算に使用

    [Header("合流速度")]
    [SerializeField]
    private float moveTime;
    private float startTime;

    [Header("ランダム範囲")]
    [SerializeField]
    private Vector2 minMax;
    [Header("目標座標（「0,0,0」の場合、座標を自動設定）")]
    [SerializeField]
    private Vector3 targetPos;
    private bool moveFlag;//移動するかを判定（Y軸 ＝０で無効化）

    private void Start()
    {
        //初期座標を設定(「0,0,0」の場合)
        if(targetPos == Vector3.zero)
        {
            //座標はランダム
            float cPosX = 0;
            float cPosZ = 0;
            if (Random.Range(0, 2) == 0)
            {
                //X軸が自由
                cPosX = Random.Range(-minMax.y, minMax.y);
                cPosZ = Random.Range(minMax.x, minMax.y);
            }
            else
            {
                //Z軸が自由
                cPosX = Random.Range(minMax.x, minMax.y);
                cPosZ = Random.Range(-minMax.y, minMax.y);
            }
            targetPos = new Vector3(cPosX, 0, cPosZ);
            moveFlag = true;
            startPos = transform.position;

        }

        //開始時間を得る
        startTime = Time.time;
    }


    public override PlanetStateMachine.State Fanction(float deltaTime)
    {
        if (moveFlag)
        {
            //Y軸を０まで移動（時間割合）
            float distance = Vector3.Distance(startPos, targetPos);
            float moveVal = (Time.time - startTime) / distance;
            transform.position = Vector3.Slerp(startPos, targetPos, moveVal * moveTime);

            //移動速度を計算し適応
            if (PRA)
            {
                //速度計算
                float planetVelocity = (transform.position - posBefore).magnitude / deltaTime;
                //現在の座標保存
                posBefore = transform.position;
                //適用
                PRA.SetMoveSpeed(planetVelocity);
            }

            //座標が０付近になったら
            if (transform.position.y <= 0.1f && transform.position.y >= -0.1f)
            {
                //Y座標を０にする
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                //移動終了
                moveFlag = false;
            }
        }

        return PlanetStateMachine.State.Now;
    }
}
