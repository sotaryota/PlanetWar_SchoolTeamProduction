using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    [SerializeField]
    PlayerStatus[] playerStatus;

    [SerializeField]
    GameObject startText;

    [SerializeField]
    PlayerMove[] playerMove = new PlayerMove[2];

    [SerializeField]
    Animator[] animator = new Animator[2];

    [SerializeField]
    float startTime;

    [SerializeField]
    Timer timerScript;

    [SerializeField] PlayerAnimManeger[] playerAnimator = new PlayerAnimManeger[2];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 2; ++i)
        {
            playerAnimator[i].PlayAnimSetReady(Random.Range(0, 2));
        }

        for (int i = 0; i < playerMove.Length; ++i)
        {
            //animator[i].SetTrigger("start");
            playerMove[i].enabled = false;
        }

        StartCoroutine("StartGame");
    }
    
    IEnumerator StartGame()
    {
        startText.SetActive(true);
        timerScript.enabled = false;



        yield return new WaitForSeconds(startTime);

        timerScript.enabled = true;

        for (int i = 0; i < playerMove.Length; ++i)
        {
            playerMove[i].enabled = true;
            playerStatus[i].SetState(PlayerStatus.State.Stay);
        }
        startText.SetActive(false);
    }
}
