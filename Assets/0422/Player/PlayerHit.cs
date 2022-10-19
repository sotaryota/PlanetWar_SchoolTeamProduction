using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private Animator animator;
    PlayerStatus status;
    [SerializeField]
    PlayerDead dead;

    private void Start()
    {
        animator = GetComponent<Animator>();
        status   = GetComponent<PlayerStatus>();
    }


    private void OnTriggerEnter(Collider other)
    {
        //ヒットしたのがプレイヤーの場合は無効
        if(other.transform.tag == "Player") { return; }

        //ヒットしたのがヒットエリアの場合は無効
        if (other.transform.tag == "CatchArea") { return; }

        //プラネットがThrow状態
        if (other.GetComponent<PlanetStateMachine>().GetState() == PlanetStateMachine.State.Throw)
        {
            //プラネットのIDが自身と一致する場合は処理をしない
            if (other.GetComponent<PlanetThrowHit>().catchPlayerID == status.GetID()) { return; }

            //プレイヤがCatch状態の時
            if (status.GetState() == PlayerStatus.State.Catch)
            {
                //ダメージ状態に変更
                status.SetState(PlayerStatus.State.Damage);

                //Catch状態に戻す
                status.SetState(PlayerStatus.State.Catch);
            }
            else
            {
                //Catchでないならダメージ状態に変更するだけ
                status.SetState(PlayerStatus.State.Damage);
            }

            //ダメージ処理
            status.Damage(other.GetComponent<PlanetData>().GetDamage());

            //ディフェンスを上昇
            status.DefenseUp(25 + (other.GetComponent<PlanetData>().GetDamage() / 5));

            //HPが0以下の時
            if (status.GetHp() <= 0)
            {
                //プレイヤを死亡状態に変更
                status.SetState(PlayerStatus.State.Dead);

                dead.SetDeadState(PlayerDead.DeadState.die);
            }
            else
            {
                //プレイヤをヒット状態に変更
                status.SetState(PlayerStatus.State.Damage);

                Debug.Log("惑星ヒット");

                //ダメージボイス再生
                this.GetComponent<PlayerSEManager>().DamageVoice();

                //アニメーション
                animator.SetTrigger("damage");
            }
        }
    }

    private void Update()
    {
        if (dead.GetDead() == PlayerDead.DeadState.non)
        {
            //HPが0以下の時
            if (status.GetHp() <= 0)
            {
                //プレイヤを死亡状態に変更
                status.SetState(PlayerStatus.State.Dead);

                dead.SetDeadState(PlayerDead.DeadState.exhausted);
            }
        }
    }
}
