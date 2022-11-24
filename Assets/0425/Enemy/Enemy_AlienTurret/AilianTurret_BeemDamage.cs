using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AilianTurret_BeemDamage : MonoBehaviour
{
    [SerializeField] PlayerStatus_Solo playerStatus_Solo;
    [SerializeField] AilianTurret_Status ailianTurret_Status;


    private void OnParticleCollision(GameObject other)
    {
        playerStatus_Solo.Damage(ailianTurret_Status.GetPower());
    }
}
