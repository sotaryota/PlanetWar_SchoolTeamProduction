using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetAttackHit : PlanetStateFanction
{
    [Header("プレイヤーにヒットした際に")]
    [Header("生成するエフェクト")]
    [SerializeField]
    private GameObject effectPrefab;
    [SerializeField]
    private Vector3 effectSize = Vector3.one;

    [Header("鳴らしたい効果音の情報")]
    public AudioSource source;
    public AudioClip clip;

    public override PlanetStateMachine.State Fanction(float deltaTime)
    {
        //エフェクト生成
        GameObject go = Instantiate(effectPrefab);
        go.transform.position = this.transform.position;
        go.transform.localScale = effectSize;

        //効果音を鳴らす
        if (source)
        {
            source.PlayOneShot(clip);
        }
        else
        {
            print("PlanetAttackHit：AudioSourceの参照がありません。");
        }

        //自身を破壊
        Destroy(gameObject);
        return PlanetStateMachine.State.Destroy;
    }
}
