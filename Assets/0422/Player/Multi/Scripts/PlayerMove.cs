using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("カメラ")]
    [SerializeField]
    Camera myCamera;
    [Header("スクリプト")]
    public PlayerStatus playerStatus;
    [SerializeField]
    PlanetCatchRelease planetCatchRelease;
    Rigidbody rb;
    Gamepad gamepad;
    [SerializeField] PlayerAnimManeger playerAnimator;

    private Vector3 moveDirection;
    private float horizontal;
    private float vertical;
    private float waitcnt = 0.0f;

    Vector3 moveForward;

    private void Start()
    {
        rb  = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //プレイヤが存在しないor死んでいるなら処理をしない
        if(playerStatus.GetState() == PlayerStatus.State.Non || playerStatus.GetState() == PlayerStatus.State.Dead)
        { return; }

        if (gamepad == null)
        {
            gamepad = Gamepad.all[playerStatus.GetID()];
        }

        if (!planetCatchRelease.throwFlag)
        {
            rb.velocity = Vector3.zero;
            return;
        }
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
        if(horizontal <= 0.1 && horizontal >= -0.1 && vertical <= 0.1 && vertical >= -0.1)
        {
            //Catch状態でないなら
            if (playerStatus.GetState() != PlayerStatus.State.Catch)
            {
                //プレイヤをStay状態に
                playerStatus.SetState(PlayerStatus.State.Stay);
            }

            //減速処理
            rb.AddForce(-rb.velocity * (Time.deltaTime * 20));

            //待機時間のカウント
            waitcnt += Time.deltaTime;

            //15秒に1回処理ボイスを鳴らす
            if (waitcnt > 15.0f)
            {
                this.GetComponent<PlayerSEManager>().WaitVoice();

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
            if (playerStatus.GetState() != PlayerStatus.State.Catch)
            {
                //プレイヤをMove状態に
                playerStatus.SetState(PlayerStatus.State.Move);
            }
            //移動処理
            rb.AddForce(moveDirection * Time.deltaTime - rb.velocity * (Time.deltaTime * 20));

            //移動した分スピードを上げる
            playerStatus.SpeedUp(moveDirection.magnitude * Time.deltaTime / 100);

            //待機ボイスのカウントを0に
            waitcnt = 0.0f;

            //アニメーション
            playerAnimator.PlayAnimSetRun(true);

        }
    }
}
