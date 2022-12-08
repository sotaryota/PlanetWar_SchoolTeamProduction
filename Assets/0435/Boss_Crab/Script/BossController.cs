using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("ボス自身のHPを参照"), SerializeField]
    private Enemy_HpData HPData;

    [Header("ボスのアニメーターを参照"), SerializeField]
    private Animator bossAnimator;


    [Header("ブレス攻撃情報")]
    [SerializeField] private GameObject breathPrefab;
    [SerializeField] private Transform createPosObject;
    [SerializeField] private float breathCreateWait;
    [SerializeField] private float breathEndWait;


    private bool attacked;
    private bool dieAnimPlayed;

    // Start is called before the first frame update
    void Start()
    {
        attacked = false;
        dieAnimPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!BossDie())
        {
            if (!attacked)
            {
                StartCoroutine("Attack_Breath");
                attacked = true;
            }
        }
    }

    IEnumerator Attack_Breath()
    {
        //アニメーションをブレス攻撃にして待機
        bossAnimator.SetTrigger("Breath");
        yield return new WaitForSeconds(breathCreateWait);

        //攻撃を生成して待機
        GameObject go = Instantiate(breathPrefab);
        go.transform.position = createPosObject.position;
        yield return new WaitForSeconds(breathEndWait);

        //攻撃終了
        attacked = false;
    }

    public bool BossDie()
    {
        if (HPData.JudgeDie() && !dieAnimPlayed)
        {
            bossAnimator.SetTrigger("Die");
            dieAnimPlayed = true;
        }

        //死亡しているかを返す
        return HPData.JudgeDie();
    }

}
