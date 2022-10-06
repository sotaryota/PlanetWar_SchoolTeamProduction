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
    //"Fill"������
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
        //�Q�[�W�̍��摜�����炷����
        GaugeUpdate(playerstatus.GetHp());
    }
    public void GaugeUpdate(float nowHP)
    {
        //�e�X�g�p
        nowHP -= 50f;

        HPBlackGauge.fillAmount = nowHP / maxHP;

        //Slider�̏ꍇ
        //drawGauge();
    }
}
