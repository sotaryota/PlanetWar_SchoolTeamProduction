using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rob2_Move : MonoBehaviour
{
    [Header("スクリプト")]
    [SerializeField] Rob2_Status rob2_Status;
    [SerializeField] Rob2_Sensing rob2_Sensing;

    [SerializeField] Rob2_AnimManeger rob2Animator;

    GameObject player;

    bool die = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        //エネミーが存在しないor死んでいるなら処理をしない
        if (rob2_Status.GetState() == Rob2_Status.State.Non || rob2_Status.GetState() == Rob2_Status.State.Dead)
        {
            if (die == false)
            {
                rob2Animator.PlayRob2AnimDie();
                die = true;
            }
            return;
        }

        PlayerLook();

        if (rob2_Sensing.sensing == false)
        {   
            MoveOrStop();
        }
        else
        {
            rob2Animator.PlayRob2AnimSetRun(false);
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
        if (rob2_Status.GetState() == Rob2_Status.State.Attack)
        { return; }

            //Catch状態でないなら
            if (rob2_Status.GetState() != Rob2_Status.State.Attack)
        {
            //プレイヤをMove状態に
            rob2_Status.SetState(Rob2_Status.State.Move);
        }

        //移動処理
        Vector3 movePos = player.transform.position;
        movePos.y = this.transform.position.y; ;
        transform.position = Vector3.MoveTowards(transform.position, movePos, rob2_Status.GetSpeed() * Time.deltaTime);

        //アニメーション
        rob2Animator.PlayRob2AnimSetRun(true);
    }
}