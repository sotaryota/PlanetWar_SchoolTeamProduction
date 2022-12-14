using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider_Sensing : MonoBehaviour
{
    [SerializeField] Spider_Status spider_Status;
    [SerializeField] Spider_AnimManeger spider_Animtor;

    [SerializeField] GameObject attackArea;

    public bool sensing;

    //�U���������肷��t���O
    bool attackFlag = true;

    //�U���̔���
    [SerializeField]
    float attackStartup = 1.5f;

    //�U���̎����I��
    [SerializeField]
    float attackActive = 1.5f;

    //�U���������������̃t���O
    public bool attackHit = false;

    //�U���I�����̍d��
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

        //�d�����Ԓ��͏��������Ȃ�
        if (!attackFlag) return;

        StartCoroutine(AttackWait());
    }

    IEnumerator AttackWait()
    {
        attackFlag = false;

        //rob2��Attack��Ԃ�
        spider_Status.SetState(Spider_Status.State.Attack);

        spider_Animtor.PlaySpiderAnimAttack();

        yield return new WaitForSeconds(attackStartup);

        attackArea.SetActive(true);

        yield return new WaitForSeconds(attackActive);

        attackArea.SetActive(false);

        yield return new WaitForSeconds(attackRecovery);

        //�G�l�~�[��Stay��Ԃɂ���
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
