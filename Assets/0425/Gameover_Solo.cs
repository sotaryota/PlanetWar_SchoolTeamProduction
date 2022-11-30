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

            if (dead == false)
            {
                playerAnimManeger.PlayAnimDie();
                gameoverStaging.SetActive(true);
                gameStart_solo.ScriptStop();
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
}
