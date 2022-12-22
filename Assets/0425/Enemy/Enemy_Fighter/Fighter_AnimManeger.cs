using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter_AnimManeger : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    //�U�����[�V����(�g��)
    public void PlayFighterAnimHadou()
    {
        animator.SetTrigger("hadou");
    }
    //�U�����[�V����(����)
    public void PlayFighterAnimSyoryu()
    {
        animator.SetTrigger("syoryu");
    }
    //�U�����[�V����(����)
    public void PlayFighterAnimTatumaki()
    {
        animator.SetTrigger("tatumaki");
    }
    //���S���[�V����
    public void PlayFighterAnimDie()
    {
        animator.SetTrigger("die");
    }

    //���s���[�V����
    public void PlayFighterAnimSetRun(bool AnimFlag)
    {
        animator.SetBool("run", AnimFlag);
    }

}
