using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter_Attack : MonoBehaviour
{
    [Header("ステータス管理スクリプト")]
    [SerializeField] Fighter_Status fighter_Status;
    [SerializeField] PlayerStatus_Solo playerStatus;
    [SerializeField] Fighter_Sensing fighter_Sensing;

    private void Start()
    {
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus_Solo>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (fighter_Sensing.attackHit == false)
            {
                playerStatus.Damage(fighter_Status.GetPower());
                fighter_Sensing.attackHit = true;
            }
        }
    }
}
