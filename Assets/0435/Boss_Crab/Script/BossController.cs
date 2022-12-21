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

    [Header("登場時の叫ぶ行動の情報")]
    [SerializeField] private float shoutStartWait;
    [SerializeField] private float shoutEndWait;

    [System.Serializable]
    public class SmashData
    {
        [Header("生成箇所と警告位置")]
        public Transform createPos;
        public GameObject warningObject;

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
    [SerializeField] private Transform breathCreatePos;
    [SerializeField] private GameObject breathWarningArea;
    [SerializeField] private float breathCreateWait;
    [SerializeField] private float breathEndWait;
    [Tooltip("攻撃確率"), Range(0.0f, 100.0f)]
    [SerializeField] private float breathProbability;

    [Header("頭突き攻撃情報")]
    [SerializeField] private Transform headAttackPos;
    [SerializeField] private GameObject headAttackPrefab;
    [SerializeField] private GameObject HeadWarningArea;
    [SerializeField] private float headCreateWait;
    [SerializeField] private float headEndWait;
    [Tooltip("攻撃確率"), Range(0.0f, 100.0f)]
    [SerializeField] private float headProbability;

    [Header("死亡時の行動")]
    [SerializeField] private float dieFallSpeed;

    private string nowCoroutine; //現在のコルーチン名
    private bool attack;//攻撃
    private bool looking;//軸合わせ
    private bool dieAnimPlayed;//死亡アニメーション

    // Start is called before the first frame update
    void Start()
    {
        //フラグの初期設定
        looking = false;
        attack = false;
        dieAnimPlayed = false;

        //警告範囲無効化
        breathWarningArea.SetActive(false);

        //叫ぶ
        StartCoroutine("Shout");
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

    IEnumerator Shout()
    {
        //叫び始めまで待機
        yield return new WaitForSeconds(shoutStartWait);

        //アニメーション変更
        bossAnimator.SetTrigger("Shout");

        //叫び終わりまで待機
        yield return new WaitForSeconds(shoutEndWait);

        //軸合わせ開始
        looking = true;
    }

    IEnumerator Sarch()
    {
        //コルーチン名を指定する。
        int attackSelect = Random.Range(0, 6);
        attackSelect /= 5;

        //確率で動作(ブレス)
        if(Random.Range(0.0f, 100.0f) <= breathProbability)
        {
            nowCoroutine = "Attack_Breath";
            yield break;
        }

        //確率で動作(頭突き)
        if(Random.Range(0.0f, 100.0f) <= headProbability)
        {
            nowCoroutine = "Attack_Head";
            yield break;
        }

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
        smashData[smashDataSelect].warningObject.SetActive(true);
        yield return new WaitForSeconds(smashData[smashDataSelect].startWait);

        //攻撃を生成して待機
        GameObject go = Instantiate(smashAttack);
        go.transform.position = smashData[smashDataSelect].createPos.position;
        smashData[smashDataSelect].warningObject.SetActive(false);
        yield return new WaitForSeconds(smashData[smashDataSelect].endWait);

        //軸合わせ有効化
        looking = true;

        //攻撃終了
        nowCoroutine = null;
    }

    IEnumerator Attack_Breath()
    {
        //アニメーションをブレス攻撃
        bossAnimator.SetTrigger("Breath");

        //警告範囲有効化
        breathWarningArea.SetActive(true);

        //生成まで待機
        yield return new WaitForSeconds(breathCreateWait);

        //攻撃を生成して待機
        GameObject go = Instantiate(breathPrefab);
        go.transform.position = breathCreatePos.position;
        go.GetComponent<CrabBreathController>().SetTarget(breathTargetPos.position);

        //終了まで待機
        for(float time = 0; time < breathEndWait; time += Time.deltaTime)
        {
            //警告範囲無効化
            if (go == null)
            {
                breathWarningArea.SetActive(false);
            }

            yield return null;
        }

        //軸合わせ有効化
        looking = true;

        //攻撃終了
        nowCoroutine = null;
    }

    IEnumerator Attack_Head()
    {
        //アニメーションを頭突き攻撃にして待機
        bossAnimator.SetTrigger("Attack_Head");
        HeadWarningArea.SetActive(true);
        yield return new WaitForSeconds(headCreateWait);

        //攻撃を生成して待機
        GameObject go = Instantiate(headAttackPrefab);
        go.transform.position = headAttackPos.position;
        go.transform.rotation = headAttackPos.rotation;
        HeadWarningArea.SetActive(false);
        yield return new WaitForSeconds(headEndWait);

        //軸合わせ有効化
        looking = true;

        //攻撃終了
        nowCoroutine = null;
    }
    public bool BossDie()
    {
        if (HPData.JudgeDie())
        {
            if (!dieAnimPlayed)
            {
                //現在のコルーチンを停止
                if (nowCoroutine != null)
                {
                    StopCoroutine(nowCoroutine);
                }

                //いろいろ無効化
                breathWarningArea.SetActive(false);

                //死亡アニメーション
                bossAnimator.SetTrigger("Die");
                dieAnimPlayed = true;
            }

            //下に沈む
            this.transform.Translate(new Vector3(0, -dieFallSpeed * Time.deltaTime, 0));
        }



        //死亡しているかを返す
        return HPData.JudgeDie();
    }
}


