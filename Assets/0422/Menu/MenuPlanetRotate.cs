using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlanetRotate : MonoBehaviour
{
    [Header("Ž©“]‘¬“x")]
    [SerializeField]
    private Vector3 rotateSpeed;
    private Quaternion nowRotation;

    void Start()
    {
        nowRotation = this.transform.rotation;
    }

    // Update is called once per frame
    public void Update()
    {
        this.transform.rotation = Quaternion.Euler(nowRotation.eulerAngles + (rotateSpeed * Time.deltaTime));
        nowRotation = this.transform.rotation;
    }
}
