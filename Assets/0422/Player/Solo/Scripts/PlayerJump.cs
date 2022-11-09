using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    Gamepad gamepad;
    Rigidbody rb;
    [Header("スクリプト")]
    [SerializeField] PlayerStatus_Solo playerStatus;
    [Header("ジャンプ")]
    [SerializeField] Vector3 jumpPow;
    [SerializeField] float jumpWait;
    [SerializeField] bool isGroung;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //プレイヤが存在しないor死んでいるorジャンプ中なら処理をしない
        if (playerStatus.GetState() == PlayerStatus_Solo.State.Non || playerStatus.GetState() == PlayerStatus_Solo.State.Dead ||
            playerStatus.GetState() == PlayerStatus_Solo.State.Jump) { return; }
        if (gamepad == null) { gamepad = Gamepad.current; }

        //地面に着地中
        if(isGroung)
        {
            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
                StartCoroutine("Jump");
            }
        }
    }

    [Header("減速")]
    [SerializeField] float jumpDrag; //ジャンプ中
    [SerializeField] float moveDrag; //走行中

    IEnumerator Jump()
    {
        rb.AddForce(jumpPow, ForceMode.Impulse);

        //プレイヤーをジャンプ状態に変更
        playerStatus.SetState(PlayerStatus_Solo.State.Jump);

        //抵抗の値を変更
        rb.drag = jumpDrag;

        yield return new WaitForSeconds(jumpWait);
        
        //抵抗の値を変更
        rb.drag = moveDrag;
        
        //プレイヤーを待機状態に変更
        playerStatus.SetState(PlayerStatus_Solo.State.Stay);
    }

    //地面との接触判定
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGroung = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGroung = false;
        }
    }
}