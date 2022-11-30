using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenuSystem : MonoBehaviour
{
    //PauseNowを取得してUpdateでif

    enum menu
    {
        resume = 0,
        retry,
        backmenu,

        ENUMEND
    }

    Gamepad gamepad;
    //カーソル移動
    bool selectLock;
    //多重クリック防止のbool
    bool onButton;

    //フェード
    [SerializeField]
    Fade fadeScript;
    //ボタン画像の読み込み
    [SerializeField]
    GameObject[] button_Images = new GameObject[(int)menu.ENUMEND];
    //ボタン画像を置いてあるパネルや画像
    [SerializeField]
    GameObject PauseImage;
    //オーディオ
    [SerializeField]
    AudioSource audioSource;
    public AudioClip selectSound;
    //選択中のボタン
    int nowSelecting;
    //直前に選択したボタン
    int prevSelecting;
    //ポーズ中か
    bool ispauseNow;

    void Start()
    {
        selectLock = false;
        onButton = false;
        ispauseNow = false;
    
        //初期位置
        nowSelecting = 0;
        prevSelecting = nowSelecting;
        audioSource = GetComponent<AudioSource>();
        PauseImage.SetActive(false);
    }

    void Update()
    {

        for (int i = 0; i < Gamepad.all.Count; ++i)
        {
            gamepad = Gamepad.all[i];
            PauseSystem();
            //メニューを表示している場合のみ
            if (ispauseNow)
            {
                SelectSystem();
                onClickAction();
            }
        }
        //決定押されていない　＆　上入力なし　＆　下入力なし
        if (!gamepad.buttonSouth.wasPressedThisFrame &&
            gamepad.leftStick.ReadValue().y <= 0.1f &&
            gamepad.leftStick.ReadValue().y >= -0.1f
            )
        {
            //ロック解除
            selectLock = false;
        }

        //選択中のボタンが切り替わったら
        if (nowSelecting != prevSelecting)
        {
            //直前のボタンを小さく
            button_Images[prevSelecting].GetComponent<Animator>().SetBool("selected", false);
            //選択中を大きく
            button_Images[nowSelecting].GetComponent<Animator>().SetBool("selected", true);
            //効果音再生
            audioSource.PlayOneShot(selectSound);
            //直前の選択を更新
            prevSelecting = nowSelecting;
        }
        //フェードが終わったらメニューに戻す
        if (fadeScript.FadeOut())
        {
            SceneManager.LoadScene("Menu");
        }

    }

    void SelectSystem()
    {
        //ロックがかかっていない場合
        if (selectLock == false)
        {
            //下入力
            if (gamepad.leftStick.ReadValue().y < -0.1f)
            {
                nowSelecting++;
                //最大値を超えたら0にする
                if (nowSelecting >= (int)menu.ENUMEND)
                    nowSelecting = 0;
                //ロックをかける
                selectLock = true;
            }
            //上入力
            else if (gamepad.leftStick.ReadValue().y > 0.1f)
            {
                nowSelecting--;
                if (nowSelecting < 0)
                    nowSelecting = (int)menu.ENUMEND - 1;
                //ロックをかける
                selectLock = true;
            }
        }
    }
    void onClickAction()
    {
        //連打防止用
        if (!onButton)
        {
            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
                //一番下を選択中
                switch (nowSelecting)
                {
                    case (int)menu.resume:

                        Time.timeScale = 1.0f;
                        break;
                    case (int)menu.backmenu:

                        selectLock = true;
                        onButton = true;
                        fadeScript.fademode = true;
                        Time.timeScale = 1.0f;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    void PauseSystem()
    {
        //押したらポーズ切り替え
        if (gamepad.buttonNorth.wasPressedThisFrame)
        {
            ispauseNow = !ispauseNow;
            

            //ポーズ中であれば
            if (ispauseNow)
            {
                //audioSource.PlayOneShot(se);
                PauseImage.SetActive(true);
                nowSelecting = 0;
                button_Images[nowSelecting].GetComponent<Animator>().SetBool("selected", true);
                Time.timeScale = 0.0f;

            }
            //非ポーズ中であれば
            else
            {
                //audioSource.PlayOneShot(se_);
                for (int i = 0; i < button_Images.Length; ++i)
                {
                    button_Images[i].GetComponent<Animator>().SetBool("selected", false);
                }
                print("falseNow");
                PauseImage.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
    }
}
