using System;
using UnityEngine;

[Serializable]
public class SaveData
{
    /// <summary>
    /// npc�̃f�[�^���ɉ����Ĕz��̃T�C�Y��ύX
    /// </summary>
    public NPCClass.NPCState[] npcStateList = new NPCClass.NPCState[22];
    public Vector3 PlayerPos;
}
