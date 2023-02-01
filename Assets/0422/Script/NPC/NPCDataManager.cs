using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDataManager : MonoBehaviour
{
    #region シングルトン

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

    [Header("NPCデータ")]
    public EnemyCreater_Data.EnemyName enemyName; // 生成したい敵の名前
    [SerializeField] int eventId; // 話しかけたNPCのイベントID
    [Header("全NPCの状態")]
    public NPCClass.NPCState[] npcStateList;
    NPCList list;
    
    /// <summary>
    /// バトル移行時に呼ぶ関数
    /// <param name="id">イベントID</param>
    /// <param name="enemyName">敵の種類</param>
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
        // イベントが終了したNPCの状態をEventEndに変更
        npcStateList[eventId] = NPCClass.NPCState.BattleEventEnd;
    }
}
