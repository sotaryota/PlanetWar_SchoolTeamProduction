using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    Gamepad gamepad;
    CharacterController controller;
    [Header("�X�N���v�g��")]
    [SerializeField] PlayerStatus_Solo playerStatus;
    [SerializeField] PlayerMove_Solo playerMove;
    [SerializeField] Animator playerAnimator;
    [Header("�d��")]
    public Vector3 jumping;                 //�W�����v�p
    [SerializeField] float gravity;         //�d��
    [SerializeField] GameObject playerFoot; //�����̔���


    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //�v���C�������݂��Ȃ�or����ł���or��b���Ȃ珈�������Ȃ�
        if (playerStatus.GetState() == PlayerStatus_Solo.State.Non || playerStatus.GetState() == PlayerStatus_Solo.State.Dead ||
            playerStatus.GetState() == PlayerStatus_Solo.State.Talking) { return; }
        if (gamepad == null) { gamepad = Gamepad.current; }

        //�n�ʂɐڒn���Ă��ă{�^�����������Ƃ�
        if (playerFoot.GetComponent<PlayerGroundCheck>().isGround)
        {
            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
                StartCoroutine("Jump");
            }
        }
        else
        {
            //�ڒn���Ă��Ȃ��Ȃ�d�͉��Z
            Debug.Log("�d�͉��Z��");
            PlayerGravity();
        }
        //�ړ�����
        controller.Move(jumping * Time.deltaTime);
    }
    //�d��
    private void PlayerGravity()
    {
        jumping.y -= gravity * Time.deltaTime;
    }

    //�W�����v�p�R���[�`��
    IEnumerator Jump()
    {
        jumping.y = playerStatus.GetJumpPower();
        playerStatus.SetState(PlayerStatus_Solo.State.Jump);

        yield return new WaitForSeconds(0.5f);

        playerStatus.SetState(PlayerStatus_Solo.State.Stay);
    }
}