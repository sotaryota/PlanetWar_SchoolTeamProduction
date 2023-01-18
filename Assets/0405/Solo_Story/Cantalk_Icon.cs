using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cantalk_Icon : MonoBehaviour
{
    [Header("Write_Effectの参照")]
    [SerializeField]
    Write_Effect effect;
    bool cantalk_flag;


    [Header("話せるアイコンの参照")]
    [SerializeField]
    private Image cantalk_Icon;
    [SerializeField]
    private float distance_x = 0f;
    [SerializeField]
    private float distance_y = 2.5f;

    [Header("効果音")]
    [SerializeField]
    private AudioSource audioSource;
    //[SerializeField]
    //private AudioClip CantalkSound;

    void Start()
    {
        cantalk_flag = effect.buttonFlag;
        audioSource = GameObject.Find("Player_Solo").GetComponent<AudioSource>();
    }

    void Update()
    {
        bool uiActive = false;
        if (effect.npc) {
            uiActive = !effect.isTalking;
        }

        if (uiActive)
        {
            cantalk_Icon.gameObject.SetActive(true);
            //NPCの位置を取得
            Vector3 UI_Pos = effect.npc.gameObject.transform.position;
            //HPゲージの位置を調整
            UI_Pos.x += distance_x;
            //yをサイズによって調整します。
            if (effect.npc.GetComponent<CapsuleCollider>().height > 2.2f)
            {
                UI_Pos.y += distance_y * 2;
            }
            else
            {
                UI_Pos.y += distance_y;
            }
            //配置
            Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, UI_Pos);
            cantalk_Icon.transform.position = screenPos;
        }
        else
        {
            cantalk_Icon.gameObject.SetActive(false);
        }


    }
}
