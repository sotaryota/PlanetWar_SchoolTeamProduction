using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cantalk_Icon : MonoBehaviour
{
    [Header("PlayerStatus_Soloの参照")]
    [SerializeField]
    private PlayerStatus_Solo playerStatus;

    [Header("NPCTalkingの参照")]
    [SerializeField]
    NPCTalking talk;
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
        cantalk_flag = talk.buttonFlag;
        audioSource = GameObject.Find("Player_Solo").GetComponent<AudioSource>();
    }

    void Update()
    {
        bool uiActive = false;
        if (talk.npc) {
            uiActive = !talk.isTalking;
        }

        if (uiActive)
        {
            if (playerStatus.GetState() == PlayerStatus_Solo.State.Non || playerStatus.GetState() == PlayerStatus_Solo.State.Dead ||
                playerStatus.GetState() == PlayerStatus_Solo.State.Talking)
            {
                cantalk_Icon.gameObject.SetActive(false);
                return;
            }

            cantalk_Icon.gameObject.SetActive(true);
            //NPCの位置を取得
            Vector3 UI_Pos = talk.npc.gameObject.transform.position;
            //HPゲージの位置を調整
            UI_Pos.x += distance_x;
            //yをサイズによって調整します。
            if (talk.npc.GetComponent<CapsuleCollider>().height > 2.2f)
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
