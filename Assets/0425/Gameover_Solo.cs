using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameover_Solo: MonoBehaviour
{
    [SerializeField]
    PlayerStatus_Solo playerStatus;

    [SerializeField]
    PlayerAnimManeger playerAnimManeger;

    [SerializeField]
    GameObject gameoverStaging;

    [SerializeField]
    GameStart_Solo gameStart_solo;

    bool dead = false;
    [SerializeField]
    float deadTime = 0;

    [SerializeField]
    ParticleSystem deadEffecrt;

    //オーディオ追加
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip deadJingle;
    //ゲームオーバー時のメニュー
    [SerializeField]
    GameoverMenu gameoverMenu;
    //ポーズできないようにする
    [SerializeField]
    PauseMenuSystem pauseMenuSystem;

    // Start is called before the first frame update
    void Start()
    {
        gameoverStaging.SetActive(false);
        deadTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerStatus.GetState() == PlayerStatus_Solo.State.Dead)
        {
            DeadStop();
            DeadEffect();

            if (dead == false)
            {
                deadEffecrt.Play();
                playerAnimManeger.PlayAnimDie();
                gameoverStaging.SetActive(true);
                gameStart_solo.ScriptStop();
                audioSource.PlayOneShot(deadJingle);
                gameoverMenu.PauseSystem();
                pauseMenuSystem.SetCanPause(false);
            }
            dead = true;
        }
    }

    void DeadStop()
    {
        deadTime += Time.unscaledDeltaTime;

        if (deadTime < 0.2)
        {
            Time.timeScale = 0;
        }
        else if (deadTime > 1.5 && deadTime < 4)
        {
            Time.timeScale = 0.2f;
        }
        else if (deadTime >= 4)
        {
            Time.timeScale = 1;
        }
    }

    void DeadEffect()
    {
        deadEffecrt.Simulate(
        Time.unscaledDeltaTime / 2.0f, //パーティクルシステムを早送りする時間
        true,        //子のパーティクルシステムもすべて早送りするかどうか
        false             //再起動し最初から再生するかどうか
        );
    }
}
