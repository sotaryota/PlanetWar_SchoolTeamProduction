using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

//ポーズさせたいシーンにこれを置く。
//フェードくん、ボタンにしたい画像、そのボタンをまとめるオブジェクト等をアタッチすればおｋ
//ボタンにはアニメーションを持たせること。なお、「UpdateMode」を「UnscaledTime」にすること！
//そのシーンのマネージャーのUpdateに下記を置いとかないと動いたりするので注意

//PauseMenuSystem pauseMenuSystem;
//if (pauseMenuSystem.pausejudge()) { }

//SetCanPause(_bool_); でポーズ不可にできる。
//会話が始まるときにFalseを送り、終わったらTrueを送る等

public class PauseSystem_Boss : PauseMenuSystem
{
    //クリックしたときの挙動
    protected override void onClickAction()
    {
        //連打防止用
        if (!onButton)
        {
            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
                audioSource.PlayOneShot(pushSound);
                switch (buttonClass[nowSelecting].menuCell)
                {
                    case menu.resume:
                        PausePanel.SetActive(false);
                        Time.timeScale = 1.0f;
                        ispauseNow = false;
                        break;
                    case menu.retry:
                        selectLock = true;
                        onButton = true;
                        fadeScript.fademode = true;
                        Time.timeScale = 1.0f;
                        nextscene = "StoryBoss";
                        break;
                    case menu.back:
                        selectLock = true;
                        onButton = true;
                        fadeScript.fademode = true;
                        Time.timeScale = 1.0f;
                        nextscene = "Story";
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
