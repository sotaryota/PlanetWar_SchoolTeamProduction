using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetNormalMove : PlanetStateFanction
{
    [Header("�ړ����x")]
    [SerializeField]
    private Vector3 speed;

    public void ThrowMoveSetting(Vector3 speed_)
    {
        speed = speed_;
    }

    private void Start()
    {
        //���̂Ƃ���Ȃ�
    }

    public override PlanetStateMachine.State Fanction(float deltaTime)
    {
        //�����Ă�������ֈړ�
        this.transform.Translate(speed * deltaTime);
        return PlanetStateMachine.State.Now;
    }
}
