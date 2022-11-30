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
    //"Fill"‚ğ“ü‚ê‚é
    //[SerializeField]
    //private GameObject HPgauge_Color;

    float maxHP = 100f;
    void Start()
    {
        HPBlackGauge.fillAmount = 0;
        maxHP = playerstatus.GetHp();
    }

    void Update()
    {
        //ƒQ[ƒW‚Ì•‰æ‘œ‚ğŒ¸‚ç‚·ˆ—
        GaugeUpdate(playerstatus.GetHp());
    }
    public void GaugeUpdate(float nowHP)
    {
        HPBlackGauge.fillAmount = 1f - (nowHP / maxHP);

        //Slider‚Ìê‡
        //drawGauge();
    }
}
