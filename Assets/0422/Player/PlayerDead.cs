using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    [SerializeField]
    PlayerStatus status;
    [SerializeField]
    Animator animator;

    public enum DeadState
    {
        Hit,      //惑星ヒット
        die,      //惑星ヒットで死亡
        exhausted, //太陽のスリップダメージで死亡
        non
    };

    [SerializeField]
    DeadState deadState;

    //追加
    private bool firstInFlag;

    public void SetDeadState(DeadState state)
    {
        deadState = state;
    }

    public DeadState GetDead()
    {
        return deadState;
    }

    private void Start()
    {
        deadState = DeadState.non;
        firstInFlag = true;
    }

    private void Update()
    {
        Dead();
    }

    bool DeadCheck()
    {
        return status.GetState() == PlayerStatus.State.Dead;
    }

    void Dead()
    {
        if (!DeadCheck()) { return; }
        if (!firstInFlag) { return; }
        firstInFlag = false;

        switch (deadState)
        {
            //惑星ヒットでの死亡時-----------------------------------------
            case DeadState.die:
                Debug.Log("惑星ヒットにより死亡");

                //アニメーション
                animator.SetTrigger("die");

                //死亡ボイス再生
                this.GetComponent<PlayerSEManager>().DeathVoice();
                break;

            //太陽のスリップダメージでの死亡時-----------------------------
            case DeadState.exhausted:
                Debug.Log("スリップダメージにより死亡");

                //アニメーション
                animator.SetTrigger("exhausted");

                //死亡ボイス再生
                this.GetComponent<PlayerSEManager>().DeathVoice();
                break;

            default:
                firstInFlag = true;
                break;
        }

        print("PlayerDead:追加部分");
        
    }
}

