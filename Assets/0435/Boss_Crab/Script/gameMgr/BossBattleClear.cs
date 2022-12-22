using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleClear : MonoBehaviour
{
    [Header("ボス参照")]
    [SerializeField]
    BossEnemy_HPManager bossHP;

    [SerializeField]
    GameObject finishText;

    //勝ち負け判定
    bool win;
    //オーディオ追加
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip winJingle;

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
            if (!win)
            {
                finishText.SetActive(true);
                audioSource.PlayOneShot(winJingle);
                win = true;
            }
        }
    }
}
