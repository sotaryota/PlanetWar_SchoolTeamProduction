using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimManeger : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    //�������[�V����
    public void PlayAnimThrow()
    {
        animator.SetTrigger("throw");
    }
    //�_���[�W���[�V����
    public void PlayAnimDamage()
    {
        animator.SetTrigger("damage");
    }
    //�f���ł̎��S���[�V����
    public void PlayAnimDie()
    {
        animator.SetTrigger("die");
    }
    //���z�ł̎��S���[�V����
    public void PlayAnimExhausted()
    {
        animator.SetTrigger("exhausted");
    }
    //�������[�V����
    public void PlayAnimAppeal()
    {
        animator.SetTrigger("appeal");
    }
    //���s���[�V����
    public void PlayAnimSetRun(bool AnimFlag)
    {
        animator.SetBool("run", AnimFlag);
    }
    //������[�V����
    public void PlayAnimSetDodge(bool AnimFlag)
    {
        animator.SetBool("dodge", AnimFlag);
    }
}