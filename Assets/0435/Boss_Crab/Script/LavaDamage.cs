using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDamage : MonoBehaviour
{
    [SerializeField]
    private float damage;

    private void OnTriggerStay(Collider other)
    {
        print(other.transform.tag);
        if (other.transform.tag != "Player") return;
        print("ダメージ");
        other.GetComponent<PlayerStatus_Solo>().Damage(damage * Time.deltaTime);
    }

    private void OnCollisionStay(Collision collision)
    {
        print(collision.transform.tag);
        if (collision.transform.tag != "Player") return;
        print("ダメージ");
        collision.transform.GetComponent<PlayerStatus_Solo>().Damage(damage * Time.deltaTime);
    }
}
