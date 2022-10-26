using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class ShowResult : MonoBehaviour
{
    [SerializeField]
    private DataCopy copydata;
    [SerializeField]
    private GameObject param_UI;
    [SerializeField]
    private Text powerText, defenceText, speedText;
    [SerializeField]
    private GameObject pleaseStart;

    [SerializeField]
    private BackTitle backTitle;
    public AudioSource audioSource;
    public AudioClip[] audioClip;

    //0->Player1 1->Player2 2->Draw
    int winPlayerNum = 2;

    [Header("ステータスデータが保存されているオブジェクト名")]
    [SerializeField]
    private string objectName;
    
    [SerializeField]
    GameObject maincamera;
    [SerializeField]
    GameObject player1, player2;

    //プレイヤーアニメーション
    Animator[] animator = new Animator[2];

    //カメラアニメーション
    [SerializeField]
    TimelineAsset[] timelines;
    PlayableDirector director;

    //勝者を表示する用のImage
    [SerializeField]
    GameObject[] winnerTextImage = new GameObject[3];

    float TimeCnt;
    void Start()
    {
        copydata = GameObject.Find(objectName).GetComponent<DataCopy>();

        TimeCnt = 0f;

        director = maincamera.GetComponent<PlayableDirector>();
        animator[0] = player1.GetComponent<Animator>();
        animator[1] = player2.GetComponent<Animator>();

        pleaseStart.SetActive(false);

        for(int i = 0; i < winnerTextImage.Length; ++i)
        {
            winnerTextImage[i].SetActive(false);
        }

        //判定
        if (copydata.playerData[0].HP > copydata.playerData[1].HP)
        {
            winPlayerNum = 0;
        }
        else if (copydata.playerData[0].HP < copydata.playerData[1].HP)
        {
            winPlayerNum = 1;
        }
        else { winPlayerNum = 2; }


        //処理
        if (winPlayerNum == 0 || winPlayerNum == 1)
        {
            //勝利プレイヤーのみを表示・位置指定 
            if (winPlayerNum == 0)
            {
                player1.SetActive(true);
                player2.SetActive(false);
                player1.transform.position = Vector3.zero;
            }
            else if(winPlayerNum == 1)
            {
                player2.SetActive(true);
                player1.SetActive(false);
                player2.transform.position = Vector3.zero;
            }
            //勝利アニメーション(プレイヤー)をプレイ
            animator[winPlayerNum].SetTrigger("win");
            //勝利アニメーション(カメラ)をプレイ
            director.playableAsset = timelines[0];
            director.Play(timelines[0]);

            audioSource.PlayOneShot(audioClip[winPlayerNum]);
            PowerTextUpdate(copydata.playerData[winPlayerNum].atk);
            defenceTextUpdate(copydata.playerData[winPlayerNum].def);
            speedTextUpdate(copydata.playerData[winPlayerNum].spd);
            WinPlayerTextUpdate(winPlayerNum);
        }
        //引き分けの時の表示
        else
        {
            //プレイヤーをどちらも表示・位置指定
            player1.SetActive(true);
            player2.SetActive(true);
            player1.transform.position = Vector3.zero;
            player2.transform.position = new Vector3(6,0,0);
            //引き分けアニメーション(プレイヤー)をプレイ
            animator[0].SetTrigger("draw");
            animator[1].SetTrigger("draw");
            //引き分けアニメーション(カメラ)をプレイ
            director.playableAsset = timelines[1];
            director.Play(timelines[1]);

            param_UI.SetActive(false);
            WinPlayerTextUpdate();
        }

        backTitle = GetComponent<BackTitle>();
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();

        Destroy(copydata.gameObject);
    }

    void Update()
    {
        if (!backTitle.enabled)
        {
            backTitle.enabled = true;
        }

        //勝者表示
        TimeCnt += Time.deltaTime;
        if (winPlayerNum == 0 || winPlayerNum == 1)
        {
            if (TimeCnt > 6)
            {
                pleaseStart.SetActive(true);
            }
        }
        else
        {
            if (TimeCnt > 2)
            {
                pleaseStart.SetActive(true);
            }
        }
    }

    private void PowerTextUpdate(float power)
    {
        powerText.text = power.ToString("F0");
    }
    private void defenceTextUpdate(float defence)
    {
        defenceText.text = defence.ToString("F0");
    }
    private void speedTextUpdate(float speed)
    {
        speedText.text = speed.ToString("F0");
    }
    private void WinPlayerTextUpdate(int winplayernum)
    {
        winnerTextImage[winplayernum].SetActive(true);
    }
    private void WinPlayerTextUpdate()
    {
        winnerTextImage[2].SetActive(true);
    }
}
