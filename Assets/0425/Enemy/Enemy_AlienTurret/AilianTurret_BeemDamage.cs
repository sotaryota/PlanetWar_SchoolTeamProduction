using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AilianTurret_BeemDamage : MonoBehaviour
{
    [SerializeField] AilianTurret_Status ailianTurret_Status;


    private void OnParticleCollision(GameObject other)
    {
        PlayerStatus_Solo pss;
        if(pss = other.GetComponent<PlayerStatus_Solo>())
        {
            pss.Damage(ailianTurret_Status.GetPower());
        }
    }
}
