using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRob_Sensing : MonoBehaviour
{
    [SerializeField] BoxRob_Status boxRob_Status;
    [SerializeField] BoxRob_AnimManeger boxRob_Animtor;

    [SerializeField] ParticleSystem attack_Fire;

  

    public bool sensing;

    //UŒ‚’†‚©”»’è‚·‚éƒtƒ‰ƒO
    bool attackFlag = true;

    //UŒ‚‚Ì”­¶
    [SerializeField]
    float attackStartup = 1.5f;

    //UŒ‚‚Ì‘±I—¹
    [SerializeField]
    float attackActive = 1.5f;

    //UŒ‚I—¹‚Ìd’¼
    [SerializeField]
    float attackRecovery = 2.0f;

    private void Start()
    {
        sensing = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (boxRob_Status.GetState() == BoxRob_Status.State.Non || boxRob_Status.GetState() == BoxRob_Status.State.Dead)
        { return; }
        if (other.tag != "Player")
        { return; }

        sensing = true;

        //d’¼ŠÔ’†‚Íˆ—‚ğ‚µ‚È‚¢
        if (!attackFlag) return;

        StartCoroutine(AttackWait());
    }

    IEnumerator AttackWait()
    {

        attackFlag = false;

        //rob2‚ğAttackó‘Ô‚É
        boxRob_Status.SetState(BoxRob_Status.State.Attack);

        boxRob_Animtor.PlayBoxRobAnimAttack(true);

        yield return new WaitForSeconds(attackStartup);

        attack_Fire.Play();

        yield return new WaitForSeconds(attackActive);

        attack_Fire.Stop();

        boxRob_Animtor.PlayBoxRobAnimAttack(false);

        yield return new WaitForSeconds(attackRecovery);

        //ƒGƒlƒ~[‚ğStayó‘Ô‚É‚·‚é
        boxRob_Status.SetState(BoxRob_Status.State.Stay);

        attackFlag = true;
        sensing = false;
    }
}
