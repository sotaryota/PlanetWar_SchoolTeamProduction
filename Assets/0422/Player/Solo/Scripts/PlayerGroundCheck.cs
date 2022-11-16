using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    [SerializeField] PhysicMaterial playerFriction;
    [SerializeField] float groundFriction;
    [SerializeField] float jumpFriction;
    [SerializeField] float slopeFriction;
    public bool isGroung;

    //ínñ Ç∆ÇÃê⁄êGîªíË
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGroung = true;
            playerFriction.dynamicFriction = groundFriction;
            playerFriction.staticFriction  = groundFriction;
        }
        if (other.gameObject.tag == "Slope")
        {
            isGroung = true;
            playerFriction.dynamicFriction = slopeFriction;
            playerFriction.staticFriction  = slopeFriction;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGroung = false;
            playerFriction.dynamicFriction = jumpFriction;
            playerFriction.staticFriction  = jumpFriction;
        }
        if (other.gameObject.tag == "Slope")
        {
            isGroung = true;
            playerFriction.dynamicFriction = groundFriction;
            playerFriction.staticFriction  = groundFriction;
        }
    }
}
