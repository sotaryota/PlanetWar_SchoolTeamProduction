using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRob_Move : MonoBehaviour
{
    [Header("スクリプト")]
    [SerializeField] BoxRob_Status boxRob_Status;
    [SerializeField] BoxRob_Sensing boxRob_Sensing;

    [SerializeField] BoxRob_AnimManeger boxRob_Animator;

    GameObject player;

    bool die = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        //エネミーが存在しないor死んでいるなら処理をしない
        if (boxRob_Status.GetState() == BoxRob_Status.State.Non || boxRob_Status.GetState() == BoxRob_Status.State.Dead)
        {
            if (die == false)
            {
                boxRob_Animator.PlayBoxRobAnimDie();
                die = true;
            }
            return;
        }

        PlayerLook();

        if (boxRob_Sensing.sensing == false)
        {
            MoveOrStop();
        }
        else
        {
            boxRob_Animator.PlayBoxRobAnimSetRun(false);
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
        if (boxRob_Status.GetState() != BoxRob_Status.State.Attack)
        {
            //プレイヤをMove状態に
            boxRob_Status.SetState(BoxRob_Status.State.Move);
        }

        //移動処理
        Vector3 movePos = player.transform.position;
        movePos.y = this.transform.position.y; ;
        transform.position = Vector3.MoveTowards(transform.position, movePos, boxRob_Status.GetSpeed() * Time.deltaTime);

        //アニメーション
        boxRob_Animator.PlayBoxRobAnimSetRun(true);
    }
}