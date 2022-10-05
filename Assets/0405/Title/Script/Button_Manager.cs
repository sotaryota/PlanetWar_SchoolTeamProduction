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

    [SerializeField]
    Start_Button start_Button;
    Tutorial_Button tutorial_Button;
    End_Button end_Button;

    interface ButtonPressed
    {
        public abstract void Pressed();
    }
    class Gamestart : ButtonPressed
    {
        public Start_Button start_Button { get; private set; }

        public void Pressed()
        {
            start_Button.OnClick();
        }
    }
    class Tutorial : ButtonPressed
    {
        public Tutorial_Button tutorial_Button { get; private set; }
        public void Pressed()
        {
            tutorial_Button.OnClick();
        }
    }
    class End : ButtonPressed
    {
        public End_Button end_Button { get; private set; }
        public void Pressed()
        {
            end_Button.OnClick();
        }
    }

    ButtonPressed[] selectClass = new ButtonPressed[] { new Gamestart(), new Tutorial() };


    //ボタン画像の読み込み
    [SerializeField]
    GameObject[] button_Images = new GameObject[1];
    //オーディオ
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
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Gamepad.all.Count; ++i)
        {
            gamepad = Gamepad.all[i];
            StageSelectSystem();
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
        }
    }

    void StageSelectSystem()
    {
        //ロックがかかっていない場合
        if (selectLock == false)
        {
            //決定ボタン
            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
                //ステージ選択
                selectClass[nowSelecting].Pressed();
                //完全ロック
                selectLock = true;
            }
            //上入力
            else if (gamepad.leftStick.ReadValue().y > 0.1f)
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
            else if (gamepad.leftStick.ReadValue().y < -0.1f)
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
}
