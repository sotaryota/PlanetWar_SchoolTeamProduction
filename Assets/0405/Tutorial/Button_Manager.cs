using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Button_Manager : MonoBehaviour
{
    Gamepad gamepad;
    Gamepad gamepadInputed;
    bool selectLock;

    //フェード
    [SerializeField]
    Fade fadeScript;
    //説明画像の位置
    [SerializeField]
    Image TutorialImage;
    //表示する画像の読み込み
    [SerializeField]
    Sprite[] selectSprites = new Sprite[5];
    //ボタン画像の読み込み
    [SerializeField]
    GameObject[] button_Images = new GameObject[1];
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
        nowSelecting = 0;
        prevSelecting = nowSelecting;
        audioSource = GetComponent<AudioSource>();
        button_Images[0].GetComponent<Animator>().SetBool("selected", true);
        TutorialImage.sprite = selectSprites[0];
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Gamepad.all.Count; ++i)
        {
            gamepad = Gamepad.all[i];
            StageSelectSystem();
            BackMenu();
        }

        //ロック解除の判定
        if (gamepadInputed != null)
        {
            //決定押されていない　＆　上入力なし　＆　下入力なし
            if (!gamepadInputed.buttonSouth.wasPressedThisFrame &&
                gamepadInputed.leftStick.ReadValue().y <= 0.1f &&
                gamepadInputed.leftStick.ReadValue().y >= -0.1f
                )
            {
                //ロック解除
                selectLock = false;
                gamepadInputed = null;
            }
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
            //イメージ変更
            TutorialImage.sprite = selectSprites[nowSelecting];
        }

        //フェードが終わったらメニューに戻す
        if (fadeScript.FadeOut())
        {
            SceneManager.LoadScene("Menu");
        }
    }

    void StageSelectSystem()
    {
        //ロックがかかっていない場合
        if (selectLock == false)
        {
            if (gamepad.leftStick.ReadValue().y < -0.1f)
            {
                //上を選択
                nowSelecting++;
                //最大値を超えたら0にする
                if (nowSelecting > button_Images.Length - 1)
                    nowSelecting = 0;
                //ロックをかける
                selectLock = true;
                gamepadInputed = gamepad;
            }
            //下入力
            else if (gamepad.leftStick.ReadValue().y > 0.1f)
            {
                //下を選択
                nowSelecting--;
                if (nowSelecting < 0)
                    nowSelecting = button_Images.Length - 1;
                //ロックをかける
                selectLock = true;
                gamepadInputed = gamepad;
            }
        }
    }

    void BackMenu()
    {
        if (nowSelecting == button_Images.Length - 1)
        {
            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
                selectLock = true;
                fadeScript.fademode = true;
            }
        }
    }
}
