using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_Camera : MonoBehaviour
{
    [Header("��]���x")]
    [SerializeField]
    Vector3 rotateSpeed;

    void Update()
    {
        this.transform.Rotate(rotateSpeed * Time.deltaTime, Space.World);
    }
}
