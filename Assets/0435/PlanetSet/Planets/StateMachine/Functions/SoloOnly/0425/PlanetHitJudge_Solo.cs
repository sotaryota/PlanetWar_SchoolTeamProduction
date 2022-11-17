using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetHitJudge_Solo : PlanetStateFanction
{
    [SerializeField] Enemy_HpData enemy_HpData;

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
              

                hit = true;
            }
            else if (other.transform.tag == "Enemy")
            {
                enemy_HpData.Damage(10);

                hit = true;
            }
        }
    }
}
