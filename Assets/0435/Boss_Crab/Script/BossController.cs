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
    private Vector3 lockPos;

    [Header("軸合わせ速度と合わせる時間")]
    [SerializeField] private float lookingTime;
    [SerializeField] private float lookingSpeed;

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
    private int smashDataSelect = 0;

    [Header("ブレス攻撃情報")]
    [SerializeField] private Transform breathTargetPos;
    [SerializeField] private GameObject breathPrefab;
    [SerializeField] private Transform createPosObject;
    [SerializeField] private float breathCreateWait;
    [SerializeField] private float breathEndWait;

    private string nowCoroutine; //現在のコルーチン名
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
        if (!BossDie()) {

            if (attack)
            {
                StartCoroutine("Sarch");
                if (nowCoroutine != null)
                {
                    StartCoroutine(nowCoroutine);
                    attack = false;
                }
                
            }
            else if (looking)
            {
                nowCoroutine = "Looking";
                StartCoroutine(nowCoroutine);
            }
        }
    }

    IEnumerator Sarch()
    {
        //コルーチン名を指定する。指定した後に
        int attackSelect = Random.Range(0, 6);
        attackSelect /= 5;

        //サーチエリアをその都度変更する
        CrabAttackSearch searchScript;//可変式

        for (int i = 0; i < smashData.Length; ++i)
        {
            if (searchScript = smashData[i].createPos.GetComponent<CrabAttackSearch>())
            {
                if(JudgeHitAndStart(searchScript, "Attack_Smash"))
                {
                    smashDataSelect = i;
                    yield break;
                }
            }
        }
        
        if(searchScript = breathTargetPos.GetComponent<CrabAttackSearch>())
        {
            if (searchScript.HitCheck())
            {
                if(JudgeHitAndStart(searchScript, "Attack_Breath"))
                {
                    yield break;
                }
            }
        }

        //ここまで来たら処理なし
        nowCoroutine = null;
        print("攻撃なし");
        yield break;
    }

    public bool JudgeHitAndStart(CrabAttackSearch searchScript, string coroutineName)
    {
        if (searchScript.HitCheck())
        {
            nowCoroutine = coroutineName;
            return true;
        }
        return false;
    }

    //軸合わせ
    IEnumerator Looking()
    {
        //軸合わせ無効
        looking = false;

        //指定フレームかけて敵の方向を向く
        for (float i = 0; i < lookingTime; i += Time.deltaTime)
        {
            //回転ベクトルを計算
            Vector3 myPos = this.transform.position;
            Vector3 pPos = player.transform.position;
            pPos.y = myPos.y;

            //回転量を計算
            Vector3 lookVec = pPos - myPos;
            Quaternion quaternion = Quaternion.LookRotation(lookVec);

            //回転速度を計算してから回転
            float lookingNowCount = Time.deltaTime * lookingSpeed;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, quaternion, lookingNowCount);
            yield return null;
        }

        //攻撃開始
        attack = true;
    }

    IEnumerator Attack_Smash()
    {
        //アニメーションを殴りにして待機
        bossAnimator.SetTrigger(smashData[smashDataSelect].animTriggerName);
        yield return new WaitForSeconds(smashData[smashDataSelect].startWait);

        //攻撃を生成して待機
        GameObject go = Instantiate(smashAttack);
        go.transform.position = smashData[smashDataSelect].createPos.position;
        yield return new WaitForSeconds(smashData[smashDataSelect].endWait);

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


