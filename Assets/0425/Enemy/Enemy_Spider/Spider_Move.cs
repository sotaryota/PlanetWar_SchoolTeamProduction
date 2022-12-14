using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider_Move : MonoBehaviour
{
    [Header("スクリプト")]
    [SerializeField] Spider_Status spider_Status;
    [SerializeField] Spider_Sensing spider_Sensing;

    [SerializeField] Spider_AnimManeger spider_Animator;

    GameObject player;

    bool die = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        //エネミーが存在しないor死んでいるなら処理をしない
        if (spider_Status.GetState() == Spider_Status.State.Non || spider_Status.GetState() == Spider_Status.State.Dead)
        {
            if (die == false)
            {
                spider_Animator.PlaySpiderAnimDie();
                die = true;
            }
            return;
        }

        PlayerLook();

        if (spider_Sensing.sensing == false)
        {
            MoveOrStop();
        }
        else
        {
            spider_Animator.PlaySpiderAnimSetRun(false);
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
        if (spider_Status.GetState() == Spider_Status.State.Attack)
        { return; }

        //Catch状態でないなら
        if (spider_Status.GetState() != Spider_Status.State.Attack)
        {
            //SpiderをMove状態に
            spider_Status.SetState(Spider_Status.State.Move);
        }

        //移動処理
        Vector3 movePos = player.transform.position;
        movePos.y = this.transform.position.y; ;
        transform.position = Vector3.MoveTowards(transform.position, movePos, spider_Status.GetSpeed() * Time.deltaTime);

        //アニメーション
        spider_Animator.PlaySpiderAnimSetRun(true);
    }
}