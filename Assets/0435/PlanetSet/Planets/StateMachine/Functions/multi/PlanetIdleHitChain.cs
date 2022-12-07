using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetIdleHitChain : PlanetStateFanction
{
    bool hit = false;
    bool destroy = false;
    Vector3 hitPlanetPos;

    private void Start()
    {
        hit = false;
        destroy = false;
    }

    public override PlanetStateMachine.State Fanction(float deltaTime)
    {
        if (hit)
        {
            if (destroy)
            {
                //�q�b�g��Ԃ�
                return PlanetStateMachine.State.Hit;
            }
            else
            {
                Vector3 throwSpeed = new Vector3(0, 0, 7.5f);
                Vector3 throwAngle = this.transform.position - hitPlanetPos;
                throwAngle.y = this.transform.position.y;

                GetComponent<PlanetThrowMove>().ThrowMoveSetting(throwSpeed, Quaternion.LookRotation(throwAngle).eulerAngles);

                GetComponent<PlanetThrowHit>().catchPlayerID = 2;

                //������Ԃ�
                return PlanetStateMachine.State.Throw;
            }

        }

        return PlanetStateMachine.State.Now;
    }

    private void OnTriggerEnter(Collider other)
    {
        //�q�b�g�����̂��f���������ꍇ
        if (other.transform.tag == "Planet")
        {
            PlanetStateMachine myPSM = GetComponent<PlanetStateMachine>();
            //���g���ҋ@��Ԃ̏ꍇ
            if (myPSM.GetState() == PlanetStateMachine.State.Idle)
            {
                PlanetStateMachine youPSM = other.GetComponent<PlanetStateMachine>();

                //���̘f�����������Ă����ꍇ�͘A��
                if (youPSM.GetState() == PlanetStateMachine.State.Throw)
                {
                    //�d�������g�����d�������ꍇ
                    if (this.GetComponent<PlanetData>().GetWeight() <=
                        other.GetComponent<PlanetData>().GetWeight())
                    {
                        //�q�b�g�����f���̏�Ԃ�ۑ����A�q�b�g�t���O��true��
                        hitPlanetPos = other.transform.position;
                        this.transform.rotation = Quaternion.Euler(Vector3.zero);
                        hit = true;
                    }
                }
                //�f���̏d�����z������d�������ꍇ
                else if (other.GetComponent<PlanetData>().GetWeight() >= 500)
                {
                    //�q�b�g�����f���̏�Ԃ�ۑ����A�q�b�g�t���O��true��
                    hitPlanetPos = other.transform.position;
                    this.transform.rotation = Quaternion.Euler(Vector3.zero);
                    hit = true;
                    //destroy = true;
                }
            }

        }
    }
}
