using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Camera : MonoBehaviour
{
    [Header("‰ñ“]‘¬“x")]
    [SerializeField]
    float rotateSpeed;
    [SerializeField]
    Transform player;
    void Update()
    {
        Vector3 playerPos = player.position;
        transform.RotateAround(playerPos, Vector3.up, rotateSpeed * Time.deltaTime);
        transform.LookAt(playerPos);
    }
}
