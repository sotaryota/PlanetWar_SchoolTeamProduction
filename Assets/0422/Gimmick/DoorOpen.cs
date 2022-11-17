using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] Animation animation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            animation.playAutomatically = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            animation.playAutomatically = true;
        }
    }
}
