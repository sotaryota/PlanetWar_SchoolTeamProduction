using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetIdleHitChain : PlanetStateFanction
{
    public override PlanetStateMachine.State Fanction(float deltaTime)
    {

        return PlanetStateMachine.State.Now;
    }    
}
