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
    [SerializeField] GameObject attack_Hadou_ArmPos;

    public bool sensing;

    //攻撃中か判定するフラグ
    bool attackFlag = true;

    //攻撃の発生
    [SerializeField]
    float[] attackStartup = new float[3];

    //攻撃の持続終了
    [SerializeField]
    float[] attackActive = new float[2];

    //攻撃が当たったかのフラグ
    public bool attackHit = false;

    //攻撃終了時の硬直
    [SerializeField]
    float[] attackRecovery = new float[3];

    float attackCnt = 0;

    [SerializeField]
    float attackIntarval = 0;

    [SerializeField]
    float[] selectAttackDis;
    private void Start()
    {
        attack_Syoryu_Alia.SetActive(false);
        attack_Tatumaki_Alia.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (fighter_Status.GetState() == Fighter_Status.State.Non || fighter_Status.GetState() == Fighter_Status.State.Dead)
        { return; }

        //硬直時間中は処理をしない
        if (!attackFlag) 
        { return; };


        attackCnt += Time.deltaTime;

        if (attackCnt < attackIntarval) 
        { return; }

        sensing = true;

        float dis = Vector3.Distance(this.transform.position, player.transform.position);

        if (dis >= selectAttackDis[0])
        {
            AttackWait("Hadou");
        }
        else if(dis >= selectAttackDis[1])
        {
            AttackWait("Tatumaki");
        }
        else 
        {
            AttackWait("Syoryu");
        }

        
    }


    void AttackWait(string attackType)
    {
        //カウント初期化＆硬直
        attackFlag = false;
        attackCnt = 0;

        fighter_Status.SetState(Fighter_Status.State.Attack);

        switch (attackType) {
            case "Hadou":
                StartCoroutine(AttackHadou());
                break;
            case "Syoryu":
                StartCoroutine(AttackSyoryu());
                break;
            case "Tatumaki":
                StartCoroutine(AttackTatumaki());
                break;
            default:
                break;
        }

        //エネミーをStay状態にする
        fighter_Status.SetState(Fighter_Status.State.Stay);

        attackHit = false;
    }

    IEnumerator AttackHadou()
    {
        fighter_Animtor.PlayFighterAnimHadou();

        yield return new WaitForSeconds(attackStartup[0]);

        GameObject hadou = Instantiate(attack_Hadou.gameObject);
        hadou.transform.position = attack_Hadou_ArmPos.transform.position;

        Vector3 targetPos = player.transform.position;
        targetPos.y = hadou.transform.position.y;
        hadou.transform.LookAt(targetPos);

        Fighter_HadouMove hadoMove = hadou.GetComponent<Fighter_HadouMove>();
        hadoMove.fighter_Status = this.GetComponent<Fighter_Status>();


        yield return new WaitForSeconds(attackRecovery[0]);

        attackFlag = true;
        sensing = false;
    } 

    IEnumerator AttackSyoryu()
    {
        fighter_Animtor.PlayFighterAnimSyoryu();

        yield return new WaitForSeconds(attackStartup[1]);

        attack_Syoryu_Alia.SetActive(true);

        yield return new WaitForSeconds(attackActive[0]);

        attack_Syoryu_Alia.SetActive(false);

        yield return new WaitForSeconds(attackRecovery[1]);

        attackFlag = true;
        sensing = false;
    }

    IEnumerator AttackTatumaki()
    {
        fighter_Animtor.PlayFighterAnimTatumaki();

        yield return new WaitForSeconds(attackStartup[2]);

        attack_Tatumaki_Alia.SetActive(true);

        yield return new WaitForSeconds(attackActive[1]);

        attack_Tatumaki_Alia.SetActive(false);

        yield return new WaitForSeconds(attackRecovery[2]);

        attackFlag = true;
        sensing = false;
    }


    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            sensing = false;
        }
    }
}
