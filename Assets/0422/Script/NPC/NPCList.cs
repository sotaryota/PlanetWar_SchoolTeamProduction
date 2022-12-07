using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCList : MonoBehaviour
{
    [SerializeField] NPCDataManager npcData;
    [SerializeField] List<NPCClass> npcList;
   
    private void Awake()
    {
        npcData = GameObject.Find("DataManager").GetComponent<NPCDataManager>();
        print("1");
        for (int i = 0;i < npcList.Count;++i)
        {
            npcList[i].SetState(npcData.npcStateList[i]);
            print(npcData.npcStateList[i]);
        }
    }
}
