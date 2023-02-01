using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject guardianObj;
    private void Start()
    {
        if(guardianObj.GetComponent<NPCClass>().GetState() == NPCClass.NPCState.BattleEventEnd)
        {
            animator.SetTrigger("Open");
        }
    }
}
