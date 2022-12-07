using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rob2_Attack : MonoBehaviour
{
    [Header("ステータス管理スクリプト")]
    [SerializeField] Rob2_Status rob2_Status;
    [SerializeField] PlayerStatus_Solo playerStatus;
    [SerializeField] Rob2_Sensing rob2_Sensing;

    private void Start()
    {
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus_Solo>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (rob2_Sensing.attackHit == false)
            {
                playerStatus.Damage(rob2_Status.GetPower());
                rob2_Sensing.attackHit = true;
            }
        }
    }
}
