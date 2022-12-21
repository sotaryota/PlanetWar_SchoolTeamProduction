using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Write_Effect : MonoBehaviour
{
    Gamepad gamepad;
    [Header("スクリプト")]
    [SerializeField] PlayerStatus_Solo playerStatus;
    [SerializeField] PlayerDataManager playerData;
    [SerializeField] NPCDataManager    npcData;
    [SerializeField] SceneDataManager  sceneData;

    [Header("キャンバス")]
    [SerializeField] GameObject canvas = default;
    [SerializeField] GameObject talkCanvas = default;
    [SerializeField] GameObject selectCanvas = default;
    [SerializeField] Text talkTextObj = default;//テキストボックス
    [SerializeField] Text[] selectTextObj;
    [SerializeField] GameObject[] selectImage;
    private string[] selectText = new string[2]; //選択肢の文字列
    private string talkText;   //テキストボックスに入れる文字列

    [Header("文字送り時間")]
    [SerializeField] float feedTime;

    [Header("改行時間")]
    [SerializeField] float newLineTime;
    private int visibleLength;      //表示する文字数

    [Header("フラグ")]
    public bool isTalking;  //会話中かのフラグ
    public bool isSelect;   //セレクト中のフラグ
    public bool buttonFlag; //会話中にボタンを押せなくするフラグ

    [Header("バトル遷移時のフェードとカメラ")]
    [SerializeField] Camera camera;
    [SerializeField] float maxFOV;             // 視野角最大値
    [SerializeField] float minFOV;             // 視野角最小値
    [SerializeField] float cameraMoveSpeed;    // カメラの移動速度
    [SerializeField] private FadeManager fade; // スクリプト
    [SerializeField] private float fadeSpeed;  // フェードの速さ
    [SerializeField] private Color fadeColor;  // フェードのカラー

    [Header("NPC")]
    public GameObject npc;  //接触中のNPC

    [Header("プレイヤー")]
    [SerializeField] GameObject player;

    void Update()
    {
        if (buttonFlag) { return; }
        if (gamepad == null) gamepad = Gamepad.current;

        //会話中じゃないとき、話しかけたら
        if (!isTalking && npc.GetComponent<NPCClass>().GetTalkFlag())
        {
            print("会話中");
            //テキストボックス表示
            canvas.SetActive(true);
            talkCanvas.SetActive(true);
            switch (npc.GetComponent<NPCClass>().GetState())
            {
                case NPCClass.NPCState.Normal:
                    //文字送り開始
                    StartCoroutine("TextDisplay");
                    break;
                case NPCClass.NPCState.Battle:
                    //文字送り開始
                    StartCoroutine("TextDisplay");
                    break;
                case NPCClass.NPCState.Friend:
                    //文字送り開始
                    StartCoroutine("TextDisplay");
                    break;
                case NPCClass.NPCState.FriendEventEnd:
                    //文字送り開始
                    StartCoroutine("TextDisplay");
                    break;
                case NPCClass.NPCState.BattleEventEnd:
                    //会話開始
                    StartCoroutine("TextDisplay");
                    break;
            }
        }
        if(isSelect && npc.GetComponent<NPCClass>().GetTalkFlag())
        {
            StartCoroutine("SelectDisplay");
        }
    }
    //これでテキストを更新する
    public void SetText(string newtext)
    {
        this.talkText = newtext;
        visibleLength = 0;
        //現在のテキストを消去
        talkTextObj.text = "";
    }

    enum TalkSelect
    {
        Yes,
        No
    };
    int nowSelect;
    bool stickFlag;
    IEnumerator SelectDisplay()
    {
        isSelect = false;
        print("選択肢表示");
        selectCanvas.SetActive(true);
        nowSelect = (int)TalkSelect.Yes;
        selectImage[(int)TalkSelect.Yes].SetActive(true);
        selectImage[(int)TalkSelect.No].SetActive(false);
        for (int i = 0; i < selectText.Length; ++i)
        {
            selectText[i] = npc.GetComponent<NPCClass>().GetTalk(npc.GetComponent<NPCClass>().GetState())[i];
            selectTextObj[i].text = selectText[i];
        }
        yield return new WaitForSeconds(0);
        while(!gamepad.buttonEast.isPressed)
        {
            print("ぼたんをおしてね");
            if (gamepad.leftStick.ReadValue().y > 0 || gamepad.leftStick.ReadValue().y < 0)
            {
                if (stickFlag)
                {
                    if (nowSelect == (int)TalkSelect.Yes)
                    {
                        nowSelect = (int)TalkSelect.No;
                        selectImage[(int)TalkSelect.Yes].SetActive(false);
                        selectImage[(int)TalkSelect.No].SetActive(true);
                        print("NO");
                    }
                    else
                    {
                        nowSelect = (int)TalkSelect.Yes;
                        selectImage[(int)TalkSelect.No].SetActive(false);
                        selectImage[(int)TalkSelect.Yes].SetActive(true);
                        print("YES");
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
        switch (nowSelect)
        {
            case (int)TalkSelect.Yes:
                print("ひとつめのボタンを押しました");
                npc.GetComponent<NPCClass>().SetState(npc.GetComponent<NPCClass>().GetFirstSelectState());
                selectCanvas.SetActive(false);
                isTalking = false;
                yield break;
            case (int)TalkSelect.No:
                print("ふたつめのボタンを押しました");
                npc.GetComponent<NPCClass>().SetState(npc.GetComponent<NPCClass>().GetSecondSelectState());
                selectCanvas.SetActive(false);
                isTalking = false;
                yield break;
        }
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
            Debug.Log("配列番号" + i);
            SetText(npc.GetComponent<NPCClass>().GetTalk(npc.GetComponent<NPCClass>().GetState())[i]);
            Debug.Log(talkText);
            //出てない文字があれば
            while (visibleLength < talkText.Length)
            {
                yield return new WaitForSeconds(feedTime);
                // 1文字ずつ増やす
                visibleLength++;
                talkTextObj.text = talkText.Substring(0, visibleLength);
                if (gamepad.buttonEast.isPressed)
                {
                    visibleLength = talkText.Length - 1;
                }
            }
            //会話終了
            if (i == npc.GetComponent<NPCClass>().GetTalk(npc.GetComponent<NPCClass>().GetState()).Length - 1)
            {
                
                switch (npc.GetComponent<NPCClass>().GetState())
                {
                    case NPCClass.NPCState.Normal:
                        //セレクトの項目があるとき
                        if(npc.GetComponent<NPCClass>().GetSelectFlag())
                        {
                            while(!gamepad.buttonEast.isPressed)
                            {
                                yield return 0;
                            }
                            yield return new WaitForSeconds(newLineTime);
                            //セレクト状態に変更
                            npc.GetComponent<NPCClass>().SetState(NPCClass.NPCState.Select);
                            //会話用キャンバスを非表示
                            talkCanvas.SetActive(false);
                            //セレクトフラグをtrueに
                            isSelect = true;
                            print("通常会話終了1");
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
                            print("会話終了");
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
                        //会話終了
                        isTalking = false;

                        Debug.Log("会話終了");
                        yield break;
                    case NPCClass.NPCState.Battle:
                        while (!gamepad.buttonEast.isPressed)
                        {
                            yield return 0;
                        }
                        yield return new WaitForSeconds(newLineTime);
                        //必要なデータを保存
                        playerData.StoryEndPlayerData(playerStatus.GetHp(), playerStatus.GetPower(), player.transform.position);
                        npcData.StoryEndNPCData(npc.GetComponent<NPCClass>().GetEnemyName(), npc.GetComponent<NPCClass>().GetEventID());
                        // フェード開始
                        fade.FadeSceneChange("StoryBattle", fadeColor.r, fadeColor.g, fadeColor.b, fadeSpeed);

                        // フェード中にカメラを動かす
                        while(camera.fieldOfView <= maxFOV)
                        {
                            camera.fieldOfView++;
                            yield return new WaitForSeconds(cameraMoveSpeed);
                        }
                        while(camera.fieldOfView >= minFOV)
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
                        //会話終了
                        isTalking = false;

                        Debug.Log("会話終了");
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
