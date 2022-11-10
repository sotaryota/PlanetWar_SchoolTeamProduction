using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Menu_System : MonoBehaviour
{
    //PauseNowを取得してUpdateでif

    enum menu
    {
        NewGame = 0,
        Continue,
        BackMenu,

        ENUMEND
    }

    Gamepad gamepad;
    //カーソル移動
    bool selectLock;
    //多重クリック防止のbool
    bool onButton;

    //フェード
    //[SerializeField]
    //Fade fadeScript;
    //ボタン画像の読み込み
    [Header("アニメーションさせるオブジェクト")]
    [SerializeField]
    GameObject[] button_Images = new GameObject[(int)menu.ENUMEND];
    //オーディオ
    [SerializeField]
    AudioSource audioSource;
    public AudioClip selectSound;
    //選択中のボタン
    int nowSelecting;
    //直前に選択したボタン
    int prevSelecting;

    void Start()
    {
        selectLock = false;
        onButton = false;

        //初期位置
        nowSelecting = 0;
        prevSelecting = nowSelecting;
        audioSource = GetComponent<AudioSource>();
        button_Images[0].GetComponent<Animator>().SetBool("selected", true);
    }

    void Update()
    {
        for (int i = 0; i < Gamepad.all.Count; ++i)
        {
            gamepad = Gamepad.all[i];
            SelectSystem();
            onClickAction();
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
            //直前のボタンのアニメーション終了処理
            button_Images[prevSelecting].GetComponent<Animator>().SetBool("selected", false);
            //選択中のボタンのアニメーション開始
            button_Images[nowSelecting].GetComponent<Animator>().SetBool("selected", true);
            //効果音再生
            audioSource.PlayOneShot(selectSound);
            //直前の選択を更新
            prevSelecting = nowSelecting;
        }
        //フェードが終わったらメニューに戻す
        /*if (fadeScript.FadeOut())
        {
            SceneManager.LoadScene("Menu");
        }*/
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
        //連打禁止用
        if (!onButton)
        {
            //押したら
            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
                //操作を止めて
                selectLock = true;
                onButton = true;

                //選んでいる項目別に挙動を変える
                switch (nowSelecting)
                {
                    case (int)menu.NewGame:
                        //fadeScript.fademode = true;
                        Time.timeScale = 1.0f;
                        break;
                    case (int)menu.Continue:
                        //fadeScript.fademode = true;
                        break;
                    case (int)menu.BackMenu:
                        //fadeScript.fademode = true;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
