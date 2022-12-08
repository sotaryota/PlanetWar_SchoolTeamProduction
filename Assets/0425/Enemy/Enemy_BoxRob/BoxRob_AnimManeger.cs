using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRob_AnimManeger : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    //投げモーション
    public void PlayBoxRobAnimAttack(bool attackFlag)
    {
        animator.SetBool("attack",attackFlag);
    }
    ////ダメージモーション
    //public void PlayBoxRobAnimDamage()
    //{
    //    animator.SetTrigger("damage");
    //}
    //死亡モーション
    public void PlayBoxRobAnimDie()
    {
        animator.SetTrigger("die");
    }
    ////惑星の生成モーション
    //public void PlayRob1AnimGeneration()
    //{
    //    animator.SetTrigger("generation");
    //}


    //走行モーション
    public void PlayBoxRobAnimSetRun(bool AnimFlag)
    {
        animator.SetBool("run", AnimFlag);
    }

}
