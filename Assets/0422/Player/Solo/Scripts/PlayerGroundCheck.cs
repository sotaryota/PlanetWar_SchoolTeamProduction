using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    [SerializeField] PhysicMaterial playerFriction;
    [SerializeField] float jumpFriction;
    [SerializeField] float groundFriction;
    public bool isGroung;

    //ínñ Ç∆ÇÃê⁄êGîªíË
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGroung = true;
            playerFriction.dynamicFriction = jumpFriction;
            playerFriction.staticFriction  = jumpFriction;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGroung = false;
            playerFriction.dynamicFriction = groundFriction;
            playerFriction.staticFriction  = groundFriction;
        }
    }
}
