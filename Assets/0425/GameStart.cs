using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < playerMove.Length; ++i)
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
        }
        startText.SetActive(false);
    }
}
