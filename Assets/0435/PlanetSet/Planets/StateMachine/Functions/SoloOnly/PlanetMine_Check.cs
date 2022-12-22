using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMine_Check : PlanetStateFanction
{
    [Header("�w�肳�ꂽ���̂����g����Q��")]
    [SerializeField] private PlanetFallMeteor fallScript;
    [SerializeField] private Collider collider;
    [SerializeField] private GameObject warningObject;

    private bool setActiveOnce = true;
    private bool hit = false;

    public override PlanetStateMachine.State Fanction(float deltaTime)
    {
        if (setActiveOnce)
        {
            if (!fallScript.GetMoveFlag())
            {
                collider.enabled = true;
                warningObject.SetActive(true);
                setActiveOnce = false;
            }
        }


        if (hit == true)
        {
            return PlanetStateMachine.State.Hit;
        }
        return PlanetStateMachine.State.Now;
    }

    private void OnTriggerEnter(Collider other)
    {
        //�w�肳�ꂽ�^�O�������̏ꍇ�͔���Ȃ�
        if (other.transform.tag == "Ground") return;
        if (other.transform.tag == "CatchArea") return;

        hit = true;
    }
}
