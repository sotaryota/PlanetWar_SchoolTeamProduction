using System;
using UnityEngine;

[Serializable]
public class SaveData
{
    /// <summary>
    /// npcのデータ数に応じて配列のサイズを変更
    /// これ何とかしたい
    /// </summary>
    public NPCClass.NPCState[] npcStateList = new NPCClass.NPCState[30];
}
