using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("ボス自身のHPを参照"), SerializeField]
    private Enemy_HpData HPData;

    [Header("ボスのアニメーターを参照"), SerializeField]
    private Animator bossAnimator;

    [Header("プレイヤー参照"), SerializeField]
    private GameObject player;

    private string nowCoroutine;


    [System.Serializable]
    public class SmashData
    {
        [Tooltip("生成箇所")]
        public Transform createPos;

        [Tooltip("モーション名")]
        public string animTriggerName;

        [Header("攻撃の開始時間と終了時間")]
        public float startWait;
        public float endWait;
    }
    [Header("殴り攻撃情報")]
    [SerializeField] private GameObject smashAttack;
    [SerializeField] private SmashData[] smashData = new SmashData[1];

    [Header("ブレス攻撃情報")]
    [SerializeField] private Transform breathTargetPos;
    [SerializeField] private GameObject breathPrefab;
    [SerializeField] private Transform createPosObject;
    [SerializeField] private float breathCreateWait;
    [SerializeField] private float breathEndWait;


    private bool attack;//攻撃
    private bool looking;//軸合わせ
    private bool dieAnimPlayed;//死亡アニメーション

    // Start is called before the first frame update
    void Start()
    {
        looking = true;
        attack = false;
        dieAnimPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!BossDie())
        {
            //アタックフラグがtrueなら攻撃、falseなら軸合わせ
            if (attack)
            {
                int sttackSelect = Random.Range(0, 2);
                switch (sttackSelect)
                {
                    case 0:
                        nowCoroutine = "Attack_Smash";
                        break;

                    case 1:
                        nowCoroutine = "Attack_Breath";
                        break;

                    default:
                        print("攻撃なし");
                        break;
                }
                StartCoroutine(nowCoroutine);
                attack = false;
            }
            else if(looking)
            {
                //仮
                Vector3 lookPos = player.transform.position;
                lookPos.y = this.transform.position.y;
                this.transform.LookAt(lookPos);

                attack = true;
                looking = false;
            }
        }
    }

    IEnumerator Attack_Smash()
    {
        //殴りをランダム取得
        int smashNum = Random.Range(0, smashData.Length);

        //アニメーションを殴りにして待機
        bossAnimator.SetTrigger(smashData[smashNum].animTriggerName);
        yield return new WaitForSeconds(smashData[smashNum].startWait);

        //攻撃を生成して待機
        GameObject go = Instantiate(smashAttack);
        go.transform.position = smashData[smashNum].createPos.position;
        yield return new WaitForSeconds(smashData[smashNum].endWait);

        //軸合わせ有効化
        looking = true;

        //攻撃終了
        nowCoroutine = null;
    }

    IEnumerator Attack_Breath()
    {
        //アニメーションをブレス攻撃にして待機
        bossAnimator.SetTrigger("Breath");
        yield return new WaitForSeconds(breathCreateWait);

        //攻撃を生成して待機
        GameObject go = Instantiate(breathPrefab);
        go.transform.position = createPosObject.position;
        go.GetComponent<CrabBreathController>().SetTarget(breathTargetPos.position);
        yield return new WaitForSeconds(breathEndWait);

        //軸合わせ有効化
        looking = true;

        //攻撃終了
        nowCoroutine = null;
    }

    public bool BossDie()
    {
        if (HPData.JudgeDie() && !dieAnimPlayed)
        {
            //現在のコルーチンを停止
            if (nowCoroutine != null)
            {
                StopCoroutine(nowCoroutine);
            }

            //死亡アニメーション
            bossAnimator.SetTrigger("Die");
            dieAnimPlayed = true;
        }

        //死亡しているかを返す
        return HPData.JudgeDie();
    }

}
