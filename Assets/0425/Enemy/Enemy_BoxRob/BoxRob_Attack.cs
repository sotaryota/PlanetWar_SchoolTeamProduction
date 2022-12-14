using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRob_Attack : MonoBehaviour
{
    [Header("ステータス管理スクリプト")]
    [SerializeField] BoxRob_Status boxRob_Status;
    PlayerStatus_Solo playerStatus;
    [SerializeField] BoxRob_Sensing boxRob_Sensing;



    private void Start()
    {
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus_Solo>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player")
        {
           playerStatus.Damage(boxRob_Status.GetPower());
        }
    }
}
