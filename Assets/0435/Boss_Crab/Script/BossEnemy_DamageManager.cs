using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy_DamageManager : Enemy_HpData
{
    [Header("�{�X�̑S��HP"), SerializeField]
    private BossEnemy_HPManager BossHP_main;

    [Header("�{�̂ւ̃_���[�W�{��"), SerializeField]
    private float damageValue_Main;

    [Header("���ʂ̃_���[�W�{��"), SerializeField]
    private float damageValue_Cell;

    [Header("���ʔj�󎞂̒ǉ��_���[�W"), SerializeField]
    private float BreakDamage;
    private bool dieFlag;
    [SerializeField] private GameObject breakEffect;

    [Header("���ʔj���̖{�̂ւ̃_���[�W�{��"), SerializeField]
    private float damageValue_breaked;

    [Header("�Q�Ƃ�����΁A�q�b�g������"), SerializeField]
    private GameObject criticalEffect;

    private void Start()
    {
        dieFlag = true;
    }

    public override void Damage(float damage)
    {
        //���S���Ă��Ȃ�
        if (dieFlag)
        {
            //�Z���Ƀ_���[�W
            hp_ -= damage * damageValue_Cell;

            //���S�����ꍇ�͒ǉ��_���[�W�A�{���X�V�Aonce��false��
            if (CellBreakJudge())
            {
                //�G�t�F�N�g����
                if (breakEffect)
                {
                    GameObject go = Instantiate(breakEffect);
                    go.transform.position = this.transform.position;
                }

                BossHP_main.BaseDamage(BreakDamage);
                damageValue_Main = damageValue_breaked;
                dieFlag = false;
            }
        }

        //�N���e�B�J���G�t�F�N�g�����i�Q�Ƃ�����΁j
        if (criticalEffect)
        {
            GameObject go = Instantiate(criticalEffect);
            go.transform.position = this.transform.position;
        }

        BossHP_main.BaseDamage(damage * damageValue_Main);
    }

    public bool CellBreakJudge()
    {
        return JudgeDie();
    }


}
