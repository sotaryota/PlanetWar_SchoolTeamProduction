using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rob1_AnimManeger : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    //�������[�V����
    public void PlayRob1AnimThrow()
    {
        animator.SetTrigger("throw");
    }
    //�_���[�W���[�V����
    public void PlayRob1AnimDamage()
    {
        animator.SetTrigger("damage");
    }
    //���S���[�V����
    public void PlayRob1AnimDie()
    {
        animator.SetTrigger("die");
    }
   //�f���̐������[�V����
   public void PlayRob1AnimGeneration()
    {
        animator.SetTrigger("generation");
    }


    //���s���[�V����
    public void PlayRob1AnimSetRun(bool AnimFlag)
    {
        animator.SetBool("run", AnimFlag);
    }
 
}
