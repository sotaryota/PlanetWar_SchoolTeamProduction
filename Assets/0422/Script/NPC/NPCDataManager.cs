using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] EnemyCreater_Data.EnemyName enemyName;
    [SerializeField] int eventId;
    public List<NPCClass.NPCState> npcStateList;
    /// <summary>
    /// �o�g���ڍs���ɌĂԊ֐�
    /// </summary>
    /// <param name="enemyName">�G�̎��</param>
    public void StoryEndNPCData(EnemyCreater_Data.EnemyName enemy,int id)
    {
        eventId = id;
        enemyName = enemy;
        SceneManager.LoadScene("StoryBattle");
    }    

    public void BattleEndData()
    {
        npcStateList[eventId] = NPCClass.NPCState.EventEnd;
    }
}
