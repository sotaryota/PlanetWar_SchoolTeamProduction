using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_UI : MonoBehaviour
{
    [SerializeField]
    private Slider HPgauge;

    //"Fill"������
    [SerializeField]
    private GameObject HPgauge_Color;

    //�e�X�g
    //float nowHP = 100f;

    void Start()
    {
        //�ő�l(maxValue)��Slider�ɂ���B���L�͕��
        //float maxHP = 100f;
        //HPgauge.maxValue = maxHP;

        HPgauge.value = HPgauge.maxValue;
    }

    void Update()
    {
        //�e�X�g�p
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
                //������葽����
                if (HPgauge.value > (HPgauge.maxValue / 2))
                {
                    r = 1f - (HPgauge.value - (HPgauge.maxValue / 2f)) / (HPgauge.maxValue / 2);
                    g = 1.0f;
                }
                //�����ȉ��̎�
                else
                {
                    r = 1.0f;
                    g = HPgauge.value / (HPgauge.maxValue / 2);
                }
                HPgauge_Color.GetComponent<Image>().color = new Color(r, g, 0, 1);
    }
}
