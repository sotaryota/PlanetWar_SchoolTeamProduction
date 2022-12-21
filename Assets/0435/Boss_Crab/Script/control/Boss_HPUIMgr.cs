using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_HPUIMgr : MonoBehaviour
{
    [SerializeField]
    private Enemy_HpData status;
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
        maxHP = status.GetHp();
        nowHP = maxHP;
        beforeHp = nowHP;
    }

    void Update()
    {
        nowHP = status.GetHp();
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
