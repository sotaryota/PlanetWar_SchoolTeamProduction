using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Solo_Menu_System : MonoBehaviour
{
    public enum menu
    {
        NewGame = 0,
        Continue,
        BackMenu
    }

    Gamepad gamepad;
    //カーソル移動
    bool selectLock;
    //多重クリック防止のbool
    bool onButton;

    //フェード
    [SerializeField]
    Fade fadeScript;
    //移行するシーンを保存する場所
    string nextscene;
    //ボタン画像の読み込み
    [System.Serializable]
    public class Button
    {
        public GameObject buttonImage;
        public menu menuCell;
    }
    [SerializeField]
    private Button[] buttonClass = new Button[1];

    //オーディオ
    [SerializeField]
    AudioSource audioSource;
    public AudioClip selectSound;
    public AudioClip pushSound;
    //選択中のボタン
    int nowSelecting;
    //直前に選択したボタン
    int prevSelecting;

    void Start()
    {
        selectLock = false;
        onButton = false;
        nextscene = "";

        //初期位置
        nowSelecting = 0;
        prevSelecting = nowSelecting;
        audioSource = GetComponent<AudioSource>();
        buttonClass[0].buttonImage.GetComponent<Animator>().SetBool("selected", true);
    }

    void Update()
    {
        gamepad = Gamepad.current;
        SelectSystem();
        onClickAction();
        //決定押されていない　＆　上入力なし　＆　下入力なし
        if (!gamepad.buttonEast.wasPressedThisFrame &&
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
            buttonClass[prevSelecting].buttonImage.GetComponent<Animator>().SetBool("selected", false);
            //選択中のボタンのアニメーション開始
            buttonClass[nowSelecting].buttonImage.GetComponent<Animator>().SetBool("selected", true);
            //効果音再生
            audioSource.PlayOneShot(selectSound);
            //直前の選択を更新
            prevSelecting = nowSelecting;
        }

        //フェードが終わったらシーン遷移
        if (fadeScript.FadeOut())
        {
            SceneManager.LoadScene(nextscene);
        }
    }

    void SelectSystem()
    {
        //何もロックがかかっていない場合
        if (selectLock == false && onButton == false)
        {
            //下入力
            if (gamepad.leftStick.ReadValue().y < -0.1f)
            {
                nowSelecting++;
                //最大値を超えたら0にする
                if (nowSelecting >= buttonClass.Length)
                    nowSelecting = 0;
                //ロックをかける
                selectLock = true;
            }
            //上入力
            else if (gamepad.leftStick.ReadValue().y > 0.1f)
            {
                nowSelecting--;
                if (nowSelecting < 0)
                    nowSelecting = buttonClass.Length - 1;
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
            if (gamepad.buttonEast.wasPressedThisFrame)
            {
                //操作を止めて
                selectLock = true;
                onButton = true;
                //決定音
                audioSource.PlayOneShot(pushSound);
                //選んでいる項目別に挙動を変える
                switch (buttonClass[nowSelecting].menuCell)
                {
                    case menu.NewGame:
                        fadeScript.fademode = true;
                        nextscene = "Opening";
                        break;
                    case menu.Continue:
                        fadeScript.fademode = true;
                        nextscene = "Story";
                        break;
                    case menu.BackMenu:
                        fadeScript.fademode = true;
                        nextscene = "Menu";
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
