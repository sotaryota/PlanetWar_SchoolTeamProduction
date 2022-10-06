using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPGauge_UI : MonoBehaviour
{
    [SerializeField]
    private PlayerStatus playerstatus;
    [SerializeField]
    private Image HPBlackGauge;
    //"Fill"を入れる
    [SerializeField]
    private GameObject HPgauge_Color;

    float maxHP = 100f;
    void Start()
    {
        HPBlackGauge.fillAmount = 0;
        maxHP = playerstatus.GetHp();
    }

    void Update()
    {
        //ゲージの黒画像を減らす処理
        GaugeUpdate(playerstatus.GetHp());
    }
    public void GaugeUpdate(float nowHP)
    {
        //テスト用
        nowHP -= 50f;

        HPBlackGauge.fillAmount = nowHP / maxHP;

        //Sliderの場合
        //drawGauge();
    }
}
