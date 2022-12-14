using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetNormalMove : PlanetStateFanction
{
    [Header("移動速度")]
    [SerializeField]
    private Vector3 speed;

    public void ThrowMoveSetting(Vector3 speed_)
    {
        speed = speed_;
    }

    private void Start()
    {
        //今のところなし
    }

    public override PlanetStateMachine.State Fanction(float deltaTime)
    {
        //向いている方向へ移動
        this.transform.Translate(speed * deltaTime);
        return PlanetStateMachine.State.Now;
    }
}
