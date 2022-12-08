using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy_HPManager : Enemy_HpData
{
    public void BaseDamage(float damage)
    {
        Damage(damage);
    }
}
