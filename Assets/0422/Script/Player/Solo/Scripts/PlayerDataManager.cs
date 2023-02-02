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
    public Vector3 playerPos;      //プレイヤーの復帰位置
    [SerializeField] private Quaternion playerAngle; //プレイヤーの向き
    [SerializeField] private Quaternion cameraAngle; //カメラの位置と向き

    /// <summary>
    /// ストーリー復帰時に呼ぶ関数
    /// </summary>
    /// <param name="pPos">プレイヤーの復帰位置</param>
    /// <param name="cAngle">プレイヤーの向き</param>
    /// <param name="pAngle">カメラの位置と向き</param>
    public void StoryStartPlayerPos(ref Vector3 pPos, ref Quaternion pAngle, ref Quaternion cAngle)
    {
        pPos   = playerPos;
        pAngle = playerAngle;
        cAngle = cameraAngle;
    }

    /// <summary>
    /// バトル移行時呼ぶ関数
    /// </summary>
    /// <param name="pPos">プレイヤーの復帰位置</param>
    /// <param name="pAngle">プレイヤーの向き</param>
    /// <param name="cAngle">カメラの位置と向き</param>
    public void StoryEndPlayerPos(Vector3 pPos, Quaternion pAngle,Quaternion cAngle)
    {
        playerPos   = pPos;
        playerAngle = pAngle;
        cameraAngle = cAngle;
    }
}
