using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter_Sensing : MonoBehaviour
{
    [SerializeField] Fighter_Status fighter_Status;
    [SerializeField] Fighter_AnimManeger fighter_Animtor;

    [SerializeField] GameObject player;

    [SerializeField] ParticleSystem attack_Hadou;
    [SerializeField] GameObject attack_Syoryu_Alia;
    [SerializeField] GameObject attack_Tatumaki_Alia;

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

    float attackCnt = 0;

    [SerializeField]
    float attackIntarval = 8.0f;

    [SerializeField]
    int[] selectAttackDis;
    private void Start()
    {
        attack_Syoryu_Alia.SetActive(false);
        attack_Tatumaki_Alia.SetActive(false);
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (fighter_Status.GetState() == Fighter_Status.State.Non || fighter_Status.GetState() == Fighter_Status.State.Dead)
    //    { return; }
    //    if (other.tag != "Player")
    //    { return; }

    //    sensing = true;

    //    //�d�����Ԓ��͏��������Ȃ�
    //    if (!attackFlag) return;

    //    AttackWait();
    //}

    private void Update()
    {
        if (fighter_Status.GetState() == Fighter_Status.State.Non || fighter_Status.GetState() == Fighter_Status.State.Dead)
        { return; }

        //�d�����Ԓ��͏��������Ȃ�
        if (!attackFlag) 
        { return; };


        attackCnt += Time.deltaTime;

        if (!(attackCnt < attackIntarval)) 
        { return; }

        sensing = true;

        float dis = Vector3.Distance(this.transform.position, player.transform.position);

        if (dis > selectAttackDis[0])
        {
            AttackWait("Hadou");
        }
        else if(dis > selectAttackDis[1])
        {
            AttackWait("Tatumaki");
        }
        else 
        {
            AttackWait("Syoryu");
        }

        attackCnt = 0;
    }


    void AttackWait(string attackType)
    {
        attackFlag = false;
        fighter_Status.SetState(Fighter_Status.State.Attack);

        switch (attackType) {
            case "Hadou":
                StartCoroutine(AttackHadou());
                break;
            case "Syoryu":
                StartCoroutine(AttackSyoryu());
                break;
            case "Tatumaki":
                StartCoroutine(AttackSyoryu());
                break;
            default:
                break;
        }
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

        attack_Syoryu_Alia.SetActive(true);

        yield return new WaitForSeconds(attackActive[0]);

        attack_Syoryu_Alia.SetActive(false);

        yield return new WaitForSeconds(attackRecovery[1]);
    }

    IEnumerator AttackTatumaki()
    {
        fighter_Animtor.PlayFighterAnimTatumaki();

        yield return new WaitForSeconds(attackStartup[2]);

        attack_Tatumaki_Alia.SetActive(true);

        yield return new WaitForSeconds(attackActive[1]);

        attack_Tatumaki_Alia.SetActive(false);

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
