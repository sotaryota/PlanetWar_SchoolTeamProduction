using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    Gamepad gamepad;
    Rigidbody rb;
    [Header("�X�N���v�g")]
    [SerializeField] PlayerStatus_Solo playerStatus;
    [SerializeField] PlayerAnimManeger playerAnimation;
    [SerializeField] PlayerGroundCheck playerGround;
    [Header("�W�����v")]
    [SerializeField] Vector3 jumpPow;
    [SerializeField] float jumpWait;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //�v���C�������݂��Ȃ�or����ł���or�W�����v���Ȃ珈�������Ȃ�
        if (playerStatus.GetState() == PlayerStatus_Solo.State.Non || playerStatus.GetState() == PlayerStatus_Solo.State.Dead ||
            playerStatus.GetState() == PlayerStatus_Solo.State.Jump) { return; }
        if (gamepad == null) { gamepad = Gamepad.current; }

        //�n�ʂɒ��n��
        if(playerGround.isGroung)
        {
            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
                StartCoroutine("Jump");
            }
        }
    }

    [Header("����")]
    [SerializeField] float jumpDrag; //�W�����v��
    [SerializeField] float moveDrag; //���s��

    IEnumerator Jump()
    {
        rb.AddForce(jumpPow, ForceMode.Impulse);
        
        //�W�����v���[�V����
        //playerAnimation.PlayAnimSetJump(true);

        //�v���C���[���W�����v��ԂɕύX
        playerStatus.SetState(PlayerStatus_Solo.State.Jump);

        //��R�̒l��ύX
        rb.drag = jumpDrag;

        yield return new WaitForSeconds(jumpWait);
        
        //��R�̒l��ύX
        rb.drag = moveDrag;

        //�W�����v���[�V����
        //playerAnimation.PlayAnimSetJump(false);

        //�v���C���[��ҋ@��ԂɕύX
        playerStatus.SetState(PlayerStatus_Solo.State.Stay);
    }
}