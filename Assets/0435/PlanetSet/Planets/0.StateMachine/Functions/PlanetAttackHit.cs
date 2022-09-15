using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetAttackHit : PlanetStateFanction
{
    [Header("�v���C���[�ɂԂ������ہA��������G�t�F�N�g")]
    [SerializeField]
    private GameObject effectPrefab;
    [SerializeField]
    private Vector3 effectSize = Vector3.one;

    public override PlanetStateMachine.State Fanction(float deltaTime)
    {
        //�G�t�F�N�g����
        GameObject go = Instantiate(effectPrefab);
        go.transform.position = this.transform.position;
        go.transform.localScale = effectSize;

        //���g��j��
        Destroy(gameObject);
        return PlanetStateMachine.State.Destroy;
    }
}
