using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rob2_Sensing : MonoBehaviour
{
    [SerializeField] Rob2_Status rob2_Status;
    [SerializeField] Rob2_AnimManeger rob2_Animtor;

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
        if (rob2_Status.GetState() == Rob2_Status.State.Non || rob2_Status.GetState() == Rob2_Status.State.Dead)
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
        rob2_Status.SetState(Rob2_Status.State.Attack);

        rob2_Animtor.PlayRob2AnimAttack();

        yield return new WaitForSeconds(attackStartup);

        attackArea.SetActive(true);

        yield return new WaitForSeconds(attackActive);

        attackArea.SetActive(false);

        yield return new WaitForSeconds(attackRecovery);

        //ƒGƒlƒ~[‚ğStayó‘Ô‚É‚·‚é
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
