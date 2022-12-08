using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRob_AnimManeger : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    //�������[�V����
    public void PlayBoxRobAnimAttack(bool attackFlag)
    {
        animator.SetBool("attack",attackFlag);
    }
    ////�_���[�W���[�V����
    //public void PlayBoxRobAnimDamage()
    //{
    //    animator.SetTrigger("damage");
    //}
    //���S���[�V����
    public void PlayBoxRobAnimDie()
    {
        animator.SetTrigger("die");
    }
    ////�f���̐������[�V����
    //public void PlayRob1AnimGeneration()
    //{
    //    animator.SetTrigger("generation");
    //}


    //���s���[�V����
    public void PlayBoxRobAnimSetRun(bool AnimFlag)
    {
        animator.SetBool("run", AnimFlag);
    }

}
