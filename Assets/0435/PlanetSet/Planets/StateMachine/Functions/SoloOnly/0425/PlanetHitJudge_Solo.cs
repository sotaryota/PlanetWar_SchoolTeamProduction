using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetHitJudge_Solo : PlanetStateFanction
{
    [Header("ソロモードのプレイヤーとのみ判定")]
    [SerializeField] Enemy_HpData enemy_HpData;
    [SerializeField] PlayerStatus_Solo playerStatus_Solo;
    [SerializeField] PlanetData planetData;

    bool hit;

    private void Start()
    {
        hit = false;
    }

    public override PlanetStateMachine.State Fanction(float deltaTime)
    {
        if (hit == true)
        {
            return PlanetStateMachine.State.Hit;
        }
        return PlanetStateMachine.State.Now;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlanetStateMachine psm = GetComponent<PlanetStateMachine>();
        if (psm.GetState() == PlanetStateMachine.State.Throw)
        {
            if (other.transform.tag == "Player") {
                playerStatus_Solo.Damage(planetData.GetDamage());
                hit = true;
            }
        }
    }
}
