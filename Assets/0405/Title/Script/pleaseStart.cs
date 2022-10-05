using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pleaseStart : MonoBehaviour
{
    //時間の計測
    float TimeCnt;
    //表示時間
    [SerializeField]
    float rightTime;
    //非表示時間
    [SerializeField]
    float deleteTime;
    //表示中か否か
    bool isrighting;

    [SerializeField]
    Image pleaseStartlogo;
    float alpha;

    void Start()
    {
        TimeCnt = 0f;
        //rightTime = 2f;
        //deleteTime = 0.5f;
        isrighting = true;
        alpha = 1.0f;
        pleaseStartlogo = GetComponent<Image>();
    }
    void Update()
    {
        //計測
        TimeCnt += Time.deltaTime;

        //表示中なら
        if (isrighting)
        {
            //表示時間分まで表示
            if (TimeCnt >= rightTime)
            {
                //フラグを変えて非表示&リセット
                isrighting = false;
                alpha = 0.0f;
                TimeCnt = 0f;
            }
        }
        //非表示なら非表示時間分まで測って
        else if (TimeCnt >= deleteTime)
        {
            //フラグを変えて表示&リセット
            isrighting = true;
            alpha = 1.0f;
            TimeCnt = 0f;
        }
        Color colorCopy = pleaseStartlogo.color;
        pleaseStartlogo.color = new Color(colorCopy.r, colorCopy.g, colorCopy.b, alpha);
    }
}