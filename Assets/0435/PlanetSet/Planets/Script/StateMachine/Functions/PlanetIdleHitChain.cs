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

            //投げ状態へ
            return PlanetStateMachine.State.Throw;
        }

        return PlanetStateMachine.State.Now;
    }

    private void OnTriggerEnter(Collider other)
    {

        //ヒットしたのが惑星だった場合
        if (other.transform.tag == "Planet")
        {
            PlanetStateMachine myPSM = GetComponent<PlanetStateMachine>();
            //自身が待機状態の場合
            if (myPSM.GetState() == PlanetStateMachine.State.Idle)
            {
                PlanetStateMachine youPSM = other.GetComponent<PlanetStateMachine>();
                if (youPSM.GetState() == PlanetStateMachine.State.Throw)
                {
                    //重さが自身よりも重かった場合
                    if (this.GetComponent<PlanetData>().GetWeight() <=
                        other.GetComponent<PlanetData>().GetWeight())
                    {
                        //ヒットした惑星の状態を保存し、ヒットフラグをtrueに
                        hitPlanetPos = other.transform.position;
                        this.transform.rotation = Quaternion.Euler(Vector3.zero);
                        hit = true;
                    }
                }
            }
        }
    }
}
