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

    [Header("HP�𐳖ʂɕ\�����鏈��")]
    [SerializeField]
    private Image HPGauge;

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

            InCameraUI();
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
    void InCameraUI()
    {
        GameObject myEnemy = hpData.gameObject;
        Camera camera = Camera.main;

        //��ʂ̒�
        Vector2 enemyScreenPos = camera.WorldToViewportPoint(myEnemy.transform.position);
        Rect judgeRect = new Rect(0, 0, Screen.width, Screen.height);
        bool inCameraJudge = judgeRect.Contains(enemyScreenPos);

        //�������O
        Vector3 judgeVec = myEnemy.transform.position - camera.transform.position;
        judgeVec.y = 0;

        Image greenImg = this.GetComponent<Image>();
        GameObject child = this.transform.GetChild(0).gameObject;
        Image blackImg = child.GetComponentInChildren<Image>();

        //bool ans = inCameraJudge && frontJudge;
        //greenImg.enabled = ans;
        //blackImg.enabled = ans;

    }
}
