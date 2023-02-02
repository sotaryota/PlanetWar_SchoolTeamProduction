using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_HpData : MonoBehaviour
{
    [SerializeField]
    [Range(0, 300)]
    protected float hp_;         //HP

    public float GetHp()
    {
        return this.hp_;
    }

    public virtual void Damage(float damage)
    {
        this.hp_ -= damage;
    }

    public bool JudgeDie()
    {
        if(this.hp_ <= 0)
        {
            return true;
        }
        return false;
    }
}
