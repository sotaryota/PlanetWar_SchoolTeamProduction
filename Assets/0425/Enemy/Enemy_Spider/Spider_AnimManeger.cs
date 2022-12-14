using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider_AnimManeger : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    //攻撃モーション
    public void PlaySpiderAnimAttack()
    {
        animator.SetTrigger("attack");
    }
    //ダメージモーション
    public void PlaySpiderAnimDamage()
    {
        animator.SetTrigger("damage");
    }
    //死亡モーション
    public void PlaySpiderAnimDie()
    {
        animator.SetTrigger("die");
    }

    //走行モーション
    public void PlaySpiderAnimSetRun(bool AnimFlag)
    {
        animator.SetBool("run", AnimFlag);
    }

}