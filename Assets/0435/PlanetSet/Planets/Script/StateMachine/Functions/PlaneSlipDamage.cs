using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSlipDamage : PlanetStateFanction
{
    [Header("プレイヤー参照")]
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    [Header("ダメージ距離")]
    [SerializeField] private float distance;

    [Header("DPS")]
    [SerializeField] private float damage;

    public override PlanetStateMachine.State Fanction(float deltaTime)
    {
        //スリップダメージ
        Damage(player1, deltaTime);
        Damage(player2, deltaTime);
        return PlanetStateMachine.State.Now;
    }

    private void Damage(GameObject pl_, float tdt)
    {
        //距離が遠かった場合は処理なし
        Vector3 nowDistance = player1.transform.position - this.transform.position;
        if (nowDistance.magnitude > distance) return;

        PlayerStatus ps = pl_.GetComponent<PlayerStatus>();
        ps.Damage(damage * tdt);
    }

}
