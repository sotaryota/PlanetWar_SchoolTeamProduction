using System;
using UnityEngine;

[Serializable]
public class SaveData
{
    public Vector3 playerPos;      // �v���C���[�̈ʒu
    public Quaternion playerAngle; // �v���C���[�̌���
    public Quaternion cameraAngle; // �v���C���[�̃J�����̌���
    /// <summary>
    /// npc�̃f�[�^���ɉ����Ĕz��̃T�C�Y��ύX
    /// </summary>
    public NPCClass.NPCState[] npcStateList = new NPCClass.NPCState[27];
}
