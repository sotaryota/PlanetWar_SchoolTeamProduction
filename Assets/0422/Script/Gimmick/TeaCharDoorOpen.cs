using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaCharDoorOpen : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] NPCClass[] TeacharObj;
    private void Start()
    {
        for(int i = 0; i < TeacharObj.Length; ++i)
        {
            TeacharObj[i] = TeacharObj[i].GetComponent<NPCClass>();
        }
    }

    private void Update()
    { 
        if (TeacharObj[0].GetState() == NPCClass.NPCState.BattleEventEnd &&
            TeacharObj[1].GetState() == NPCClass.NPCState.BattleEventEnd &&
           (TeacharObj[2].GetState() == NPCClass.NPCState.FriendEventEnd ||
            TeacharObj[2].GetState() == NPCClass.NPCState.BattleEventEnd) &&
           (TeacharObj[3].GetState() == NPCClass.NPCState.FriendEventEnd ||
            TeacharObj[3].GetState() == NPCClass.NPCState.BattleEventEnd))
        {
            animator.SetTrigger("Open");
        }
    }
}
