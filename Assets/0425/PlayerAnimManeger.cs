using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimManeger : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    //投げモーション
    public void PlayAnimThrow()
    {
        animator.SetTrigger("throw");
    }
    //ダメージモーション
    public void PlayAnimDamage()
    {
        animator.SetTrigger("damage");
    }
    //惑星での死亡モーション
    public void PlayAnimDie()
    {
        animator.SetTrigger("die");
    }
    //太陽での死亡モーション
    public void PlayAnimExhausted()
    {
        animator.SetTrigger("exhausted");
    }
    //挑発モーション
    public void PlayAnimAppeal()
    {
        animator.SetTrigger("appeal");
    }
    //タイムアップ時の勝利モーション
    public void PlayAnimWin()
    {
        animator.SetTrigger("win");
    }
    //タイムアップ時の敗北モーション
    public void PlayAnimLose()
    {
        animator.SetTrigger("lose");
    }

    //走行モーション
    public void PlayAnimSetRun(bool AnimFlag)
    {
        animator.SetBool("run", AnimFlag);
    }
    //走行モーション
    public void PlayAnimSetJump(bool AnimFlag)
    {
        animator.SetBool("jump", AnimFlag);
    }
    //回避モーション
    public void PlayAnimSetDodge(bool AnimFlag)
    {
        animator.SetBool("dodge", AnimFlag);
    }
    //筋トレモーション
    public void PlayAnimSetTrain(bool AnimFlag)
    {
        animator.SetBool("train", AnimFlag);
    }
    //開始時の準備モーション
    public void PlayAnimSetReady(int Anim)
    {
        animator.SetInteger("ready",Anim);
    }
}
