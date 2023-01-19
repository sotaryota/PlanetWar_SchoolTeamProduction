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

    //勝ち判定
    bool win;

    //クリア後の処理
    [Header("フェードアウト後に指定したシーンへ以降")]
    [SerializeField] FadeManager fadeMgr;
    [SerializeField] float fadeSpeed;


    // Start is called before the first frame update
    void Start()
    {
        win = false;
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
                    fadeMgr.FadeSceneChange("Ending", 0, 0, 0, fadeSpeed);
                }
            }
        }
    }
}
