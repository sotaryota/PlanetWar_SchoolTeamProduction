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
    //�^�C���A�b�v���̏������[�V����
    public void PlayAnimWin()
    {
        animator.SetTrigger("win");
    }
    //�^�C���A�b�v���̔s�k���[�V����
    public void PlayAnimLose()
    {
        animator.SetTrigger("lose");
    }

    //���s���[�V����
    public void PlayAnimSetRun(bool AnimFlag)
    {
        animator.SetBool("run", AnimFlag);
    }
    //���s���[�V����
    public void PlayAnimSetJump(bool AnimFlag)
    {
        animator.SetBool("jump", AnimFlag);
    }
    //������[�V����
    public void PlayAnimSetDodge(bool AnimFlag)
    {
        animator.SetBool("dodge", AnimFlag);
    }
    //�؃g�����[�V����
    public void PlayAnimSetTrain(bool AnimFlag)
    {
        animator.SetBool("train", AnimFlag);
    }
    //�J�n���̏������[�V����
    public void PlayAnimSetReady(int Anim)
    {
        animator.SetInteger("ready",Anim);
    }
}
