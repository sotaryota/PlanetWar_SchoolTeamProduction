using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    [SerializeField]
    PlayerStatus status;
    [SerializeField] 
    PlayerAnimManeger playerAnimator;

    //死亡時の判定用
    private bool firstInFlag;

    /// <summary>
    /// 死亡パターン
    /// 追加する場合はDead関数内のswitch文に処理を追加
    /// </summary>
    public enum DeadState
    {
        die,       //惑星ヒットで死亡
        exhausted, //太陽のスリップダメージで死亡
        non
    };

    [SerializeField]
    DeadState deadState;

    //死亡状態の取得と書き換え
    //-----------------------------------------
    public void SetDeadState(DeadState state)
    {
        deadState = state;
    }
    public DeadState GetDead()
    {
        return deadState;
    }
    //-----------------------------------------

    private void Start()
    {
        deadState = DeadState.non;
        firstInFlag = true;
    }
    private void Update()
    {
        Dead();
    }

    void Dead()
    {
        if (status.GetState() != PlayerStatus.State.Dead) { return; }
        if (!firstInFlag) { return; }
        firstInFlag = false;

        #region 死亡時の処理はここで行ってください

        switch (deadState)
        {
            //惑星ヒットでの死亡時-----------------------------------------
            case DeadState.die:
                Debug.Log("惑星ヒットにより死亡");

                //アニメーション
                playerAnimator.PlayAnimDie();

                //死亡ボイス再生
                this.GetComponent<PlayerSEManager>().DeathVoice();
                break;

            //太陽のスリップダメージでの死亡時-----------------------------
            case DeadState.exhausted:
                Debug.Log("スリップダメージにより死亡");

                //アニメーション
                playerAnimator.PlayAnimExhausted();

                //死亡ボイス再生
                this.GetComponent<PlayerSEManager>().DeathVoice();
                break;

            default:
                firstInFlag = true;
                break;
        }
        #endregion

        print("PlayerDead:追加部分");
        
    }
}

