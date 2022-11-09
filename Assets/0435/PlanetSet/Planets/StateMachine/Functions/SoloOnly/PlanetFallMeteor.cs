using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetFallMeteor : PlanetStateFanction
{
    private Vector3 startPos;//初期位置（Lerpで使用）

    [Header("合流速度")]
    [SerializeField]
    private float moveTime;
    private float startTime;

    [Header("ポジション設定")]
    [SerializeField]
    private Vector2 Range_X;
    [SerializeField]
    private float posY;
    [SerializeField]
    private Vector2 Range_Z;


    [Header("目標座標（「0,0,0」の場合、座標を自動設定）")]
    [SerializeField]
    private Vector3 targetPos;
    private bool moveFlag;//移動するかを判定（Y軸 ＝０で無効化）

    private void Start()
    {
        //初期座標を設定(「0,0,0」の場合)
        if (targetPos == Vector3.zero)
        {
            //座標はランダム
            float cPosX = Random.Range(Range_X.x, Range_X.y);
            float cPosZ = Random.Range(Range_Z.x, Range_Z.y);
            targetPos = new Vector3(cPosX, posY, cPosZ);
        }

        //移動開始
        moveFlag = true;

        //初期位置保存
        startPos = transform.position;

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
            transform.position = Vector3.Lerp(startPos, targetPos, moveVal * moveTime);

            //座標が０付近になったら
            if (transform.position.y <= 0)
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
