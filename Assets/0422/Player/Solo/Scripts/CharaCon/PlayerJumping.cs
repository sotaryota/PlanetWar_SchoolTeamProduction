using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumping : MonoBehaviour
{
    Gamepad gamepad;
    CharacterController controller;
    [Header("�X�N���v�g��")]
    [SerializeField] PlayerStatus_Solo playerStatus;
    [SerializeField] PlayerMovement playerMove;
    [SerializeField] Animator playerAnimator;
    [Header("�W�����v")]
    [SerializeField] Vector3 jumping;
    [SerializeField] float jumpPow;
    [Header("�d��")]
    [SerializeField] float gravity;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //�v���C�������݂��Ȃ�or����ł���or�W�����v���Ȃ珈�������Ȃ�
        if (playerStatus.GetState() == PlayerStatus_Solo.State.Non || playerStatus.GetState() == PlayerStatus_Solo.State.Dead ||
            playerStatus.GetState() == PlayerStatus_Solo.State.Jump) { return; }
        if (gamepad == null) { gamepad = Gamepad.current; }
        if(controller.isGrounded)
        {
            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
                Debug.Log("�W�����v");
                PlayerJump();
            }
        }
        else
        {
            Debug.Log("�n��");
            PlayerGravity();
        }

        controller.Move(jumping * Time.deltaTime);
    }
    private void PlayerGravity()
    {
        jumping.y -= gravity * Time.deltaTime;
    }
    private void PlayerJump()
    {
        jumping.y = jumpPow;
    }
}
