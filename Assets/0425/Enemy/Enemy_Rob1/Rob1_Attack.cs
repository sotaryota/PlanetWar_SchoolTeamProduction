using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rob1_Attack : MonoBehaviour
{
    [Header("生成する惑星のPrefabと位置")]
    [SerializeField]
    private GameObject planetPrefab;
    [SerializeField]
    private GameObject planet;
    [SerializeField]
    private GameObject handPos;

    [Header("ステータス管理スクリプト")]
    public Rob1_Status rob1Status;
    [SerializeField] Rob1_AnimManeger rob1Animator;

    [Header("惑星を投げた時のウェイト")]
    [SerializeField]
    private float waitTime;
    public bool throwFlag = true;

    float attackCount;
    [SerializeField]
    int attackInterval;

    float attackDelay;
    bool attackGenerate = true;

    [SerializeField]
    public bool sensing = false;

    // Update is called once per frame
    void Update()
    {
        if (rob1Status.GetState() == Rob1_Status.State.Non || rob1Status.GetState() == Rob1_Status.State.Dead)
        { return; }

            if (planet)
        {
            attackCount += Time.deltaTime;

            //所持している惑星の位置を腕の位置に
            planet.transform.position = handPos.transform.position;

            if (attackCount > attackInterval)
            {
                PlanetThrow();
            }
        }
        else
        {
            attackCount = 0;
        }
    }

    //--------------------------------------
    //惑星を投げる処理
    //--------------------------------------

    private void PlanetThrow()
    {
        //惑星を所持していないなら処理をしない
        if (!planet) return;


        PlanetStateMachine stateMachine = planet.GetComponent<PlanetStateMachine>();
        PlanetThrowMove throwMove = planet.GetComponent<PlanetThrowMove>();

        //惑星の状態をThrowにする
        stateMachine.SetState(PlanetStateMachine.State.Throw);

        //惑星のスピードと飛ぶ方向を決める
        Vector3 throwSpeed = new Vector3(0, 0, 10);
        Vector3 playerAngle = this.transform.rotation.eulerAngles;
        Vector3 throwAngle = new Vector3(0, playerAngle.y, 0);
        throwMove.ThrowMoveSetting(throwSpeed, throwAngle);

        //投げるボイス再生
        //this.GetComponent<PlayerSEManager>().ThrowVoice();

        //変数を空にして飛ばす
        planet = null;

        //アニメーション
        rob1Animator.PlayRob1AnimThrow();

        //硬直時間
        StartCoroutine("ThrowWait");

    }

    //--------------------------------------
    //投げ動作後の硬直時間
    //--------------------------------------

    IEnumerator ThrowWait()
    {
        throwFlag = false;

        yield return new WaitForSeconds(waitTime);

        //エネミーをStay状態にする
        rob1Status.SetState(Rob1_Status.State.Stay);

        throwFlag = true;
    }

    //--------------------------------------
    //攻撃用惑星の生成
    //--------------------------------------

    private void OnTriggerStay(Collider other)
    {
        if (rob1Status.GetState() == Rob1_Status.State.Non || rob1Status.GetState() == Rob1_Status.State.Dead)
        { return; }
        if (other.tag != "Player")
        { return; }

        sensing = true;

        //硬直時間中は処理をしない
        if (!throwFlag) return;

        //惑星を所持している場合は処理をしない
        if (planet) { return; }

        attackDelay += Time.deltaTime;

        //プレイヤをCatch状態に
        rob1Status.SetState(Rob1_Status.State.Catch);

        //アニメーション
        if (attackGenerate)
        {
            rob1Animator.PlayRob1AnimGeneration();
            attackGenerate = false;
        }
        

        if (attackDelay > 3)
        {
            //惑星の生成
            GameObject go = Instantiate(planetPrefab);
            //惑星を所持
            go.transform.position = handPos.transform.position;
            planet = go;
            //有効化
            go.SetActive(true);
            //プラネットの状態をCatch状態にする           
            go.GetComponent<PlanetStateMachine>().SetState(PlanetStateMachine.State.Catch);

            attackDelay = 0;
            attackGenerate = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            sensing = false;
    }
}
