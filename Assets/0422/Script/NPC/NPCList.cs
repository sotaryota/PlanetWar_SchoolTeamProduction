using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCList : MonoBehaviour
{
    [SerializeField] NPCDataManager npcData;
    public NPCClass[] npcList;
   
    private void Awake()
    {
        npcData = GameObject.Find("DataManager").GetComponent<NPCDataManager>();
        for (int i = 0;i < npcList.Length;++i)
        {
            //NPCの状態を更新
            npcList[i].SetState(npcData.npcStateList[i]);
            print(npcData.npcStateList[i]);
        }
    }
}
