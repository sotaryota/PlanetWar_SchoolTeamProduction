using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AilianTurret_Move : MonoBehaviour
{
    [SerializeField] AilianTurret_Status ailianTurret_Status;

    [SerializeField] float rotateSpeed;

    GameObject player;
    private void Start()
    {
        rotateSpeed = ailianTurret_Status.GetSpeed();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (ailianTurret_Status.GetState() == AilianTurret_Status.State.Non || ailianTurret_Status.GetState() == AilianTurret_Status.State.Dead)
        { return; }

        // ターゲット方向のベクトルを取得
        Vector3 relativePos = player.transform.position - this.transform.position;
        // 方向を、回転情報に変換
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        // 現在の回転情報と、ターゲット方向の回転情報を補完する
        transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, rotateSpeed * Time.deltaTime);

    }
}