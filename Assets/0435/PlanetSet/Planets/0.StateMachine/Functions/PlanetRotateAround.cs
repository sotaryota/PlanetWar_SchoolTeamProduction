using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// �f�����w����W�𒆐S�Ƃ��ĉ�]���s��
/// 
/// </summary>


public class PlanetRotateAround : PlanetStateFanction
{
    [Header("��]�̒��S���W")]
    [SerializeField]
    private Vector3 centerPos;

    [Header("��]���x")]
    [SerializeField]
    private float moveSpeed;

    [Header("��]��")]
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
