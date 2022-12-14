using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBattleClear : MonoBehaviour
{
    [Header("プレイヤー参照"), SerializeField]
    private PlayerStatus_Solo player;

    [Header("ボス参照")]
    [SerializeField]
    BossEnemy_HPManager bossHP;

    [Header("移動制限コライダー"), SerializeField]
    private Collider moveStopCollider;

    [Header("無効化するスクリプト")]
    [SerializeField]
    MonoBehaviour[] stopScripts = new MonoBehaviour[0];

    [SerializeField]
    GameObject finishText;

    //勝ち判定
    bool win;
    //オーディオ追加
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip winJingle;

    //クリア後の処理
    [Header("フェードアウト後に指定したシーンへ以降")]
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
