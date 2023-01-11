using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBattleClear : MonoBehaviour
{
    [Header("�v���C���[�Q��"), SerializeField]
    private PlayerStatus_Solo player;

    [Header("�{�X�Q��")]
    [SerializeField]
    BossEnemy_HPManager bossHP;

    [Header("�ړ������R���C�_�["), SerializeField]
    private Collider moveStopCollider;

    [Header("����������X�N���v�g")]
    [SerializeField]
    MonoBehaviour[] stopScripts = new MonoBehaviour[0];

    [SerializeField]
    GameObject finishText;

    //��������
    bool win;
    //�I�[�f�B�I�ǉ�
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip winJingle;

    //�N���A��̏���
    [Header("�t�F�[�h�A�E�g��Ɏw�肵���V�[���ֈȍ~")]
    [SerializeField] FadeManager fadeMgr;
    [SerializeField] float fadeStartWait;
    private float fadeWaitCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        win = false;
        finishText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (bossHP.JudgeDie())
        {
            if (player.GetState() != PlayerStatus_Solo.State.Dead)
            {
                if (!win)
                {
                    finishText.SetActive(true);
                    //audioSource.PlayOneShot(winJingle);

                    moveStopCollider.gameObject.SetActive(true);

                    for (int i = 0; i < stopScripts.Length; ++i)
                    {
                        if (stopScripts[i])
                        {
                            stopScripts[i].enabled = false;
                        }
                    }

                    win = true;
                }
                else
                {
                    fadeWaitCount += Time.deltaTime;
                    if(fadeWaitCount >= fadeStartWait)
                    {
                        fadeMgr.FadeSceneChange("StoryMenu", 0, 0, 0, 4);
                    }
                }
            }
        }
    }
}
