using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{

    #region �V���O���g��

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

    [Header("�v���C���[�f�[�^")]
    [SerializeField] private float playerHp;    //�v���C���[��HP
    [SerializeField] private float playerPower; //�v���C���[�̃p���[
    [SerializeField] private Vector3 playerPos; //�v���C���[�̕��A�ʒu

    /// <summary>
    /// �X�g�[���[���A���ɌĂԊ֐�
    /// </summary>
    /// <param name="hp">�v���C���[��HP</param>
    /// <param name="power">�v���C���[�̃p���[</param>
    /// <param name="pos">�v���C���[�̕��A�ʒu</param>
    public void StoryStartPlayerData(ref float hp,ref float power,ref Vector3 pos)
    {
        hp    = playerHp;
        power = playerPower;
        pos   = playerPos;
    }

    /// <summary>
    /// �o�g���ڍs���ĂԊ֐�
    /// </summary>
    /// <param name="hp">�v���C���[��HP</param>
    /// <param name="power">�v���C���[�̃p���[</param>
    /// <param name="pos">�v���C���[�̕��A�ʒu</param>
    public void StoryEndPlayerData(float hp, float power, Vector3 pos)
    {
        playerHp    = hp;
        playerPower = power;
        playerPos   = pos;
    }

    /// <summary>
    /// �o�g���I�����Ƀv���C���[�ŌĂԊ֐�
    /// </summary>
    /// <param name="hp">�v���C���[��HP</param>
    /// <param name="power">�v���C���[�̃p���[</param>
    public void BattleEndPlayerData(float hp, float power)
    {
        playerHp    = hp;
        playerPower = power;
    }

}
