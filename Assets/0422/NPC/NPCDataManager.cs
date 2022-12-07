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
    [SerializeField] EnemyCreater_Data.EnemyName enemyName;
    /// <summary>
    /// バトル移行時に呼ぶ関数
    /// </summary>
    /// <param name="enemyName">敵の種類</param>
    public void StoryEndNPCData(EnemyCreater_Data.EnemyName enemy)
    {
        enemyName = enemy;
    }    
}
