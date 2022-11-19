using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumping : MonoBehaviour
{
    Gamepad gamepad;
    CharacterController controller;
    [Header("スクリプト等")]
    [SerializeField] PlayerStatus_Solo playerStatus;
    [SerializeField] PlayerMovement playerMove;
    [SerializeField] Animator playerAnimator;
    [Header("ジャンプ")]
    [SerializeField] Vector3 jumping;
    [SerializeField] float jumpPow;
    [Header("重力")]
    [SerializeField] float gravity;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //プレイヤが存在しないor死んでいるorジャンプ中なら処理をしない
        if (playerStatus.GetState() == PlayerStatus_Solo.State.Non || playerStatus.GetState() == PlayerStatus_Solo.State.Dead ||
            playerStatus.GetState() == PlayerStatus_Solo.State.Jump) { return; }
        if (gamepad == null) { gamepad = Gamepad.current; }
        if(controller.isGrounded)
        {
            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
                Debug.Log("ジャンプ");
                PlayerJump();
            }
        }
        else
        {
            Debug.Log("地上");
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
