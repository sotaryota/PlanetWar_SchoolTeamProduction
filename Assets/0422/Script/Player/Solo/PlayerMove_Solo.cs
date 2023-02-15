using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMove_Solo : MonoBehaviour
{
    [Header("カメラとオーディオ")]
    [SerializeField] Camera myCamera;
    [SerializeField] AudioSource audio;
    public AudioClip moveSE;
    [Header("スクリプト")]
    public PlayerStatus_Solo playerStatus;
    [SerializeField] PlayerDataManager playerData;
    [SerializeField] PlayerAnimManeger playerAnimator;
    [SerializeField] PlayerGroundCheck ground;
    [SerializeField] PauseMenuSystem pause;
    [SerializeField] BattleToStory battleToStory;

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
        pause = pause.GetComponent<PauseMenuSystem>();
    }

    private void Update()
    {
        //ポーズ中は処理をしない
        if (pause.PauseJudge()) { return; }

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
            if (playerStatus.GetState() != PlayerStatus_Solo.State.Catch && playerStatus.GetState() != PlayerStatus_Solo.State.Jump)
            {
                //プレイヤをMove状態に
                playerStatus.SetState(PlayerStatus_Solo.State.Move);
            }

            //移動処理
            controller.Move(moveDirection * Time.deltaTime);

            //待機ボイスのカウントを0に
            waitcnt = 0.0f;

            //アニメーション
            playerAnimator.PlayAnimSetRun(true);

        }
    }
    void RunSE()
    {
        audio.PlayOneShot(moveSE);
    }
}