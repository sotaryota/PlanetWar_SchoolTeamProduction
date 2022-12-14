using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider_Attack : MonoBehaviour
{
    [Header("ステータス管理スクリプト")]
    [SerializeField] Spider_Status spider_Status;
    PlayerStatus_Solo playerStatus;
    [SerializeField] Spider_Sensing spider_Sensing;

    private void Start()
    {
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus_Solo>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (spider_Sensing.attackHit == false)
            {
                playerStatus.Damage(spider_Status.GetPower());
                spider_Sensing.attackHit = true;
            }
        }
    }
}
