using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rob2_Sensing : MonoBehaviour
{
    [SerializeField] Rob2_Status rob2_Status;
    [SerializeField] Rob2_AnimManeger rob2_Animtor;

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
        if (rob2_Status.GetState() == Rob2_Status.State.Non || rob2_Status.GetState() == Rob2_Status.State.Dead)
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
        rob2_Status.SetState(Rob2_Status.State.Attack);

        rob2_Animtor.PlayRob2AnimAttack();

        yield return new WaitForSeconds(attackStartup);

        attackArea.SetActive(true);

        yield return new WaitForSeconds(attackActive);

        attackArea.SetActive(false);

        yield return new WaitForSeconds(attackRecovery);

        //エネミーをStay状態にする
        rob2_Status.SetState(Rob2_Status.State.Stay);

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
