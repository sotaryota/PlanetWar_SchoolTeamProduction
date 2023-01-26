using System;
using UnityEngine;

[Serializable]
public class SaveData
{
    /// <summary>
    /// npcのデータ数に応じて配列のサイズを変更
    /// </summary>
    public NPCClass.NPCState[] npcStateList = new NPCClass.NPCState[22];
    public Vector3 PlayerPos;
}
