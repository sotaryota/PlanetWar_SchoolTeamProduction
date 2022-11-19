using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    Gamepad gamepad;
    CharacterController controller;
    [Header("スクリプト等")]
    [SerializeField] PlayerStatus_Solo playerStatus;
    [SerializeField] PlayerMove_Solo playerMove;
    [SerializeField] Animator playerAnimator;
    [Header("ジャンプ")]
    public Vector3 jumping;
    [SerializeField] float jumpPow;
    [Header("重力")]
    [SerializeField] float gravity;
    [SerializeField] GameObject playerFoot;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //プレイヤが存在しないor死んでいるorジャンプ中なら処理をしない
        if (playerStatus.GetState() == PlayerStatus_Solo.State.Non || playerStatus.GetState() == PlayerStatus_Solo.State.Dead) { return; }
        if (gamepad == null) { gamepad = Gamepad.current; }
        if (playerFoot.GetComponent<PlayerGroundCheck>().isGround)
        {
            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
                StartCoroutine("Jump");
            }
        }
        else
        {
            Debug.Log("重力加算中");
            PlayerGravity();
        }

        controller.Move(jumping * Time.deltaTime);
    }
    private void PlayerGravity()
    {
        jumping.y -= gravity * Time.deltaTime;
    }
    //private void PlayerJump()
    //{
    //    jumping.y = jumpPow;
    //    playerStatus.SetState(PlayerStatus_Solo.State.Jump);
    //}
    IEnumerator Jump()
    {
        jumping.y = jumpPow;
        playerStatus.SetState(PlayerStatus_Solo.State.Jump);

        yield return new WaitForSeconds(0.5f);

        playerStatus.SetState(PlayerStatus_Solo.State.Stay);
    }
}