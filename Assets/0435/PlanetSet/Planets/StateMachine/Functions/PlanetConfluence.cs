using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetConfluence : PlanetStateFanction
{
    [Header("登場座標からY軸が０になるまでの時間")]
    [SerializeField]
    private float time;

    [Header("ランダム範囲")]
    [SerializeField]
    private Vector2 minMax;
    private Vector3 targetPos;
    private bool moveFlag;

    private void Start()
    {
        //初期座標を設定
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
    }


    public override PlanetStateMachine.State Fanction(float deltaTime)
    {
        if (moveFlag)
        {
            //Y軸を０まで移動（時間割合）
            transform.position = Vector3.Slerp(transform.position, targetPos, time * deltaTime);
            if (transform.position.y <= 0.1f && transform.position.y >= -0.1f)
            {
                moveFlag = false;
            }
        }

        return PlanetStateMachine.State.Now;
    }
}
