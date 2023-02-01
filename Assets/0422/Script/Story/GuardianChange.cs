using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianChange : MonoBehaviour
{
    [SerializeField] NPCList list;
    [SerializeField] GameObject guardianObj;
    [SerializeField] GameObject openGuardianObj;
    [SerializeField] int openValue;
    [SerializeField] int clearCount = 0;

    private void Start()
    {
        for (int i = 0; i < list.npcList.Length; ++i)
        {
            if (list.npcList[i].GetState() == NPCClass.NPCState.BattleEventEnd)
            {
                clearCount++;
            }
        }

        if (clearCount >= openValue)
        {
            guardianObj.SetActive(false);
            openGuardianObj.SetActive(true);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
