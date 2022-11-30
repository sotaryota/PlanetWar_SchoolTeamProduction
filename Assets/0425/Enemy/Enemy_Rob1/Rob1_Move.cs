using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rob1_Move : MonoBehaviour
{
    [Header("カメラ")]
    [Header("スクリプト")]
    [SerializeField] Rob1_Status rob1Status;
    [SerializeField] Rob1_Attack rob1_Attack;

    [SerializeField] Rob1_AnimManeger rob1Animator;

    GameObject player;

    bool die = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        //エネミーが存在しないor死んでいるなら処理をしない
        if (rob1Status.GetState() == Rob1_Status.State.Non || rob1Status.GetState() == Rob1_Status.State.Dead)
        {
            if (die == false)
            {
                rob1Animator.PlayRob1AnimDie();
                die = true;
            }
            return; }

        PlayerLook();

        if (rob1_Attack.sensing == false)
        {
            MoveOrStop();
        }
        else
        {
            rob1Animator.PlayRob1AnimSetRun(false);
        }
    }

    //--------------------------------------
    //プレイヤーの方向に向く
    //--------------------------------------

    private void PlayerLook()
    {
        Vector3 lookPos = player.transform.position;
        lookPos.y = this.transform.position.y;
        transform.LookAt(lookPos);
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
        Vector3 movePos = player.transform.position;
        movePos.y = this.transform.position.y; ;
        transform.position = Vector3.MoveTowards(transform.position, movePos, rob1Status.GetSpeed() * Time.deltaTime);

        //アニメーション
        rob1Animator.PlayRob1AnimSetRun(true);
    }

    //public void OnTriggerStay(Collider other)
    //{
    //    if (other.tag == "Player" && rob1_Attack.throwFlag)
    //        sensing = true;
    //}

    //public void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Player")
    //        sensing = false;
    //}
}
