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

    [Header("�X�e�[�^�X�f�[�^���ۑ�����Ă���I�u�W�F�N�g��")]
    [SerializeField]
    private string objectName;
    
    [SerializeField]
    GameObject maincamera;
    [SerializeField]
    GameObject player1, player2;

    //�v���C���[�A�j���[�V����
    Animator[] animator = new Animator[2];

    //�J�����A�j���[�V����
    [SerializeField]
    TimelineAsset[] timelines;
    PlayableDirector director;

    //���҂�\������p��Image
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

        //����
        if (copydata.playerData[0].HP > copydata.playerData[1].HP)
        {
            winPlayerNum = 0;
        }
        else if (copydata.playerData[0].HP < copydata.playerData[1].HP)
        {
            winPlayerNum = 1;
        }
        else { winPlayerNum = 2; }


        //����
        if (winPlayerNum == 0 || winPlayerNum == 1)
        {
            //�����v���C���[�݂̂�\���E�ʒu�w�� 
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
            //�����A�j���[�V����(�v���C���[)���v���C
            animator[winPlayerNum].SetTrigger("win");
            //�����A�j���[�V����(�J����)���v���C
            director.playableAsset = timelines[0];
            director.Play(timelines[0]);

            audioSource.PlayOneShot(audioClip[winPlayerNum]);
            PowerTextUpdate(copydata.playerData[winPlayerNum].atk);
            defenceTextUpdate(copydata.playerData[winPlayerNum].def);
            speedTextUpdate(copydata.playerData[winPlayerNum].spd);
            WinPlayerTextUpdate(winPlayerNum);
        }
        //���������̎��̕\��
        else
        {
            //�v���C���[���ǂ�����\���E�ʒu�w��
            player1.SetActive(true);
            player2.SetActive(true);
            player1.transform.position = Vector3.zero;
            player2.transform.position = new Vector3(6,0,0);
            //���������A�j���[�V����(�v���C���[)���v���C
            animator[0].SetTrigger("draw");
            animator[1].SetTrigger("draw");
            //���������A�j���[�V����(�J����)���v���C
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

        //���ҕ\��
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
