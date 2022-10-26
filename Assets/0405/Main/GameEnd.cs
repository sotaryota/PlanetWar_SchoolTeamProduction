using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    enum EndMode
    {
        p1Die,
        p2Die,
        timeUp,

        Non
    }
    EndMode mode;

    [SerializeField]
    private Fade fadeScript;

    [Header("�v���C���[�Q��")]
    [SerializeField]
    private GameObject player1Object;
    [SerializeField]
    private GameObject player2Object;

    public PlayerStatus ps1;
    public PlayerStatus ps2;
    private float pLifeSave1 = 0, pLifeSave2 = 0;

    [SerializeField] 
    PlayerAnimManeger[] playerAnimator = new PlayerAnimManeger[2];

    public Timer timer;

    [SerializeField]
    GameObject finishText;
    [SerializeField]
    GameObject timeupText;
    float stopTime;

    [SerializeField]
    GameObject[] finishCameraPos;

    [SerializeField]
    Camera[] camera;

    bool istimeup = true;

    void Start()
    {
        ps1 = player1Object.GetComponent<PlayerStatus>();
        ps2 = player2Object.GetComponent<PlayerStatus>();

        istimeup = true;
        mode = EndMode.Non;
    }
    void Update()
    {
        if (player1Object)
        {
            if (ps1.GetHp() <= 0 && (mode == EndMode.Non || mode == EndMode.p1Die))
            {
                camera[1].rect = new Rect(0, 0, 0, 0);
                camera[0].rect = new Rect(0, 0, 1, 1);

                //�J���������o�p�̈ʒu��
                camera[0].transform.position = finishCameraPos[0].transform.position;
                //�J������180�x��]������
                Vector3 pRotate = player1Object.transform.rotation.eulerAngles;
                camera[0].gameObject.transform.rotation = Quaternion.Euler(28, pRotate.y, 0);
                camera[0].gameObject.transform.rotation = Quaternion.Euler(28, pRotate.y + 180, 0);

                finishText.SetActive(true);
                GameEndText(true);
                mode = EndMode.p1Die;
            }
        }
        if (player2Object)
        {
            if (ps2.GetHp() <= 0 && (mode == EndMode.Non || mode == EndMode.p2Die))
            {
                camera[0].rect = new Rect(0, 0, 0, 0);
                camera[1].rect = new Rect(0, 0, 1, 1);

                //�J���������o�p�̈ʒu��
                camera[1].transform.position = finishCameraPos[1].transform.position;
                //�J������180�x��]������
                Vector3 pRotate = player2Object.transform.rotation.eulerAngles;
                camera[1].gameObject.transform.rotation = Quaternion.Euler(28, pRotate.y, 0);
                camera[1].gameObject.transform.rotation = Quaternion.Euler(28, pRotate.y + 180, 0);
              
                finishText.SetActive(true);
                GameEndText(true);
                mode = EndMode.p2Die;
            }
        }
        if (timer)
        {
            if (timer.getTime() <= 0 && (mode == EndMode.Non || mode == EndMode.timeUp))
            {              
                stopTime += Time.unscaledDeltaTime;
                //HP�������������͏����A�j���[�V�������A���Ȃ������ق��͔s�k�A�j���[�V�������Ăяo��
                if (istimeup)
                {
                    istimeup = false;
                    if (ps1.GetHp() > ps2.GetHp())
                    {
                        playerAnimator[0].PlayAnimWin();
                        playerAnimator[1].PlayAnimLose();
                    }
                    else
                    {
                        playerAnimator[1].PlayAnimWin();
                        playerAnimator[0].PlayAnimLose();
                    }
                }

                timeupText.SetActive(true);
                GameEndText(false);
                mode = EndMode.timeUp;
            }
        }
    }

    private void GameEndText(bool playerDie)
    {
        stopTime += Time.unscaledDeltaTime;
        if (playerDie)
        {
            //�v���C���[�����񂾍�
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
        else
        {
            //�^�C���A�b�v������
            if (stopTime >= 3.5f)
            {
                Fade();
            }
            else if (stopTime > 0.2f)
            {
                Time.timeScale = 1f;
            }
            else
            {
                Time.timeScale = 0;
            }
        }
    }

    private void Fade()
    {
        if (fadeScript.fademode == false)
        {
            Debug.Log("fade");
            fadeScript.fademode = true;
        }

        if (fadeScript.FadeOut())
        {
            SceneManager.LoadScene("Result");
        }
    }
}
