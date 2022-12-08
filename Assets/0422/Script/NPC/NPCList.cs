using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCList : MonoBehaviour
{
    [SerializeField] NPCDataManager npcData;
    [SerializeField] List<NPCClass> npcList;
   
    private void Awake()
    {
        //�߂��݂�Find
        npcData = GameObject.Find("DataManager").GetComponent<NPCDataManager>();
        for (int i = 0;i < npcList.Count;++i)
        {
            //NPC�̏�Ԃ��X�V
            npcList[i].SetState(npcData.npcStateList[i]);
            print(npcData.npcStateList[i]);
        }
    }
}
