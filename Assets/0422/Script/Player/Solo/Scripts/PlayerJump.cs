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
    [Header("重力")]
    public Vector3 jumping;                 //ジャンプ用
    [SerializeField] float gravity;         //重力
    [SerializeField] GameObject playerFoot; //足元の判定


    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //プレイヤが存在しないor死んでいるor会話中なら処理をしない
        if (playerStatus.GetState() == PlayerStatus_Solo.State.Non || playerStatus.GetState() == PlayerStatus_Solo.State.Dead ||
            playerStatus.GetState() == PlayerStatus_Solo.State.Talking) { return; }
        if (gamepad == null) { gamepad = Gamepad.current; }

        //地面に接地していてボタンを押したとき
        if (playerFoot.GetComponent<PlayerGroundCheck>().isGround)
        {
            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
                StartCoroutine("Jump");
            }
        }
        else
        {
            //接地していないなら重力加算
            Debug.Log("重力加算中");
            PlayerGravity();
        }
        //移動処理
        controller.Move(jumping * Time.deltaTime);
    }
    //重力
    private void PlayerGravity()
    {
        jumping.y -= gravity * Time.deltaTime;
    }

    //ジャンプ用コルーチン
    IEnumerator Jump()
    {
        jumping.y = playerStatus.GetJumpPower();
        playerStatus.SetState(PlayerStatus_Solo.State.Jump);

        yield return new WaitForSeconds(0.5f);

        playerStatus.SetState(PlayerStatus_Solo.State.Stay);
    }
}