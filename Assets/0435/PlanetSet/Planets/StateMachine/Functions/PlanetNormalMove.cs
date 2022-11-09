using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetNormalMove : PlanetStateFanction
{
    [Header("ˆÚ“®‘¬“x")]
    [SerializeField]
    private Vector3 speed;

    public void ThrowMoveSetting(Vector3 speed_)
    {
        speed = speed_;
    }

    private void Start()
    {
        //¡‚Ì‚Æ‚±‚ë‚È‚µ
    }

    public override PlanetStateMachine.State Fanction(float deltaTime)
    {
        //Œü‚¢‚Ä‚¢‚é•ûŒü‚ÖˆÚ“®
        this.transform.Translate(speed * deltaTime);
        return PlanetStateMachine.State.Now;
    }
}
