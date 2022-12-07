using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] EnemyCreater_Data.EnemyName enemyName; // 生成したい敵の名前
    [SerializeField] int eventId; // 話しかけたNPCのイベントID
    [Header("全NPCの状態")]
    public List<NPCClass.NPCState> npcStateList;

    /// <summary>
    /// バトル移行時に呼ぶ関数
    /// </summary>
    /// <param name="enemyName">敵の種類</param>
    public void StoryEndNPCData(EnemyCreater_Data.EnemyName enemy,int id)
    {
        eventId = id;
        enemyName = enemy;
        SceneManager.LoadScene("StoryBattle");
    }    

    public void BattleEndData()
    {
        // イベントが終了したNPCの状態をEventEndに変更
        npcStateList[eventId] = NPCClass.NPCState.BattleEventEnd;
    }
}
