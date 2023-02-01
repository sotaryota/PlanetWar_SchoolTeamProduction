using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cantalk_Icon : MonoBehaviour
{
    [Header("PlayerStatus_Solo�̎Q��")]
    [SerializeField]
    private PlayerStatus_Solo playerStatus;

    [Header("NPCTalking�̎Q��")]
    [SerializeField]
    NPCTalking talk;
    bool cantalk_flag;


    [Header("�b����A�C�R���̎Q��")]
    [SerializeField]
    private Image cantalk_Icon;
    [SerializeField]
    private float distance_x = 0f;
    [SerializeField]
    private float distance_y = 2.5f;

    [Header("���ʉ�")]
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
            //NPC�̈ʒu���擾
            Vector3 UI_Pos = talk.npc.gameObject.transform.position;
            //HP�Q�[�W�̈ʒu�𒲐�
            UI_Pos.x += distance_x;
            //y���T�C�Y�ɂ���Ē������܂��B
            if (talk.npc.GetComponent<CapsuleCollider>().height > 2.2f)
            {
                UI_Pos.y += distance_y * 2;
            }
            else
            {
                UI_Pos.y += distance_y;
            }
            //�z�u
            Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, UI_Pos);
            cantalk_Icon.transform.position = screenPos;
        }
        else
        {
            cantalk_Icon.gameObject.SetActive(false);
        }


    }
}
