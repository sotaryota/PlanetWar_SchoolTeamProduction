using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetThrowHit : PlanetStateFanction
{
    bool hit;
    public int catchPlayerID = 2;

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
            if (other.transform.tag == "CatchArea") {Å@return; }

            PlanetData pd;
            if(pd = other.GetComponent<PlanetData>())
            {
                if(this.GetComponent<PlanetData>().GetWeight() >= pd.GetWeight()) {
                        return;
                }
            }

            if (ps = other.GetComponent<PlayerStatus>())
            {
                if (catchPlayerID == ps.GetID()) { return; }
            }
            hit = true;
        }
    }
}
