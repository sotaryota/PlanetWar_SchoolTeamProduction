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
    [SerializeField] PlayerAnimManeger playerAnimator;

    [Header("�v���p�e�B")]
    [SerializeField] private GameObject player;
    [SerializeField] private float escapeSpeed;
    [SerializeField] private float escapeCost;
    [SerializeField] private float escapeCoolTime;
    [SerializeField] private bool  isEscape;

    public float GetCost()
    {
        return escapeCost;
    }

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
        EscapeCheck();
    }

    private void EscapeCheck()
    {
        if (playerStatus.GetState() != PlayerStatus.State.Move && playerStatus.GetState() != PlayerStatus.State.Stay) { return; }
        if (!isEscape) { return; }

        //X�{�^�����������Ƃ�
        if (gamepad.buttonWest.wasPressedThisFrame)
        {
            //�f�B�t�F���X�p�����[�^�[������R�X�g��葽����
            if(playerStatus.GetDefense() >= escapeCost)
            {
                StartCoroutine("EscapeMove");
            }
        }
    }

    IEnumerator EscapeMove()
    {
        isEscape = false;

        //������[�V����
        playerAnimator.PlayAnimSetDodge(true);
        //�����Ă�������ɉ������R�X�g���f�B�t�F���X������
        rb.velocity = Vector3.zero;
        rb.AddForce(player.transform.forward * escapeSpeed, ForceMode.Impulse);
        playerStatus.Escape(escapeCost);

        yield return new WaitForSeconds(escapeCoolTime);

        playerAnimator.PlayAnimSetDodge(false);
        isEscape = true;
    }
}
