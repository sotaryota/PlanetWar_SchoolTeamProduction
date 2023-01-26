using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCList : MonoBehaviour
{
    [SerializeField] NPCDataManager npcData;
    public List<NPCClass> npcList;
   
    private void Awake()
    {
        //悲しみのFind
        npcData = GameObject.Find("DataManager").GetComponent<NPCDataManager>();
        for (int i = 0;i < npcList.Count;++i)
        {
            //NPCの状態を更新
            npcList[i].SetState(npcData.npcStateList[i]);
            print(npcData.npcStateList[i]);
        }
    }
}
