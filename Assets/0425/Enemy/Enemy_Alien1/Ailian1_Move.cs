using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ailian1_Move : MonoBehaviour
{
    [Header("カメラ")]
    [SerializeField]
    Camera myCamera;
    [Header("スクリプト")]
    public PlayerStatus playerStatus;
    [SerializeField]
    PlanetCatchRelease planetCatchRelease;
    Rigidbody rb;
   
    [SerializeField] PlayerAnimManeger playerAnimator;

    private Vector3 moveDirection;
  
    Vector3 moveForward;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //プレイヤが存在しないor死んでいるなら処理をしない
        if (playerStatus.GetState() == PlayerStatus.State.Non || playerStatus.GetState() == PlayerStatus.State.Dead)
        { return; }

   
        PlayerLook();
        MoveOrStop();

    }

    //--------------------------------------
    //カメラの向きを正面にする処理
    //--------------------------------------

    private void PlayerLook()
    {
        

        // 移動方向にスピードを掛ける
        moveDirection = moveForward * playerStatus.GetSpeed();

        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }

    //--------------------------------------
    //動きの処理
    //--------------------------------------
    private void MoveOrStop()
    {
        //スティック入力が小さい時
        //if (horizontal <= 0.1 && horizontal >= -0.1 && vertical <= 0.1 && vertical >= -0.1)
        //{
        //    //Catch状態でないなら
        //    if (playerStatus.GetState() != PlayerStatus.State.Catch)
        //    {
        //        //プレイヤをStay状態に
        //        playerStatus.SetState(PlayerStatus.State.Stay);
        //    }

        //    //減速処理
        //    rb.AddForce(-rb.velocity * (Time.deltaTime * 20));

        //    //アニメーション
        //    playerAnimator.PlayAnimSetRun(false);
        //}
        //else
        //{
        //    //Catch状態でないなら
        //    if (playerStatus.GetState() != PlayerStatus.State.Catch)
        //    {
        //        //プレイヤをMove状態に
        //        playerStatus.SetState(PlayerStatus.State.Move);
        //    }
        //    //移動処理
        //    rb.AddForce(moveDirection * Time.deltaTime - rb.velocity * (Time.deltaTime * 20));

        //    //移動した分スピードを上げる
        //    playerStatus.SpeedUp(moveDirection.magnitude * Time.deltaTime / 100);


        //    //アニメーション
        //    playerAnimator.PlayAnimSetRun(true);

        //}
    }
}
