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

    [SerializeField]
    GameObject finishText;
    float stopTime;

    [SerializeField]
    GameObject[] finishCameraPos;

    [SerializeField]
    Camera[] camera;

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
                camera[1].rect = new Rect(0, 0, 0, 0);
                camera[0].rect = new Rect(0, 0, 1, 1);

                //カメラを演出用の位置に
                camera[0].transform.position = finishCameraPos[0].transform.position;
                //カメラを180度回転させる
                Vector3 pRotate = player1Object.transform.rotation.eulerAngles;
                camera[0].gameObject.transform.rotation = Quaternion.Euler(28, pRotate.y, 0);
                camera[0].gameObject.transform.rotation = Quaternion.Euler(28, pRotate.y + 180, 0);

                finishText.SetActive(true);

                stopTime += Time.unscaledDeltaTime;
                Time.timeScale = 0;

                if (stopTime < 0.2)
                {
                    Time.timeScale = 0;
                }
                else if (stopTime > 1.5 && stopTime < 4)
                {
                    Time.timeScale = 0.2f;

                }
                else if (stopTime >= 4)
                {
                    Time.timeScale = 1;
                }

                Fade();
            }
        }
        if (player2Object)
        {
            if (ps2.GetHp() <= 0)
            {
                camera[0].rect = new Rect(0, 0, 0, 0);
                camera[1].rect = new Rect(0, 0, 1, 1);

                //カメラを演出用の位置に
                camera[1].transform.position = finishCameraPos[1].transform.position;
                //カメラを180度回転させる
                Vector3 pRotate = player2Object.transform.rotation.eulerAngles;
                camera[1].gameObject.transform.rotation = Quaternion.Euler(28, pRotate.y, 0);
                camera[1].gameObject.transform.rotation = Quaternion.Euler(28, pRotate.y + 180, 0);
              
                finishText.SetActive(true);

                stopTime += Time.unscaledDeltaTime;
                Time.timeScale = 0;

                if (stopTime < 0.2)
                {
                    Time.timeScale = 0;
                }
                else if (stopTime > 1.5 && stopTime < 4)
                {
                    Time.timeScale = 0.2f;
                   
                }
                else if(stopTime >= 4)
                {
                    Time.timeScale = 1;
                }
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
