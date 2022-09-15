using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_UI : MonoBehaviour
{
    [SerializeField]
    private Slider HPgauge;

    //"Fill"を入れる
    [SerializeField]
    private GameObject HPgauge_Color;

    //テスト
    //float nowHP = 100f;

    void Start()
    {
        //最大値(maxValue)はSliderにある。下記は補遺
        //float maxHP = 100f;
        //HPgauge.maxValue = maxHP;

        HPgauge.value = HPgauge.maxValue;
    }

    void Update()
    {
        //テスト用
        //nowHP -= 0.1f;
        //GaugeUpdate(nowHP);
    }

    public void GaugeUpdate(float nowHP)
    {
        HPgauge.value = nowHP;
        drawGauge();
    }

    void drawGauge()
    {
        float r, g;
                //半分より多い時
                if (HPgauge.value > (HPgauge.maxValue / 2))
                {
                    r = 1f - (HPgauge.value - (HPgauge.maxValue / 2f)) / (HPgauge.maxValue / 2);
                    g = 1.0f;
                }
                //半分以下の時
                else
                {
                    r = 1.0f;
                    g = HPgauge.value / (HPgauge.maxValue / 2);
                }
                HPgauge_Color.GetComponent<Image>().color = new Color(r, g, 0, 1);
    }
}
