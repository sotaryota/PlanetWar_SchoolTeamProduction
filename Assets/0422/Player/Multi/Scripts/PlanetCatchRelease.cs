using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlanetCatchRelease : MonoBehaviour
{
    Gamepad gamepad;
    
    [Header("キャッチする惑星と位置")]
    [SerializeField]
    private GameObject planet;
    [SerializeField]
    private GameObject planetPos;

    [Header("ステータス管理スクリプト")]
    public PlayerStatus playerStatus;
    [SerializeField] PlayerAnimManeger playerAnimator;

    [Header("惑星を投げた時のウェイト")]
    [SerializeField]
    private float waitTime;
    public bool throwFlag = true;

        // Update is called once per frame
    void Update()
    {
        if (gamepad == null)
        {
            gamepad = Gamepad.all[playerStatus.GetID()];
        }

        if (planet)
        {
            //所持している惑星の位置を腕の位置に
            planet.transform.position = planetPos.transform.position;
            PlanetThrow();
            PlanetRelease();
        }
    }

    //--------------------------------------
    //惑星を投げる処理
    //--------------------------------------

    private void PlanetThrow()
    {
        //惑星を所持していないなら処理をしない
        if (!planet) return;

        //Aボタンが押されたら
        if (gamepad.buttonEast.wasPressedThisFrame)
        {
            PlanetStateMachine stateMachine = planet.GetComponent<PlanetStateMachine>();
            PlanetThrowMove throwMove = planet.GetComponent<PlanetThrowMove>();

            //惑星の状態をThrowにする
            stateMachine.SetState(PlanetStateMachine.State.Throw);

            //惑星のスピードと飛ぶ方向を決める
            Vector3 throwSpeed = new Vector3(0, 0, 10);
            Vector3 playerAngle = this.transform.rotation.eulerAngles;
            Vector3 throwAngle = new Vector3(0, playerAngle.y, 0);
            throwMove.ThrowMoveSetting(throwSpeed, throwAngle);

            //投げるボイス再生
            this.GetComponent<PlayerSEManager>().ThrowVoice();

            //変数を空にして飛ばす
            planet = null;

            //力上昇
            playerStatus.PowerUp(30);

            //アニメーション
            playerAnimator.PlayAnimThrow();

            //硬直時間
            StartCoroutine("ThrowWait");
        }
    }

    //--------------------------------------
    //投げ動作後の硬直時間
    //--------------------------------------

    IEnumerator ThrowWait()
    {
        throwFlag = false;

        yield return new WaitForSeconds(waitTime);

        //プレイヤをStay状態にする
        playerStatus.SetState(PlayerStatus.State.Stay);

        throwFlag = true;
    }

    //--------------------------------------
    //惑星を手放す処理
    //--------------------------------------

    private void PlanetRelease()
    {
        //Xボタンが押されたら
        if (gamepad.buttonNorth.isPressed)
        {
            //惑星の状態をIdle状態に戻す
            PlanetStateMachine stateMachine = planet.GetComponent<PlanetStateMachine>();
            stateMachine.SetState(PlanetStateMachine.State.Idle);
            //プレイヤをStay状態にする
            playerStatus.SetState(PlayerStatus.State.Stay);
            //変数を空にする
            planet = null;
        }
    }

    //--------------------------------------
    //惑星との判定とつかむ処理
    //--------------------------------------

    private void OnTriggerStay(Collider other)
    {
        //硬直時間中は処理をしない
        if (!throwFlag) return;

        //惑星を所持している場合は処理をしない
        if (planet) { return; }
        //惑星である場合
        if (other.gameObject.tag == "Planet")
        {
            //惑星の状態がIdleなら
            if (other.GetComponent<PlanetStateMachine>().GetState() == PlanetStateMachine.State.Idle)
            {
                //ボタンが押された
                if (gamepad.buttonEast.isPressed)
                {
                    //プレイヤーのパワー+ウェイトよりも惑星の重さが小さい
                    if (other.GetComponent<PlanetData>().GetWeight() <= playerStatus.GetPower() + (playerStatus.GetDefense()))   
                    {
                        //プレイヤをCatch状態に
                        playerStatus.SetState(PlayerStatus.State.Catch);

                        //惑星のIDを自身のIDと同じにする
                        other.GetComponent<PlanetThrowHit>().catchPlayerID = playerStatus.GetID();

                        //惑星を所持
                        planet = other.gameObject;

                        //プラネットの状態をCatch状態にする
                        planet.GetComponent<PlanetStateMachine>().SetState(PlanetStateMachine.State.Catch);
                    }
                }
            }
        }
    }
}
