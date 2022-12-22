using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter_Move : MonoBehaviour
{
    [Header("スクリプト")]
    [SerializeField] Fighter_Status fighter_Status;
    [SerializeField] Fighter_Sensing fighter_Sensing;

    [SerializeField] Fighter_AnimManeger fighterAnimator;

    GameObject player;

    bool die = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        //エネミーが存在しないor死んでいるなら処理をしない
        if (fighter_Status.GetState() == Fighter_Status.State.Non || fighter_Status.GetState() == Fighter_Status.State.Dead)
        {
            if (die == false)
            {
                fighterAnimator.PlayFighterAnimDie();
                die = true;
            }
            return;
        }

        PlayerLook();

        if (fighter_Sensing.sensing == false)
        {
            MoveOrStop();
        }
        else
        {
            fighterAnimator.PlayFighterAnimSetRun(false);
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
        if (fighter_Status.GetState() == Fighter_Status.State.Attack)
        { return; }

        //Catch状態でないなら
        if (fighter_Status.GetState() != Fighter_Status.State.Attack)
        {
            //プレイヤをMove状態に
            fighter_Status.SetState(Fighter_Status.State.Move);
        }

        //移動処理
        Vector3 movePos = player.transform.position;
        movePos.y = this.transform.position.y; ;
        transform.position = Vector3.MoveTowards(transform.position, movePos, fighter_Status.GetSpeed() * Time.deltaTime);

        //アニメーション
        fighterAnimator.PlayFighterAnimSetRun(true);
    }
}