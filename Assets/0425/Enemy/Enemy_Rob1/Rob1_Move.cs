using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rob1_Move : MonoBehaviour
{
    [Header("カメラ")]
    [Header("スクリプト")]
    public Rob1_Status rob1Status;
    Rigidbody rb;
    [SerializeField] Rob1_AnimManeger rob1Animator;

    private Vector3 moveDirection;

    Vector3 moveForward;

    [SerializeField] GameObject player;

    [SerializeField] bool sensing = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //エネミーが存在しないor死んでいるなら処理をしない
        if (rob1Status.GetState() == Rob1_Status.State.Non || rob1Status.GetState() == Rob1_Status.State.Dead)
        { return; }

        PlayerLook();

        if (sensing == false)
        {
            MoveOrStop();
        }
    }

    //--------------------------------------
    //カメラの向きを正面にする処理
    //--------------------------------------

    private void PlayerLook()
    {
        this.transform.LookAt(player.transform);

        // 方向キーの入力値とカメラの向きから、移動方向を決定
        moveForward = Vector3.forward;

        // 移動方向にスピードを掛ける
        moveDirection = moveForward * rob1Status.GetSpeed();

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

            //Catch状態でないなら
            if (rob1Status.GetState() != Rob1_Status.State.Catch)
            {
                //プレイヤをMove状態に
                rob1Status.SetState(Rob1_Status.State.Move);
            }

            //移動処理
            rb.AddForce(moveDirection * Time.deltaTime - rb.velocity * (Time.deltaTime * 20));

            //アニメーション
            rob1Animator.PlayRob1AnimSetRun(true);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other == player)
            sensing = true;
    }

    public void OnTriggerExit(Collider other)
    {
        if (other == player)
            sensing = false;
    }
}
