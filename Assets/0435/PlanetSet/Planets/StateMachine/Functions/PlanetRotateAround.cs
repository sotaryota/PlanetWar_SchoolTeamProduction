using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// ˜f¯‚ªw’èÀ•W‚ğ’†S‚Æ‚µ‚Ä‰ñ“]‚ğs‚¤
/// 
/// </summary>


public class PlanetRotateAround : PlanetStateFanction
{
    [Header("‰ñ“]‚Ì’†SÀ•W")]
    [SerializeField]
    private Vector3 centerPos;

    [Header("‰ñ“]‘¬“x")]
    [SerializeField]
    private float moveSpeed;

    [Header("‰ñ“]²")]
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
