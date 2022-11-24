using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AilianTurret_Attack : MonoBehaviour
{
    [SerializeField] AilianTurret_Status ailianTurret_Status;

    [SerializeField] ParticleSystem attack_Beem;

    [SerializeField] float attackInterval;

    [SerializeField] PlayerStatus_Solo playerStatus_Solo;

    float attackTime = 0;


    private void Update()
    {
        attackTime += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ailianTurret_Status.GetState() == AilianTurret_Status.State.Non || ailianTurret_Status.GetState() == AilianTurret_Status.State.Dead)
        { return; }

        if (attackTime > attackInterval)
        {
            attack_Beem.Play();
            attackTime = 0;
        }
    }
}
