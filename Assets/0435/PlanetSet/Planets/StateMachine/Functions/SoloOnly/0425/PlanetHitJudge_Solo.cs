using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetHitJudge_Solo : PlanetStateFanction
{
    [Header("�\�����[�h�̃v���C���[�Ƃ̂ݔ���")]
    [SerializeField] PlanetData planetData;

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
            print("�q�b�g������");
            if (other.transform.tag == "Player") {

                PlayerStatus_Solo pss;
                if(pss = other.GetComponent<PlayerStatus_Solo>())
                {
                    pss.Damage(planetData.GetDamage());
                    hit = true;
                }
            }
        }
    }
}
