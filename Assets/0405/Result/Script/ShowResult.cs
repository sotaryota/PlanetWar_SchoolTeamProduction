using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowResult : MonoBehaviour
{
    [SerializeField]
    private DataCopy copydata;
    [SerializeField]
    private GameObject param_UI;
    [SerializeField]
    private Text powerText, defenceText, speedText, winnerText;

    [SerializeField]
    private BackTitle backTitle;
    public AudioSource audioSource;
    public AudioClip[] audioClip;

    [Header("ステータスデータが保存されているオブジェクト名")]
    [SerializeField]
    private string objectName;

    void Start()
    {
        copydata = GameObject.Find(objectName).GetComponent<DataCopy>();

        //0->Player1 1->Player2 2->Draw
        int winPlayerNum = 2;

        if (copydata.playerData[0].HP > copydata.playerData[1].HP)
        {
            winPlayerNum = 0;
        }
        else if (copydata.playerData[0].HP < copydata.playerData[1].HP)
        {
            winPlayerNum = 1;
        }
        else { winPlayerNum = 2; }

        if (winPlayerNum == 0 || winPlayerNum == 1)
        {
            audioSource.PlayOneShot(audioClip[winPlayerNum]);
            PowerTextUpdate(copydata.playerData[winPlayerNum].atk);
            defenceTextUpdate(copydata.playerData[winPlayerNum].def);
            speedTextUpdate(copydata.playerData[winPlayerNum].spd);
            WinPlayerTextUpdate(winPlayerNum);
        }
        //引き分けの時の表示
        else
        {
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
        //勝ったほうのプレイヤーのRotation+=5;
        if (!backTitle.enabled)
        {
            backTitle.enabled = true;
        }
    }

    private void PowerTextUpdate(float power)
    {
        powerText.text = power.ToString();
    }
    private void defenceTextUpdate(float defence)
    {
        defenceText.text = defence.ToString();
    }
    private void speedTextUpdate(float speed)
    {
        speedText.text = speed.ToString();
    }
    private void WinPlayerTextUpdate(int winplayernum)
    {
        winnerText.text = (winplayernum + 1).ToString() + "P WIN";
    }
    private void WinPlayerTextUpdate()
    {
        winnerText.text = "DRAW";
    }
}
