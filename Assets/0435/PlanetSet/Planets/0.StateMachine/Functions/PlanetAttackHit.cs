using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetAttackHit : PlanetStateFanction
{
    [Header("プレイヤーにぶつかった際、生成するエフェクト")]
    [SerializeField]
    private GameObject effectPrefab;
    [SerializeField]
    private Vector3 effectSize = Vector3.one;

    public override PlanetStateMachine.State Fanction(float deltaTime)
    {
        //エフェクト生成
        GameObject go = Instantiate(effectPrefab);
        go.transform.position = this.transform.position;
        go.transform.localScale = effectSize;

        //自身を破壊
        Destroy(gameObject);
        return PlanetStateMachine.State.Destroy;
    }
}
