using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMine_Bomb : PlanetStateFanction
{
    [Header("��������G�t�F�N�g")]
    [SerializeField] private GameObject effect;

    public override PlanetStateMachine.State Fanction(float deltaTime)
    {
        GameObject go = Instantiate(effect);
        go.transform.position = this.transform.position;
        return PlanetStateMachine.State.Destroy;
    }
}
