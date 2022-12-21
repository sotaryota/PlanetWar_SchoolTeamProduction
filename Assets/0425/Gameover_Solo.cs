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

    //�I�[�f�B�I�ǉ�
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip deadJingle;
    //�Q�[���I�[�o�[���̃��j���[
    [SerializeField]
    GameoverMenu gameoverMenu;
    //�|�[�Y�ł��Ȃ��悤�ɂ���
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
        Time.unscaledDeltaTime / 2.0f, //�p�[�e�B�N���V�X�e���𑁑��肷�鎞��
        true,        //�q�̃p�[�e�B�N���V�X�e�������ׂđ����肷�邩�ǂ���
        false             //�ċN�����ŏ�����Đ����邩�ǂ���
        );
    }
}
