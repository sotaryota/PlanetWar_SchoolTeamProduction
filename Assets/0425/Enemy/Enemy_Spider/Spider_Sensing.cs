using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider_Sensing : MonoBehaviour
{
    [SerializeField] Spider_Status spider_Status;
    [SerializeField] Spider_AnimManeger spider_Animtor;

    [SerializeField] GameObject attackArea;

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

    private void Start()
    {
        attackArea.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (spider_Status.GetState() == Spider_Status.State.Non || spider_Status.GetState() == Spider_Status.State.Dead)
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
        spider_Status.SetState(Spider_Status.State.Attack);

        spider_Animtor.PlaySpiderAnimAttack();

        yield return new WaitForSeconds(attackStartup);

        attackArea.SetActive(true);

        yield return new WaitForSeconds(attackActive);

        attackArea.SetActive(false);

        yield return new WaitForSeconds(attackRecovery);

        //エネミーをStay状態にする
        spider_Status.SetState(Spider_Status.State.Stay);

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
