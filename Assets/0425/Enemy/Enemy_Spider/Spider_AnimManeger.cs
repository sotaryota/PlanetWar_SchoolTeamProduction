using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider_AnimManeger : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    //�U�����[�V����
    public void PlaySpiderAnimAttack()
    {
        animator.SetTrigger("attack");
    }
    //�_���[�W���[�V����
    public void PlaySpiderAnimDamage()
    {
        animator.SetTrigger("damage");
    }
    //���S���[�V����
    public void PlaySpiderAnimDie()
    {
        animator.SetTrigger("die");
    }

    //���s���[�V����
    public void PlaySpiderAnimSetRun(bool AnimFlag)
    {
        animator.SetBool("run", AnimFlag);
    }

}