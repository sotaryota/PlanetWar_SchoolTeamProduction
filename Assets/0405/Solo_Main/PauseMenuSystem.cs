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

public class PauseMenuSystem : MonoBehaviour
{
    public enum menu
    {
        resume = 0,
        retry,
        back
    }

    protected Gamepad gamepad;
    //カーソル移動
    protected bool selectLock;
    //多重クリック防止のbool
    protected bool onButton;
    //会話中にポーズ不可にする
    [SerializeField] PlayerStatus_Solo playerStatus_Solo;
    //フェード
    [SerializeField]
    protected Fade fadeScript;
    //移行するシーンを保存する場所
    protected string nextscene;
    //ボタン画像の読み込み
    [System.Serializable]
    public class Button
    {
        public GameObject buttonImage;
        public menu menuCell;
    }
    [SerializeField]
    protected Button[] buttonClass = new Button[1];

    //ボタン画像を置いてあるパネルや画像
    [SerializeField]
    protected GameObject PausePanel;
    //オーディオ
    [SerializeField]
    protected AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip selectSound;
    public AudioClip pushSound;
    //選択中のボタン
    protected int nowSelecting;
    //直前に選択したボタン
    protected int prevSelecting;
    //ポーズ中か
    protected bool ispauseNow;
    //ポーズメニューを展開できる状態かどうか
    protected bool canPause;
    public void SetCanPause(bool value)
    {
        canPause = value;
    }

    void Start()
    {
        selectLock = false;
        onButton = false;
        ispauseNow = false;
        canPause = true;
        nextscene = "";

        //初期位置
        nowSelecting = 0;
        prevSelecting = nowSelecting;
        audioSource = GetComponent<AudioSource>();
        //buttonClass[0].buttonImage.GetComponent<Animator>().SetBool("selected", true);
        PausePanel.SetActive(false);
    }

    void Update()
    {
        gamepad = Gamepad.current;
        if (playerStatus_Solo != null)
        {
            if (playerStatus_Solo.GetState() == PlayerStatus_Solo.State.Talking)
            {
                canPause = false;
            }
            else { canPause = true; }
        }
        if (canPause)
        {
            PauseSystem();
        }

        //メニューを表示している場合のみ
        if (ispauseNow)
        {
            SelectSystem();
            onClickAction();
        }

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

    //クリックしたときの挙動
    protected virtual void onClickAction()
    {
        //連打防止用
        if (!onButton)
        {
            if (gamepad.buttonEast.wasPressedThisFrame)
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
                        nextscene = "StoryBattle";
                        break;
                    case menu.back:
                        selectLock = true;
                        onButton = true;
                        fadeScript.fademode = true;
                        Time.timeScale = 1.0f;
                        nextscene = "StoryMenu";
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
        if (gamepad.startButton.wasPressedThisFrame)
        {
            ispauseNow = !ispauseNow;


            //ポーズ中になったら
            if (ispauseNow)
            {
                audioSource.PlayOneShot(openSound);
                PausePanel.SetActive(true);
                nowSelecting = 0;
                buttonClass[nowSelecting].buttonImage.GetComponent<Animator>().SetBool("selected", true);
                Time.timeScale = 0.0f;

            }
            //非ポーズ中になったら
            else
            {
                //audioSource.PlayOneShot(Sound_);
                for (int i = 0; i < buttonClass.Length; ++i)
                {
                    buttonClass[i].buttonImage.GetComponent<Animator>().SetBool("selected", false);
                }
                PausePanel.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
    }

    //ポーズ中かを返す
    public bool pausejudge()
    {
        return ispauseNow;
    }
}
