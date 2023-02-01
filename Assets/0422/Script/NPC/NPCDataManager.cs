using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDataManager : MonoBehaviour
{
    #region �V���O���g��

    private static NPCDataManager npcInstance;

    public static NPCDataManager NPCInstance
    {
        get
        {
            if (npcInstance == null)
            {
                npcInstance = (NPCDataManager)FindObjectOfType(typeof(NPCDataManager));

                if (npcInstance == null)
                {
                    Debug.LogError("NPCDataManager Instance nothing");
                }
            }

            return npcInstance;
        }
    }

    private void Awake()
    {
        if (this != NPCInstance)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    #endregion

    [Header("NPC�f�[�^")]
    public EnemyCreater_Data.EnemyName enemyName; // �����������G�̖��O
    [SerializeField] int eventId; // �b��������NPC�̃C�x���gID
    [Header("�SNPC�̏��")]
    public NPCClass.NPCState[] npcStateList;
    NPCList list;
    
    /// <summary>
    /// �o�g���ڍs���ɌĂԊ֐�
    /// <param name="id">�C�x���gID</param>
    /// <param name="enemyName">�G�̎��</param>
    public void StoryEndNPCData(EnemyCreater_Data.EnemyName enemy,int id)
    {
        eventId = id;
        enemyName = enemy;
        list = GameObject.Find("NPCStateList").GetComponent<NPCList>();
        for (int i = 0; i < npcStateList.Length; ++i)
        {
            npcStateList[i] = list.npcList[i].GetState();
        }                
    }    

    public void BattleEndData()
    {
        // �C�x���g���I������NPC�̏�Ԃ�EventEnd�ɕύX
        npcStateList[eventId] = NPCClass.NPCState.BattleEventEnd;
    }
}
