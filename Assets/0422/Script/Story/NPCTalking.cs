using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NPCTalking : MonoBehaviour
{
    Gamepad gamepad;

    [Header("プレイヤー")]
    [SerializeField] GameObject player;

    [Header("カメラ")]
    [SerializeField] GameObject playerCamera;

    [Header("NPC")]
    public GameObject npc;                       //接触中のNPC

    [Header("スクリプト")]
    [SerializeField] PlayerStatus_Solo playerStatus;
    [SerializeField] PlayerDataManager playerData;
    private NPCDataManager npcData;

    [Header("キャンバス")]
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject talkCanvas;
    [SerializeField] GameObject selectCanvas;
    [SerializeField] Text talkTextObj;　　　　　　// 通常会話用のテキストボックス
    [SerializeField] Text[] selectTextObj;       // 選択肢のテキストボックス
    [SerializeField] Text nameTextObj;           // 名前表示用テキスト
    [SerializeField] GameObject[] selectImage;   // 選択表示のアイコン
    private string talkText;                     // 通常会話の文字列
    private string[] selectText = new string[2]; // 選択肢の文字列

    [Header("文字送りと改行の時間")]
    [SerializeField] float feedTime;             // 文字表示のスピード
    [SerializeField] float newLineTime;          // 改行のウェイト
    [SerializeField] float selectWait;           // 選択肢を表示するまでのウェイト
    [SerializeField] float talkInterval;         // 再度話しかける際の間隔
    private int visibleLength;                   // 表示する文字数

    [Header("バトル遷移時のフェードとカメラ")]
    [SerializeField] Camera camera;
    [SerializeField] float maxFOV;               // 視野角最大値
    [SerializeField] float minFOV;               // 視野角最小値
    [SerializeField] float cameraMoveSpeed;      // カメラの移動速度
    [SerializeField] private FadeManager fade;   // スクリプト
    [SerializeField] private float fadeSpeed;    // フェードの速さ
    [SerializeField] private Color fadeColor;    // フェードのカラー

    [Header("フラグ")]
    public bool isTalking;                       // 会話中かのフラグ
    public bool isSelect;                        // セレクト中のフラグ
    public bool buttonFlag;                      // 会話中にボタンを押せなくするフラグ

    [Header("SE")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip mojiokuri;
    [SerializeField] private AudioClip select;
    private void Start()
    {
        npcData = GameObject.Find("DataManager").GetComponent<NPCDataManager>();
        playerData = GameObject.Find("DataManager").GetComponent<PlayerDataManager>();
    }
    void Update()
    {
        if (buttonFlag) { return; }
        if (gamepad == null) gamepad = Gamepad.current;

        // テキスト表示中でなく会話が始まっているなら
        if (!isTalking && npc.GetComponent<NPCClass>().GetTalkFlag())
        {
            //テキストボックス表示
            nameTextObj.text = npc.GetComponent<NPCClass>().GetName();
            canvas.SetActive(true);
            talkCanvas.SetActive(true);

            // テキスト表示開始
            StartCoroutine("TextDisplay");

        }
        if (isSelect && npc.GetComponent<NPCClass>().GetTalkFlag())
        {
            StartCoroutine("SelectDisplay");
        }
    }

    // テキスト更新
    public void SetText(string newtext)
    {
        this.talkText = newtext;
        visibleLength = 0;
        //現在のテキストを消去
        talkTextObj.text = "";
    }

    // 選択肢関連
    //-------------------------------------------------------------------------------------------------------
    enum TalkSelect
    {
        First,
        Second
    };
    int nowSelect;
    bool stickFlag;
    bool buttonPush = false;
    IEnumerator SelectDisplay()
    {
        isSelect = false;
        selectCanvas.SetActive(true);

        //初期選択を一つ目にし画像を表示
        nowSelect = (int)TalkSelect.First;
        selectImage[(int)TalkSelect.First].SetActive(true);
        selectImage[(int)TalkSelect.Second].SetActive(false);

        // テキストの読み込み
        for (int i = 0; i < selectText.Length; ++i)
        {
            selectText[i] = npc.GetComponent<NPCClass>().GetTalk(npc.GetComponent<NPCClass>().GetState())[i];
            selectTextObj[i].text = selectText[i];
        }
        yield return new WaitForSeconds(selectWait);
        //ボタンを押すまでループ
        while (!gamepad.buttonEast.wasPressedThisFrame)
        {
            if (gamepad.leftStick.ReadValue().y > 0 || gamepad.leftStick.ReadValue().y < 0)
            {
                if (stickFlag)
                {
                    // 二つ目の選択肢に切り替え
                    if (nowSelect == (int)TalkSelect.First)
                    {
                        nowSelect = (int)TalkSelect.Second;
                        selectImage[(int)TalkSelect.First].SetActive(false);
                        selectImage[(int)TalkSelect.Second].SetActive(true);
                    }
                    // 一つ目の選択肢に切り替え
                    else
                    {
                        nowSelect = (int)TalkSelect.First;
                        selectImage[(int)TalkSelect.Second].SetActive(false);
                        selectImage[(int)TalkSelect.First].SetActive(true);
                    }
                }

                stickFlag = false;
            }
            else
            {
                stickFlag = true;
            }

            yield return 0;
        }

        audioSource.PlayOneShot(select);
        // 決定時
        switch (nowSelect)
        {
            // 一つ目の選択肢の処理
            case (int)TalkSelect.First:
                // 状態を変更
                npc.GetComponent<NPCClass>().SetState(npc.GetComponent<NPCClass>().GetFirstSelectState());
                selectCanvas.SetActive(false);
                isTalking = false;

                yield break;

            // 二つ目の選択肢の処理
            case (int)TalkSelect.Second:
                // 状態を変更
                npc.GetComponent<NPCClass>().SetState(npc.GetComponent<NPCClass>().GetSecondSelectState());
                selectCanvas.SetActive(false);
                isTalking = false;

                yield break;
        }
    }
    //-------------------------------------------------------------------------------------------------------
    [SerializeField] bool skipFlag;
    IEnumerator Skip()
    {
        yield return new WaitForSeconds(0.5f);
        skipFlag = true;
    }
    IEnumerator TextDisplay()
    {
        //一度だけ呼び出す処理
        //-------------------------------------------------
        visibleLength = 0;
        isTalking = true;
        //-------------------------------------------------

        for (int i = 0; i < npc.GetComponent<NPCClass>().GetTalk(npc.GetComponent<NPCClass>().GetState()).Length; ++i)
        {
            skipFlag = false;
            SetText(npc.GetComponent<NPCClass>().GetTalk(npc.GetComponent<NPCClass>().GetState())[i]);
            //出てない文字があれば
            while (visibleLength < talkText.Length)
            {
                yield return new WaitForSeconds(feedTime);
                // 1文字ずつ増やす
                visibleLength++;
                talkTextObj.text = talkText.Substring(0, visibleLength);
                audioSource.PlayOneShot(mojiokuri);
                StartCoroutine("Skip");
                // ボタンを押したらすべて表示
                if (skipFlag && gamepad.buttonEast.isPressed)
                {
                    audioSource.PlayOneShot(select);
                    visibleLength = talkText.Length;
                    talkTextObj.text = talkText.Substring(0, visibleLength);
                    yield return new WaitForSeconds(0.3f);
                    break;
                }
            }
            while (gamepad.buttonEast.isPressed)
            {
                yield return 0;
            }
            //会話終了
            if (i == npc.GetComponent<NPCClass>().GetTalk(npc.GetComponent<NPCClass>().GetState()).Length - 1)
            {

                switch (npc.GetComponent<NPCClass>().GetState())
                {
                    case NPCClass.NPCState.Normal:
                        //セレクトの項目があるとき
                        if (npc.GetComponent<NPCClass>().GetSelectFlag())
                        {
                            while (!gamepad.buttonEast.isPressed)
                            {
                                yield return 0;
                            }
                            yield return new WaitForSeconds(selectWait);

                            //セレクト状態に変更
                            npc.GetComponent<NPCClass>().SetState(NPCClass.NPCState.Select);

                            //会話用キャンバスを非表示
                            talkCanvas.SetActive(false);

                            //セレクトフラグをtrueに
                            isSelect = true;

                            yield break;
                        }
                        //会話の分岐が存在しない
                        else
                        {
                            while (!gamepad.buttonEast.isPressed)
                            {
                                yield return 0;
                            }
                            yield return new WaitForSeconds(newLineTime);

                            npc.GetComponent<NPCClass>().SetState(npc.GetComponent<NPCClass>().GetNonSelectState());

                            //会話を区切る
                            isTalking = false;

                            yield break;
                        }
                    case NPCClass.NPCState.Friend:
                        while (!gamepad.buttonEast.isPressed)
                        {
                            yield return 0;
                        }
                        yield return new WaitForSeconds(newLineTime);

                        //会話終了状態に変更
                        npc.GetComponent<NPCClass>().SetState(NPCClass.NPCState.FriendEventEnd);

                        //会話を区切る
                        isTalking = false;

                        yield break;
                    case NPCClass.NPCState.FriendEventEnd:
                        while (!gamepad.buttonEast.isPressed)
                        {
                            yield return 0;
                        }
                        yield return new WaitForSeconds(newLineTime);

                        //テキストボックス非表示
                        canvas.SetActive(false);
                        talkCanvas.SetActive(false);

                        //ボタンを押せるようにする
                        buttonFlag = true;

                        //プレイヤーを待機状態に変更
                        playerStatus.SetState(PlayerStatus_Solo.State.Stay);
                        npc.GetComponent<NPCClass>().SetState(npc.GetComponent<NPCClass>().GetEndState());

                        yield return new WaitForSeconds(talkInterval);
                        //会話終了
                        isTalking = false;

                        yield break;
                    case NPCClass.NPCState.Battle:
                        while (!gamepad.buttonEast.isPressed)
                        {
                            yield return 0;
                        }
                        yield return new WaitForSeconds(newLineTime);

                        //必要なデータを保存
                        playerData.StoryEndPlayerPos(player.transform.position, player.transform.rotation, playerCamera.transform.rotation);
                        npcData.StoryEndNPCData(npc.GetComponent<NPCClass>().GetEnemyName(), npc.GetComponent<NPCClass>().GetEventID());
                        // フェード開始
                        fade.FadeSceneChange("StoryBattle", fadeColor.r, fadeColor.g, fadeColor.b, fadeSpeed);

                        // フェード中にカメラを動かす
                        while (camera.fieldOfView <= maxFOV)
                        {
                            camera.fieldOfView++;
                            yield return new WaitForSeconds(cameraMoveSpeed);
                        }
                        while (camera.fieldOfView >= minFOV)
                        {
                            camera.fieldOfView--;
                            yield return new WaitForSeconds(cameraMoveSpeed);
                        }
                        yield break;
                    case NPCClass.NPCState.BattleEventEnd:
                        while (!gamepad.buttonEast.isPressed)
                        {
                            yield return 0;
                        }
                        yield return new WaitForSeconds(newLineTime);

                        //テキストボックス非表示
                        canvas.SetActive(false);
                        talkCanvas.SetActive(false);

                        //会話対象をnullにする
                        npc = null;

                        //ボタンを押せるようにする
                        buttonFlag = true;

                        //プレイヤーを待機状態に変更
                        playerStatus.SetState(PlayerStatus_Solo.State.Stay);

                        yield return new WaitForSeconds(talkInterval);
                        //会話終了
                        isTalking = false;

                        yield break;
                }
            }
            else
            {
                while (!gamepad.buttonEast.isPressed)
                {
                    yield return 0;
                }
                yield return new WaitForSeconds(newLineTime);
            }
        }
    }
}
