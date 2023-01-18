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
    [SerializeField]
    private float distance = 2.5f;


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
            //�G�̈ʒu���擾
            Vector3 UI_Pos = hpData.gameObject.transform.position;
            //HP�Q�[�W�̈ʒu�𒲐�
            UI_Pos.y += distance;
            //�z�u
            Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, UI_Pos);
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
