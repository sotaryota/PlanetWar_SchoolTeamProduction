using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetIdleHitChain : PlanetStateFanction
{
    bool hit = false;
    Vector3 hitPlanetPos;

    private void Start()
    {
        hit = false;
    }

    public override PlanetStateMachine.State Fanction(float deltaTime)
    {
        if (hit)
        {

            Vector3 throwSpeed = new Vector3(0, 0, 10);
            Vector3 throwAngle = this.transform.position - hitPlanetPos;
            throwAngle.y = this.transform.position.y;

            GetComponent<PlanetThrowMove>().ThrowMoveSetting(throwSpeed, Quaternion.LookRotation(throwAngle).eulerAngles);

            //������Ԃ�
            return PlanetStateMachine.State.Throw;
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
            }
        }
    }
}
