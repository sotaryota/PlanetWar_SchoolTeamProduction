using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetHitJudgeEnemy : PlanetStateFanction
{


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
            PlayerStatus ps;
            if (other.transform.tag != "Enemy") { return; }

            PlanetData pd = this.GetComponent<PlanetData>();
            Enemy_HpData ehp;
            if (ehp = other.GetComponent<Enemy_HpData>())
            {
                ehp.Damage(pd.GetDamage());
            }

            hit = true;
        }
    }
}
