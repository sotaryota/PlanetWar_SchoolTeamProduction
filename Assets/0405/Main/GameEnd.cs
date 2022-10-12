using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    [SerializeField]
    private Fade fadeScript;

    [Header("プレイヤー参照")]
    [SerializeField]
    private GameObject player1Object;
    [SerializeField]
    private GameObject player2Object;

    public PlayerStatus ps1;
    public PlayerStatus ps2;

    public Timer timer;
    void Start()
    {
        ps1 = player1Object.GetComponent<PlayerStatus>();
        ps2 = player2Object.GetComponent<PlayerStatus>();
    }
    void Update()
    {
        if (player1Object)
        {
            if (ps1.GetHp() <= 0)
            {
                //Fade();
            }
        }
        if (player2Object)
        {
            if (ps2.GetHp() <= 0)
            {
                Fade();
            }
        }
        if (timer)
        {
            if (timer.getTime() <= 0)
            {
                Fade();
            }
        }
    }
    private void Fade()
    {
        fadeScript.fademode = true;
        if (fadeScript.FadeOut())
        {
            SceneManager.LoadScene("Result");
        }
    }
}
