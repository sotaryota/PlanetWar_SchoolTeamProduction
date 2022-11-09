using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetNormalRotation : PlanetStateFanction
{
    [Header("Ž©“]‘¬“x")]
    [SerializeField]
    private Vector3 rotateSpeed;
    private Quaternion nowRotation;

    // Start is called before the first frame update
    void Start()
    {
        nowRotation = this.transform.rotation;
    }

    public override PlanetStateMachine.State Fanction(float deltaTime)
    {
        this.transform.rotation = Quaternion.Euler(nowRotation.eulerAngles + (rotateSpeed * Time.deltaTime));
        nowRotation = this.transform.rotation;
        return PlanetStateMachine.State.Now;
    }
}
