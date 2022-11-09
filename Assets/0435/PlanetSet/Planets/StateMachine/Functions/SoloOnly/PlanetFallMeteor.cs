using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetFallMeteor : PlanetStateFanction
{
    private Vector3 startPos;//初期位置（Lerpで使用）

    [Header("プレイヤー位置参照")]
    [SerializeField]
    private GameObject player;

    [Header("接地時エフェクト")]
    [SerializeField]
    private GameObject crashEffect;

    [Header("落下速度")]
    [SerializeField]
    private float moveTime;
    private float startTime;

    [Header("目標座標（「0,0,0」の場合、座標を自動設定）")]
    [SerializeField]
    private Vector3 targetPos;
    [Header("目標座標設定")]
    [SerializeField]
    private Vector2 Range_X;
    [SerializeField]
    private float posY;
    [SerializeField]
    private Vector2 Range_Z;

    private bool moveFlag;//移動するかを判定（Y軸 ＝０で無効化）

    private void Start()
    {
        //初期座標を設定(「0,0,0」の場合)
        if (targetPos == Vector3.zero)
        {
            TargetPosSetting();
        }

        if (!player)
        {
            print("PlanetFallMeteor:プレイヤーを参照すると隕石がプレイヤー直近に落下しなくなります");
        }

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
            if (transform.position.y <= 0)
            {
                //Y座標を０にする
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);

                //エフェクト生成
                GameObject effect = Instantiate(crashEffect);
                effect.transform.position = this.transform.position;

                //移動終了
                moveFlag = false;
            }

            //ターゲット座標とプレイヤーが近かった場合に座標再設定
            if (player)
            {
                if ((player.transform.position - targetPos).magnitude >= 3)
                {
                    TargetPosSetting();
                }
            }
        }

        return PlanetStateMachine.State.Now;
    }

    void TargetPosSetting()
    {
        //初期位置保存
        startPos = transform.position;

        //座標はランダム
        float cPosX = Random.Range(Range_X.x, Range_X.y);
        float cPosZ = Random.Range(Range_Z.x, Range_Z.y);
        targetPos = new Vector3(cPosX, posY, cPosZ);
    }
}
