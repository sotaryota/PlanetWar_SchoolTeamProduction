using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_Camera : MonoBehaviour
{
    [Header("‰ñ“]‘¬“x")]
    [SerializeField]
    Vector3 rotateSpeed;

    void Update()
    {
        this.transform.Rotate(rotateSpeed * Time.deltaTime, Space.World);
    }
}
