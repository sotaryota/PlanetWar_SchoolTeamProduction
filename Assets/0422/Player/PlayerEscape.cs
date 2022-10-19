using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEscape : MonoBehaviour
{
    private Gamepad gamepad;
    private Rigidbody rb;

    [Header("�Q�ƃX�N���v�g")]
    [SerializeField] private PlayerStatus playerStatus;

    [Header("�v���p�e�B")]
    [SerializeField] private GameObject player;
    [SerializeField] private float escapeSpeed;
    [SerializeField] private float escapeCost;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gamepad == null)
        {
            gamepad = Gamepad.all[playerStatus.GetID()];
        }
        EscapeMove();
    }

    private void EscapeMove()
    {
        //X�{�^�����������Ƃ�
        if(gamepad.buttonWest.wasPressedThisFrame)
        {
            //�f�B�t�F���X�p�����[�^�[������R�X�g��葽����
            if(playerStatus.GetDefense() >= escapeCost)
            {
                //�����Ă�������ɉ������R�X�g���f�B�t�F���X������
                rb.AddForce(player.transform.forward * escapeSpeed, ForceMode.Impulse);
                playerStatus.Escape(escapeCost);
            }
        }
    }
}
