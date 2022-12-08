using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRob_Sensing : MonoBehaviour
{
    [SerializeField] BoxRob_Status boxRob_Status;
    [SerializeField] BoxRob_AnimManeger boxRob_Animtor;

    [SerializeField] GameObject attack_Fire;


    public bool sensing;

    //攻撃中か判定するフラグ
    bool attackFlag = true;

    //攻撃の発生
    [SerializeField]
    float attackStartup = 1.5f;

    //攻撃の持続終了
    [SerializeField]
    float attackActive = 1.5f;

    //攻撃が当たったかのフラグ
    public bool attackHit = false;

    //攻撃終了時の硬直
    [SerializeField]
    float attackRecovery = 2.0f;

    private void OnTriggerStay(Collider other)
    {
        if (boxRob_Status.GetState() == BoxRob_Status.State.Non || boxRob_Status.GetState() == BoxRob_Status.State.Dead)
        { return; }
        if (other.tag != "Player")
        { return; }

        sensing = true;

        //硬直時間中は処理をしない
        if (!attackFlag) return;

        StartCoroutine(AttackWait());
    }

    IEnumerator AttackWait()
    {
        attackFlag = false;

        //rob2をAttack状態に
        boxRob_Status.SetState(BoxRob_Status.State.Attack);

        boxRob_Animtor.PlayBoxRobAnimAttack(true);

        yield return new WaitForSeconds(attackStartup);

      

        yield return new WaitForSeconds(attackActive);



        yield return new WaitForSeconds(attackRecovery);

        //エネミーをStay状態にする
        boxRob_Status.SetState(BoxRob_Status.State.Stay);

        attackFlag = true;
        attackHit = false;
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            sensing = false;
        }
    }
}
