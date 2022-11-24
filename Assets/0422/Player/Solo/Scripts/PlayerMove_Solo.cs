using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMove_Solo : MonoBehaviour
{
    [Header("カメラ")]
    [SerializeField]
    Camera myCamera;
    [Header("スクリプト")]
    public PlayerStatus_Solo playerStatus;
    [SerializeField] PlayerAnimManeger playerAnimator;

    public Vector3 moveDirection;
    private float horizontal;
    private float vertical;
    private float waitcnt = 0.0f;

    CharacterController controller;
    Gamepad gamepad;
    Vector3 moveForward;

    private RaycastHit hit;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //プレイヤが存在しないor死んでいるor会話中なら処理をしない
        if (playerStatus.GetState() == PlayerStatus_Solo.State.Non || playerStatus.GetState() == PlayerStatus_Solo.State.Dead ||
            playerStatus.GetState() == PlayerStatus_Solo.State.Talking)
        {
            playerAnimator.PlayAnimSetRun(false);
            return;
        }
        if (gamepad == null) { gamepad = Gamepad.current; }

        StickValue();
        PlayerLook();
        MoveOrStop();
    }

    //--------------------------------------
    //カメラの向きを正面にする処理
    //--------------------------------------

    private void PlayerLook()
    {
        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(myCamera.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定
        moveForward = cameraForward * vertical + myCamera.transform.right * horizontal;

        // 移動方向にスピードを掛ける
        moveDirection = moveForward * playerStatus.GetSpeed();

        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }

    //--------------------------------------
    //スティックの値の取得
    //--------------------------------------

    private void StickValue()
    {
        horizontal = gamepad.leftStick.x.ReadValue();
        vertical = gamepad.leftStick.y.ReadValue();
    }

    //--------------------------------------
    //動きの処理
    //--------------------------------------

    private void MoveOrStop()
    {
        //スティック入力が小さい時
        if (horizontal <= 0.1 && horizontal >= -0.1 && vertical <= 0.1 && vertical >= -0.1)
        {
            //Catch状態でないなら
            if (playerStatus.GetState() != PlayerStatus_Solo.State.Catch && playerStatus.GetState() != PlayerStatus_Solo.State.Jump)
            {
                //プレイヤをStay状態に
                playerStatus.SetState(PlayerStatus_Solo.State.Stay);
            }

            SlopeSlide();

            //待機時間のカウント
            waitcnt += Time.deltaTime;

            //15秒に1回処理ボイスを鳴らす
            if (waitcnt > 15.0f)
            {
                //this.GetComponent<PlayerSEManager>().WaitVoice();

                //待機ボイスのカウントを0に
                waitcnt = 0.0f;

                playerAnimator.PlayAnimAppeal();
            }
            //アニメーション
            playerAnimator.PlayAnimSetRun(false);
        }
        else
        {
            //Catch状態でないなら
            if (playerStatus.GetState() != PlayerStatus_Solo.State.Catch && playerStatus.GetState() != PlayerStatus_Solo.State.Jump)
            {
                //プレイヤをMove状態に
                playerStatus.SetState(PlayerStatus_Solo.State.Move);
            }

            SlopeSlide();
            //移動処理
            controller.Move(moveDirection * Time.deltaTime);

            //待機ボイスのカウントを0に
            waitcnt = 0.0f;

            //アニメーション
            playerAnimator.PlayAnimSetRun(true);

        }
    }

    void SlopeSlide()
    {
        var rayPos       = this.transform.position + new Vector3(0,1,0);
        var rayDirection = this.transform.forward;

        Ray ray = new Ray(rayPos, rayDirection);
        Debug.DrawRay(rayPos, rayDirection, Color.red,50f); ;
        if(Physics.Raycast(ray,out hit,1.0f))
        {
            Debug.Log("1");
            if(hit.transform.CompareTag("Ground"))
            {
                Debug.Log("2");
                if (Vector3.Angle(hit.normal,Vector3.up) > controller.slopeLimit)
                {
                    Debug.Log("3");
                    //滑るフラグが立ってたら
                    Debug.Log("滑る処理です");
                    Vector3 hitNormal = hit.normal;
                    moveDirection.x = hitNormal.x;
                    moveDirection.y = 20 * Time.deltaTime;//重力落下
                    moveDirection.z = hitNormal.z;
                }
            }
        }
    }

}