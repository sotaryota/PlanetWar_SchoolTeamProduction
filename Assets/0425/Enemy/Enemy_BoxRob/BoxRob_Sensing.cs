using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRob_Sensing : MonoBehaviour
{
    [SerializeField] BoxRob_Status boxRob_Status;
    [SerializeField] BoxRob_AnimManeger boxRob_Animtor;

    [SerializeField] GameObject attack_Fire;


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

    private void OnTriggerStay(Collider other)
    {
        if (boxRob_Status.GetState() == BoxRob_Status.State.Non || boxRob_Status.GetState() == BoxRob_Status.State.Dead)
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
        boxRob_Status.SetState(BoxRob_Status.State.Attack);

        boxRob_Animtor.PlayBoxRobAnimAttack(true);

        yield return new WaitForSeconds(attackStartup);

      

        yield return new WaitForSeconds(attackActive);



        yield return new WaitForSeconds(attackRecovery);

        //�G�l�~�[��Stay��Ԃɂ���
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