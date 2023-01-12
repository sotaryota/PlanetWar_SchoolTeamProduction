using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_HPgauge : MonoBehaviour
{
    [Header("�G�̗̑͏���}��")]
    [SerializeField]
    Enemy_HpData hpData;
    float maxHP;
    float nowHP;
    float beforeHP;

    [Header("���Q�[�W�̎Q��")]
    [SerializeField]
    private Image HPBlackGauge;


   [Header("�_���[�W���ʉ�")]
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip damageSound; 

    void Start()
    {
        HPBlackGauge.fillAmount = 0;
        maxHP = hpData.GetHp();
        nowHP = maxHP;
        beforeHP = nowHP;
        audioSource = GameObject.Find("Player_Solo").GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!hpData)
        {
            Destroy(gameObject);
        }
        else
        {
            Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, hpData.gameObject.transform.position);
            this.transform.position = screenPos;
        }
        
        
        nowHP = hpData.GetHp();
        if (nowHP < beforeHP)
        {
            receive_damage();
        }
    }

    void receive_damage()
    {
        GaugeUpdate(nowHP);

        beforeHP = nowHP;
    }
    void GaugeUpdate(float nowHP_)
    {
        HPBlackGauge.fillAmount = 1f - (nowHP_ / maxHP);
    }
}
