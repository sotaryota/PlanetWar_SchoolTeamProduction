using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rob2_AnimManeger : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    //�U�����[�V����
    public void PlayRob2AnimAttack()
    {
        animator.SetTrigger("attack");
    }
    //�_���[�W���[�V����
    public void PlayRob2AnimDamage()
    {
        animator.SetTrigger("damage");
    }
    //���S���[�V����
    public void PlayRob2AnimDie()
    {
        animator.SetTrigger("die");
    }

    //���s���[�V����
    public void PlayRob2AnimSetRun(bool AnimFlag)
    {
        animator.SetBool("run", AnimFlag);
    }

}
