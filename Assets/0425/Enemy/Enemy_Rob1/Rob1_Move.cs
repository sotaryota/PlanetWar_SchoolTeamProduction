using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rob1_Move : MonoBehaviour
{
    [Header("カメラ")]
    [Header("スクリプト")]
    public Rob1_Status rob1Status;

    [SerializeField] Rob1_AnimManeger rob1Animator;

    [SerializeField] GameObject player;

    [SerializeField] bool sensing = false;

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
        else
        {
            rob1Animator.PlayRob1AnimSetRun(false);
        }
    }

    //--------------------------------------
    //プレイヤーの向きを向く
    //--------------------------------------

    private void PlayerLook()
    {
        transform.LookAt(player.transform);
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
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, rob1Status.GetSpeed() * Time.deltaTime);

        //アニメーション
        rob1Animator.PlayRob1AnimSetRun(true);
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
            sensing = true;
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            sensing = false;
    }
}
