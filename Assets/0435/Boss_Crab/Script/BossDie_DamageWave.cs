using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDie_DamageWave : MonoBehaviour
{
    public void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Enemy")
        {
            if (other.GetComponent<Enemy_HpData>())
            {
                other.GetComponent<Enemy_HpData>().Damage(9999);
            }
        }
    }


}
