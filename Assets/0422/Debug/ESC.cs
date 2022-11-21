using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ESC : MonoBehaviour
{
    #region シングルトン

    private static ESC instance;

    public static ESC Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (ESC)FindObjectOfType(typeof(ESC));

                if (instance == null)
                {
                    Debug.LogError("FadeManager Instance nothing");
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    #endregion

    Keyboard key;
    void Update()
    {
        key = Keyboard.current;

        DebugCommand();
    }

    void DebugCommand()
    {
        //タイトル
        if (key.numpad1Key.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Title");
        }

        //メニュー
        if (key.numpad2Key.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Menu");
        }

        //チュートリアル
        if (key.numpad3Key.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Tutorial");
        }

        //メインゲーム
        if (key.numpad4Key.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Main");
        }

        //リザルト
        if(key.numpad5Key.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Result");
        }

        //ストーリーメニュー
        if(key.numpad6Key.wasPressedThisFrame)
        {
            SceneManager.LoadScene("StoryMenu");
        }

        //ストーリー
        if(key.numpad7Key.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Story");
        }
        
        //ストーリーバトル
        if(key.numpad8Key.wasPressedThisFrame)
        {
            SceneManager.LoadScene("StoryBattle");
        }

        //ゲーム終了
        if (key.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
        }
    }
}
