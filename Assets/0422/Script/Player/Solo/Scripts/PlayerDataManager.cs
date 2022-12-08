using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{

    #region シングルトン

    private static PlayerDataManager playerInstance;

    public static PlayerDataManager PlayerInstance
    {
        get
        {
            if (playerInstance == null)
            {
                playerInstance = (PlayerDataManager)FindObjectOfType(typeof(PlayerDataManager));

                if (playerInstance == null)
                {
                    Debug.LogError("PlayerDataManager Instance nothing");
                }
            }

            return playerInstance;
        }
    }

    private void Awake()
    {
        if (this != PlayerInstance)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    #endregion

    [Header("プレイヤーデータ")]
    [SerializeField] private float playerHp;    //プレイヤーのHP
    [SerializeField] private float playerPower; //プレイヤーのパワー
    [SerializeField] private Vector3 playerPos; //プレイヤーの復帰位置

    /// <summary>
    /// ストーリー復帰時に呼ぶ関数
    /// </summary>
    /// <param name="hp">プレイヤーのHP</param>
    /// <param name="power">プレイヤーのパワー</param>
    /// <param name="pos">プレイヤーの復帰位置</param>
    public void StoryStartPlayerData(ref float hp,ref float power,ref Vector3 pos)
    {
        hp    = playerHp;
        power = playerPower;
        pos   = playerPos;
    }

    /// <summary>
    /// バトル移行時呼ぶ関数
    /// </summary>
    /// <param name="hp">プレイヤーのHP</param>
    /// <param name="power">プレイヤーのパワー</param>
    /// <param name="pos">プレイヤーの復帰位置</param>
    public void StoryEndPlayerData(float hp, float power, Vector3 pos)
    {
        playerHp    = hp;
        playerPower = power;
        playerPos   = pos;
    }

    /// <summary>
    /// バトル終了時にプレイヤーで呼ぶ関数
    /// </summary>
    /// <param name="hp">プレイヤーのHP</param>
    /// <param name="power">プレイヤーのパワー</param>
    public void BattleEndPlayerData(float hp, float power)
    {
        playerHp    = hp;
        playerPower = power;
    }

}
