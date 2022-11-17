using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_HpData : MonoBehaviour
{
    [SerializeField]
    [Range(0, 100)]
    private float hp_;         //HP

    public bool die = false;

    public float GetHp()
    {
        return this.hp_;
    }

    public void Damage(float damage)
    {
        this.hp_ -= damage;
    }

    public void JudgeDie()
    {
        if(this.hp_ < 0)
        {
            die = true;
        }
    }
}
