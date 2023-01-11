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

public class GameoverMenu : MonoBehaviour
{
    public enum menu
    {
        retry = 0,
        backmenu
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
        public string nextScene;
    }
    [SerializeField]
    private Button[] buttonClass = new Button[1];

    //ボタン画像を置いてあるパネルや画像
    [SerializeField]
    GameObject PausePanel;
    //オーディオ
    [SerializeField]
    AudioSource audioSource;
    public AudioClip selectSound;
    public AudioClip pushSound;
    //選択中のボタン
    int nowSelecting;
    //直前に選択したボタン
    int prevSelecting;
    //ポーズ中か
    bool ispauseNow;
    //ポーズメニューを展開できる状態かどうか
    /*public void SetCanPause(bool value)
    {
        canPause = value;
    }*/

    void Start()
    {
        selectLock = false;
        onButton = false;
        ispauseNow = false;
        nextscene = "";

        //初期位置
        nowSelecting = 0;
        prevSelecting = nowSelecting;
        audioSource = GetComponent<AudioSource>();
        PausePanel.SetActive(false);
    }

    void Update()
    {
        gamepad = Gamepad.current;



        //メニューを表示している場合のみ
        if (ispauseNow)
        {
            SelectSystem();
            onClickAction();
        }

        //決定押されていない　＆　上入力なし　＆　下入力なし
        if (!gamepad.buttonSouth.wasPressedThisFrame &&
            gamepad.leftStick.ReadValue().x <= 0.1f &&
            gamepad.leftStick.ReadValue().x >= -0.1f
            )
        {
            //ロック解除
            selectLock = false;
        }

        //選択中のボタンが切り替わったら
        if (nowSelecting != prevSelecting)
        {
            //直前のボタンを小さく
            buttonClass[prevSelecting].buttonImage.GetComponent<Animator>().SetBool("selected", false);
            //選択中を大きく
            buttonClass[nowSelecting].buttonImage.GetComponent<Animator>().SetBool("selected", true);
            //効果音再生
            audioSource.PlayOneShot(selectSound);
            //直前の選択を更新
            prevSelecting = nowSelecting;
        }
        //フェードが終わったらメニューに戻す
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
            if (gamepad.leftStick.ReadValue().x < -0.1f)
            {
                nowSelecting++;
                //最大値を超えたら0にする
                if (nowSelecting >= buttonClass.Length)
                    nowSelecting = 0;
                //ロックをかける
                selectLock = true;
            }
            //上入力
            else if (gamepad.leftStick.ReadValue().x > 0.1f)
            {
                nowSelecting--;
                if (nowSelecting < 0)
                    nowSelecting = buttonClass.Length - 1;
                //ロックをかける
                selectLock = true;
            }
        }
    }

    //クリックしたときの挙動
    void onClickAction()
    {
        //連打防止用
        if (!onButton)
        {
            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
                audioSource.PlayOneShot(pushSound);
                switch (buttonClass[nowSelecting].menuCell)
                {
                    case menu.retry:
                        selectLock = true;
                        onButton = true;
                        fadeScript.fademode = true;
                        nextscene = buttonClass[nowSelecting].nextScene;
                        break;
                    case menu.backmenu:
                        selectLock = true;
                        onButton = true;
                        fadeScript.fademode = true;
                        nextscene = buttonClass[nowSelecting].nextScene;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void PauseSystem()
    {
        ispauseNow = true;
        PausePanel.SetActive(true);
        nowSelecting = 0;
        buttonClass[nowSelecting].buttonImage.GetComponent<Animator>().SetBool("selected", true);
    }

    //ポーズ中かを返す
    public bool pausejudge()
    {
        return ispauseNow;
    }
}
