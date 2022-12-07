using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rob2_AnimManeger : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    //攻撃モーション
    public void PlayRob2AnimAttack()
    {
        animator.SetTrigger("attack");
    }
    //ダメージモーション
    public void PlayRob2AnimDamage()
    {
        animator.SetTrigger("damage");
    }
    //死亡モーション
    public void PlayRob2AnimDie()
    {
        animator.SetTrigger("die");
    }

    //走行モーション
    public void PlayRob2AnimSetRun(bool AnimFlag)
    {
        animator.SetBool("run", AnimFlag);
    }

}
