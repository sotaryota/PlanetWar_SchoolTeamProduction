using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// 惑星が指定座標を中心として回転を行う
/// 
/// </summary>


public class PlanetRotateAround : PlanetStateFanction
{
    [Header("回転の中心座標")]
    [SerializeField]
    private Vector3 centerPos;

    [Header("回転速度")]
    [SerializeField]
    private float moveSpeed;
    public void SetMoveSpeed(float value) { moveSpeed = value; }

    [Header("回転軸")]
    [SerializeField]
    [Range(0,1)]
    private float moveX;
    [SerializeField]
    [Range(0, 1)]
    private float moveY;
    [SerializeField]
    [Range(0, 1)]
    private float moveZ;


    public override PlanetStateMachine.State Fanction(float deltaTime)
    {
        Vector3 moveAngle = new Vector3(moveX, moveY, moveZ);
        transform.RotateAround(centerPos, moveAngle, moveSpeed * Time.deltaTime);

        return PlanetStateMachine.State.Now;
    }
}
