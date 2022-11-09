using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveLimit : MonoBehaviour
{
    [SerializeField] //中心になるオブジェクト
    private GameObject centerObj;

    [SerializeField] //移動範囲の半径
    private int moveLimitRadius;

    private void Update()
    {
        MoveLimit();
    }

    //--------------------------------------
    //移動範囲の制限
    //--------------------------------------

    void MoveLimit()
    {
        //プレイヤの位置が移動可能範囲より大きい時
        if(Vector3.Distance(transform.position,centerObj.transform.position) > moveLimitRadius)
        {
            //中心オブジェクトからのベクトルを取得
            Vector3 limitPos = transform.position - centerObj.transform.position;

            //ベクトルの正規化
            limitPos.Normalize();

            //ベクトルをもとにプレイヤの位置を移動制限位置に
            transform.position = limitPos * moveLimitRadius;
        }
    }
}
