using System;
using UnityEngine;

[Serializable]
public class SaveData
{
    public Vector3 playerPos;      // プレイヤーの位置
    public Quaternion playerAngle; // プレイヤーの向き
    public Quaternion cameraAngle; // プレイヤーのカメラの向き
    /// <summary>
    /// npcのデータ数に応じて配列のサイズを変更
    /// </summary>
    public NPCClass.NPCState[] npcStateList = new NPCClass.NPCState[27];
}
