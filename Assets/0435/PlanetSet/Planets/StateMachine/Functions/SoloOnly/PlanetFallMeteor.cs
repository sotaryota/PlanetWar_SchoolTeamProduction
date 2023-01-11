using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetFallMeteor : PlanetStateFanction
{
    private Vector3 startPos;//初期位置（Lerpで使用）

    [Header("接地時エフェクト")]
    [SerializeField]
    private GameObject crashEffect;

    [Header("落下速度")]
    [SerializeField]
    private float moveTime;
    private float startTime;

    [Header("ターゲット座標")]
    public Vector3 targetPos;//ターゲット座標

    private bool moveFlag;//移動するかを判定（「 Y軸 <= ターゲット座標Y 」で無効化）
    public bool GetMoveFlag() { return moveFlag; }

    private void Start()
    {
        //初期位置保存
        startPos = transform.position;

        //移動開始
        moveFlag = true;

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
            if (transform.position.y <= targetPos.y)
            {
                //Y座標を指定の座標へ上書き
                transform.position = new Vector3(transform.position.x, targetPos.y, transform.position.z);

                //エフェクト生成
                GameObject effect = Instantiate(crashEffect);
                effect.transform.position = this.transform.position;

                //移動終了
                moveFlag = false;
            }

        }

        return PlanetStateMachine.State.Now;
    }
}
