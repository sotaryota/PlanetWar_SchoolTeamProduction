using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public enum SelectMenu
    {
        Start,      //スタート
        Tutorial,   //チュートリアル
        Title,      //タイトル
        End,        //ゲーム終了
    };

    [System.Serializable]
    public class MenuData
    {
        public GameObject menuImage;  //表示する画像
        public string sceneName; //移行したいシーンの名前
    };

    private Gamepad gamepad;
    [SerializeField] 
    private CameraManager cameraManager;
    [SerializeField]
    private float lockValue;
    public MenuData[] menuDatas;
    public SelectMenu nowSelect;
    public SelectMenu beforeSelect;

    private void Start()
    {
        nowSelect = SelectMenu.Start;
        beforeSelect = nowSelect;
    }

    private void Update()
    {
        if(gamepad == null)
        {
            gamepad = Gamepad.current;
        }

        SceneChange(menuDatas[(int)nowSelect].sceneName);
    }
    /// <summary>
    /// メニューの切り替えをしていない時
    /// ボタンを押すと引数の名前のシーンに移行
    /// Endの場合はゲーム終了
    /// </summary>
    /// <param name="sceneName">シーン名</param>
    public void SceneChange(string sceneName)
    {
        if(cameraManager.buttonLock)
        {
            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
                if (sceneName != "End")
                {
                    SceneManager.LoadScene(sceneName);
                }
                else
                {
                    Application.Quit();
                }
            }
        }
    }
}
