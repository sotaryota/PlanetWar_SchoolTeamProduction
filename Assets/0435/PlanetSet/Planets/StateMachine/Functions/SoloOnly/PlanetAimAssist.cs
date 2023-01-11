using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetAimAssist : PlanetStateFanction
{
    [Header("エイムアシスト：補正する敵（自動参照）"), SerializeField]
    private GameObject targetEnemy;
    private bool assistFlag = true;

    [Header("カメラを挿入"), SerializeField]
    private Camera targetCamera;

    [Header("画面の中心からアシストを行う範囲"), Range(0,1), SerializeField]
    private float assistScreenArea;

    [Header("補完最大距離"), SerializeField]
    private float assistDistance;

    [Header("補完値"), SerializeField]
    private float rotateValue;

    public override PlanetStateMachine.State Fanction(float deltaTime)
    {
        if (assistFlag)
        {
            //１度しか判定を行わない
            assistFlag = false;

            SearchEnemy();

            if (targetEnemy)
            {
                Assist();
            }
        }

        return PlanetStateMachine.State.Now;
    }

    private void SearchEnemy()
    {
        //敵タグが付いているオブジェクトの検索を行う
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");

        //判定用カメラを有効化
        targetCamera.enabled = true;

        //検索した敵の数だけ判定を行う
        foreach (GameObject en in enemys)
        {
            //敵の座標を求める
            var enemyScreenPos = targetCamera.WorldToViewportPoint(en.transform.position);

            //画面中心からのアシストエリアを作成
            float middle = 0.5f - (assistScreenArea / 2);
            float size = assistScreenArea;
            Rect area = new Rect(middle, middle, size, size);

            //アシストエリア内かを算出する
            if (area.Contains(enemyScreenPos))
            {
                if (Vector3.Dot((this.transform.position - en.transform.position).normalized, transform.forward) <= 0) { 
                    //参照がある場合は判定し保存、ない場合はそのまま保存
                    if (targetEnemy)
                    {
                        //敵が登録されている敵より近かった場合は入れ替え
                        float nowTargetPos = (targetEnemy.transform.position - this.transform.position).magnitude;
                        float newTargetPos = (en.transform.position - this.transform.position).magnitude;
                        if (newTargetPos < nowTargetPos)
                        {
                            targetEnemy = en;
                        }
                    }
                    else
                    {
                        //参照が無いので保存
                        targetEnemy = en;
                    }
            }
            }          
        }

        //容量削減の為カメラ削除
        //Destroy(targetCamera);
    }

    public void Assist()
    {
        //ほんの少しだけ敵の方向へ傾く処理
        float targetDis = (targetEnemy.transform.position - this.transform.position).magnitude;
        if(targetDis <= assistDistance)
        {
            Vector3 targetPos = targetEnemy.transform.position;
            targetPos.y = this.transform.position.y;
            this.transform.LookAt(targetPos);
        }
    }
}
