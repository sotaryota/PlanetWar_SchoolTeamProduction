using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rob1_AnimManeger : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    //投げモーション
    public void PlayRob1AnimThrow()
    {
        animator.SetTrigger("throw");
    }
    //ダメージモーション
    public void PlayRob1AnimDamage()
    {
        animator.SetTrigger("damage");
    }
    //死亡モーション
    public void PlayRob1AnimDie()
    {
        animator.SetTrigger("die");
    }
   //惑星の生成モーション
   public void PlayRob1AnimGeneration()
    {
        animator.SetTrigger("generation");
    }


    //走行モーション
    public void PlayRob1AnimSetRun(bool AnimFlag)
    {
        animator.SetBool("run", AnimFlag);
    }
 
}
