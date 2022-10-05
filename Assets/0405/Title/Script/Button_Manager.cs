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
    private string[] nextSceneName = new string[3];

    [SerializeField]
    OnClickBase[] selectButtonScripts = new OnClickBase[3];


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
                selectButtonScripts[nowSelecting].OnClick();
                //完全ロック
                selectLock = true;
            }
            //上入力
            else if (gamepad.leftStick.ReadValue().y < -0.1f)
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
}
