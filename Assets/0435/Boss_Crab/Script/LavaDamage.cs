using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDamage : MonoBehaviour
{
    [SerializeField]
    private float damage;

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            other.transform.GetComponent<PlayerStatus_Solo>().Damage(damage);
        }
        else if (other.transform.tag == "Enemy")
        {
            other.transform.GetComponent<Spider_Status>().Damage(damage);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<PlayerStatus_Solo>().Damage(damage);
        }
        else if(collision.transform.tag == "Enemy")
        {
            collision.transform.GetComponent<Spider_Status>().Damage(damage);
        }
        
    }
}
