using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter_AnimManeger : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    //攻撃モーション(波動)
    public void PlayFighterAnimHadou()
    {
        animator.SetTrigger("hadou");
    }
    //攻撃モーション(昇竜)
    public void PlayFighterAnimSyoryu()
    {
        animator.SetTrigger("syoryu");
    }
    //攻撃モーション(竜巻)
    public void PlayFighterAnimTatumaki()
    {
        animator.SetTrigger("tatumaki");
    }
    //死亡モーション
    public void PlayFighterAnimDie()
    {
        animator.SetTrigger("die");
    }

    //走行モーション
    public void PlayFighterAnimSetRun(bool AnimFlag)
    {
        animator.SetBool("run", AnimFlag);
    }

}
