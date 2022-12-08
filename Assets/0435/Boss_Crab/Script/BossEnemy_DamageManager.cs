using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy_DamageManager : Enemy_HpData
{
    [Header("ボスの全体HP"), SerializeField]
    private BossEnemy_HPManager BossHP_main;

    [Header("本体へのダメージ倍率"), SerializeField]
    private float damageValue_Main;

    [Header("部位のダメージ倍率"), SerializeField]
    private float damageValue_Cell;

    [Header("部位破壊時の追加ダメージ"), SerializeField]
    private float BreakDamage;
    private bool dieFlag;

    [Header("部位破壊後の本体へのダメージ倍率"), SerializeField]
    private float damageValue_breaked;

    private void Start()
    {
        dieFlag = true;
    }

    public override void Damage(float damage)
    {
        //死亡していない
        if (dieFlag)
        {
            //セルにダメージ
            hp_ -= damage * damageValue_Cell;

            //死亡した場合は追加ダメージ、倍率更新、onceをfalseに
            if (CellBreakJudge())
            {
                BossHP_main.BaseDamage(BreakDamage);
                damageValue_Main = damageValue_breaked;
                dieFlag = false;
            }
        }
        BossHP_main.BaseDamage(damage * damageValue_Main);
    }

    public bool CellBreakJudge()
    {
        return JudgeDie();
    }


}
