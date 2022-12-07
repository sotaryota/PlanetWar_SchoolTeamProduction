using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rob2_Sensing : MonoBehaviour
{
    [SerializeField] Rob2_Status rob2_Status;
    [SerializeField] Rob2_AnimManeger rob2_Animtor;

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
        if (rob2_Status.GetState() == Rob2_Status.State.Non || rob2_Status.GetState() == Rob2_Status.State.Dead)
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
        rob2_Status.SetState(Rob2_Status.State.Attack);

        rob2_Animtor.PlayRob2AnimAttack();

        yield return new WaitForSeconds(attackStartup);

        attackArea.SetActive(true);

        yield return new WaitForSeconds(attackActive);

        attackArea.SetActive(false);

        yield return new WaitForSeconds(attackRecovery);

        //�G�l�~�[��Stay��Ԃɂ���
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
