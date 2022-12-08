using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy_HPManager : Enemy_HpData
{
    [Header("ダメージカット率"), SerializeField]
    private float damageCutValue;

    public void getDamage(float damage, float critical)
    {
        Damage((damage * critical) / damageCutValue);
    }


}
