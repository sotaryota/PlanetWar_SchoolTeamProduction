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
    public Vector3 playerPos;      //�v���C���[�̕��A�ʒu
    [SerializeField] private Quaternion playerAngle; //�v���C���[�̌���
    [SerializeField] private Quaternion cameraAngle; //�J�����̈ʒu�ƌ���

    /// <summary>
    /// �X�g�[���[���A���ɌĂԊ֐�
    /// </summary>
    /// <param name="pPos">�v���C���[�̕��A�ʒu</param>
    /// <param name="cAngle">�v���C���[�̌���</param>
    /// <param name="pAngle">�J�����̈ʒu�ƌ���</param>
    public void StoryStartPlayerPos(ref Vector3 pPos, ref Quaternion pAngle, ref Quaternion cAngle)
    {
        pPos   = playerPos;
        pAngle = playerAngle;
        cAngle = cameraAngle;
    }

    /// <summary>
    /// �o�g���ڍs���ĂԊ֐�
    /// </summary>
    /// <param name="pPos">�v���C���[�̕��A�ʒu</param>
    /// <param name="pAngle">�v���C���[�̌���</param>
    /// <param name="cAngle">�J�����̈ʒu�ƌ���</param>
    public void StoryEndPlayerPos(Vector3 pPos, Quaternion pAngle,Quaternion cAngle)
    {
        playerPos   = pPos;
        playerAngle = pAngle;
        cameraAngle = cAngle;
    }
}
