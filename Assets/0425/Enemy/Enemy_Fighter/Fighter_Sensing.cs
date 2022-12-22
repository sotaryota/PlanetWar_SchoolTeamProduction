using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter_Sensing : MonoBehaviour
{
    [SerializeField] Fighter_Status fighter_Status;
    [SerializeField] Fighter_AnimManeger fighter_Animtor;

    [SerializeField] GameObject attackArea;

    public bool sensing;

    //UŒ‚’†‚©”»’è‚·‚éƒtƒ‰ƒO
    bool attackFlag = true;

    //UŒ‚‚Ì”­¶
    [SerializeField]
    float attackStartup = 1.5f;

    //UŒ‚‚Ì‘±I—¹
    [SerializeField]
    float attackActive = 1.5f;

    //UŒ‚‚ª“–‚½‚Á‚½‚©‚Ìƒtƒ‰ƒO
    public bool attackHit = false;

    //UŒ‚I—¹‚Ìd’¼
    [SerializeField]
    float attackRecovery = 2.0f;

    private void Start()
    {
        attackArea.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (fighter_Status.GetState() == Fighter_Status.State.Non || fighter_Status.GetState() == Fighter_Status.State.Dead)
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

        fighter_Status.SetState(Fighter_Status.State.Attack);

        //fighter_Animtor.PlayFighterAnimAttack();

        yield return new WaitForSeconds(attackStartup);

        attackArea.SetActive(true);

        yield return new WaitForSeconds(attackActive);

        attackArea.SetActive(false);

        yield return new WaitForSeconds(attackRecovery);

        //ƒGƒlƒ~[‚ğStayó‘Ô‚É‚·‚é
        fighter_Status.SetState(Fighter_Status.State.Stay);

        attackFlag = true;
        attackHit = false;
    }

    void AttackHadou()
    {
        fighter_Animtor.PlayFighterAnimHadou();

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            sensing = false;
        }
    }
}
