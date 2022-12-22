using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter_Sensing : MonoBehaviour
{
    [SerializeField] Fighter_Status fighter_Status;
    [SerializeField] Fighter_AnimManeger fighter_Animtor;

    [SerializeField] GameObject attackArea;

    [SerializeField] ParticleSystem attack_Hadou;
    [SerializeField] GameObject attack_Syoryu;
    [SerializeField] GameObject attack_Tatumaki;

    public bool sensing;

    //�U���������肷��t���O
    bool attackFlag = true;

    //�U���̔���
    [SerializeField]
    float[] attackStartup;

    //�U���̎����I��
    [SerializeField]
    float[] attackActive;

    //�U���������������̃t���O
    public bool attackHit = false;

    //�U���I�����̍d��
    [SerializeField]
    float[] attackRecovery;

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

        //�d�����Ԓ��͏��������Ȃ�
        if (!attackFlag) return;

        AttackWait();
    }

    void AttackWait()
    {
        attackFlag = false;
        fighter_Status.SetState(Fighter_Status.State.Attack);


        //if()
        StartCoroutine(AttackHadou());


        //�G�l�~�[��Stay��Ԃɂ���
        fighter_Status.SetState(Fighter_Status.State.Stay);

        attackFlag = true;
        attackHit = false;
    }

    IEnumerator AttackHadou()
    {
        fighter_Animtor.PlayFighterAnimHadou();

        yield return new WaitForSeconds(attackStartup[0]);

        attack_Hadou.Play();

        yield return new WaitForSeconds(attackRecovery[0]);
    } 

    IEnumerator AttackSyoryu()
    {
        fighter_Animtor.PlayFighterAnimSyoryu();

        yield return new WaitForSeconds(attackStartup[1]);

        attack_Syoryu.SetActive(true);

        yield return new WaitForSeconds(attackActive[1]);

        attack_Syoryu.SetActive(false);

        yield return new WaitForSeconds(attackRecovery[1]);
    }

    IEnumerator AttackTatumaki()
    {
        fighter_Animtor.PlayFighterAnimTatumaki();

        yield return new WaitForSeconds(attackStartup[2]);

        attack_Tatumaki.SetActive(true);

        yield return new WaitForSeconds(attackActive[2]);

        attack_Tatumaki.SetActive(false);

        yield return new WaitForSeconds(attackRecovery[2]);
    }


    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            sensing = false;
        }
    }
}
