using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AilianTurret_Attack : MonoBehaviour
{
    [SerializeField] AilianTurret_Status ailianTurret_Status;

    [SerializeField] ParticleSystem attack_Beem;

    [SerializeField] float attackInterval;

    [SerializeField] PlayerStatus_Solo playerStatus_Solo;

    private void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ailianTurret_Status.GetState() == AilianTurret_Status.State.Non || ailianTurret_Status.GetState() == AilianTurret_Status.State.Dead)
        { return; }

        attack_Beem.Play();
    }

    private void OnParticleCollision(GameObject other)
    {
        playerStatus_Solo.Damage(ailianTurret_Status.GetPower());
    } 

}
