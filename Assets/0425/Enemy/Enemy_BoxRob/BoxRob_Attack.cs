using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRob_Attack : MonoBehaviour
{
    [Header("ステータス管理スクリプト")]
    [SerializeField] BoxRob_Status boxRob_Status;
    [SerializeField] PlayerStatus_Solo playerStatus;
    [SerializeField] BoxRob_Sensing boxRob_Sensing;



    private void Start()
    {
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus_Solo>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (boxRob_Sensing.attackHit == false)
            {
                playerStatus.Damage(boxRob_Status.GetPower());
                boxRob_Sensing.attackHit = true;
            }
        }
    }
}
