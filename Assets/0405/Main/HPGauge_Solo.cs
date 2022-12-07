using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPGauge_Solo : MonoBehaviour
{
    [SerializeField]
    private PlayerStatus_Solo playerstatus;
    [SerializeField]
    private Image HPBlackGauge;
    //"Fill"‚ð“ü‚ê‚é
    //[SerializeField]
    //private GameObject HPgauge_Color;

    [SerializeField]
    Animator damageMotion;

    float maxHP = 100f;
    float nowHP;
    float beforeHp;
    void Start()
    {
        HPBlackGauge.fillAmount = 0;
        maxHP = playerstatus.GetHp();
        nowHP = maxHP;
        beforeHp = nowHP;
    }

    void Update()
    {
        nowHP = playerstatus.GetHp();
        //ƒQ[ƒW‚Ì•‰æ‘œ‚ðŒ¸‚ç‚·ˆ—
        GaugeUpdate(nowHP);

        DamageMotion();
    }
    public void GaugeUpdate(float nowHP)
    {
        HPBlackGauge.fillAmount = 1f - (nowHP / maxHP);

        //Slider‚Ìê‡
        //drawGauge();
    }

    void DamageMotion()
    {
        if (beforeHp > nowHP)
        {
            damageMotion.SetTrigger("damage");
            beforeHp = nowHP;
        }
    }
}