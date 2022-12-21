using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Write_Effect : MonoBehaviour
{
    Gamepad gamepad;
    [Header("�X�N���v�g")]
    [SerializeField] PlayerStatus_Solo playerStatus;
    [SerializeField] PlayerDataManager playerData;
    [SerializeField] NPCDataManager    npcData;
    [SerializeField] SceneDataManager  sceneData;

    [Header("�L�����o�X")]
    [SerializeField] GameObject canvas = default;
    [SerializeField] GameObject talkCanvas = default;
    [SerializeField] GameObject selectCanvas = default;
    [SerializeField] Text talkTextObj = default;//�e�L�X�g�{�b�N�X
    [SerializeField] Text[] selectTextObj;
    [SerializeField] GameObject[] selectImage;
    private string[] selectText = new string[2]; //�I�����̕�����
    private string talkText;   //�e�L�X�g�{�b�N�X�ɓ���镶����

    [Header("�������莞��")]
    [SerializeField] float feedTime;

    [Header("���s����")]
    [SerializeField] float newLineTime;
    private int visibleLength;      //�\�����镶����

    [Header("�t���O")]
    public bool isTalking;  //��b�����̃t���O
    public bool isSelect;   //�Z���N�g���̃t���O
    public bool buttonFlag; //��b���Ƀ{�^���������Ȃ�����t���O

    [Header("�o�g���J�ڎ��̃t�F�[�h�ƃJ����")]
    [SerializeField] Camera camera;
    [SerializeField] float maxFOV;             // ����p�ő�l
    [SerializeField] float minFOV;             // ����p�ŏ��l
    [SerializeField] float cameraMoveSpeed;    // �J�����̈ړ����x
    [SerializeField] private FadeManager fade; // �X�N���v�g
    [SerializeField] private float fadeSpeed;  // �t�F�[�h�̑���
    [SerializeField] private Color fadeColor;  // �t�F�[�h�̃J���[

    [Header("NPC")]
    public GameObject npc;  //�ڐG����NPC

    [Header("�v���C���[")]
    [SerializeField] GameObject player;

    void Update()
    {
        if (buttonFlag) { return; }
        if (gamepad == null) gamepad = Gamepad.current;

        //��b������Ȃ��Ƃ��A�b����������
        if (!isTalking && npc.GetComponent<NPCClass>().GetTalkFlag())
        {
            print("��b��");
            //�e�L�X�g�{�b�N�X�\��
            canvas.SetActive(true);
            talkCanvas.SetActive(true);
            switch (npc.GetComponent<NPCClass>().GetState())
            {
                case NPCClass.NPCState.Normal:
                    //��������J�n
                    StartCoroutine("TextDisplay");
                    break;
                case NPCClass.NPCState.Battle:
                    //��������J�n
                    StartCoroutine("TextDisplay");
                    break;
                case NPCClass.NPCState.Friend:
                    //��������J�n
                    StartCoroutine("TextDisplay");
                    break;
                case NPCClass.NPCState.FriendEventEnd:
                    //��������J�n
                    StartCoroutine("TextDisplay");
                    break;
                case NPCClass.NPCState.BattleEventEnd:
                    //��b�J�n
                    StartCoroutine("TextDisplay");
                    break;
            }
        }
        if(isSelect && npc.GetComponent<NPCClass>().GetTalkFlag())
        {
            StartCoroutine("SelectDisplay");
        }
    }
    //����Ńe�L�X�g���X�V����
    public void SetText(string newtext)
    {
        this.talkText = newtext;
        visibleLength = 0;
        //���݂̃e�L�X�g������
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
        print("�I�����\��");
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
            print("�ڂ���������Ă�");
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
                print("�ЂƂ߂̃{�^���������܂���");
                npc.GetComponent<NPCClass>().SetState(npc.GetComponent<NPCClass>().GetFirstSelectState());
                selectCanvas.SetActive(false);
                isTalking = false;
                yield break;
            case (int)TalkSelect.No:
                print("�ӂ��߂̃{�^���������܂���");
                npc.GetComponent<NPCClass>().SetState(npc.GetComponent<NPCClass>().GetSecondSelectState());
                selectCanvas.SetActive(false);
                isTalking = false;
                yield break;
        }
    }

    IEnumerator TextDisplay()
    {
        //��x�����Ăяo������
        //-------------------------------------------------
        visibleLength = 0;
        isTalking = true;
        //-------------------------------------------------

        for (int i = 0; i < npc.GetComponent<NPCClass>().GetTalk(npc.GetComponent<NPCClass>().GetState()).Length; ++i)
        {
            Debug.Log("�z��ԍ�" + i);
            SetText(npc.GetComponent<NPCClass>().GetTalk(npc.GetComponent<NPCClass>().GetState())[i]);
            Debug.Log(talkText);
            //�o�ĂȂ������������
            while (visibleLength < talkText.Length)
            {
                yield return new WaitForSeconds(feedTime);
                // 1���������₷
                visibleLength++;
                talkTextObj.text = talkText.Substring(0, visibleLength);
                if (gamepad.buttonEast.isPressed)
                {
                    visibleLength = talkText.Length - 1;
                }
            }
            //��b�I��
            if (i == npc.GetComponent<NPCClass>().GetTalk(npc.GetComponent<NPCClass>().GetState()).Length - 1)
            {
                
                switch (npc.GetComponent<NPCClass>().GetState())
                {
                    case NPCClass.NPCState.Normal:
                        //�Z���N�g�̍��ڂ�����Ƃ�
                        if(npc.GetComponent<NPCClass>().GetSelectFlag())
                        {
                            while(!gamepad.buttonEast.isPressed)
                            {
                                yield return 0;
                            }
                            yield return new WaitForSeconds(newLineTime);
                            //�Z���N�g��ԂɕύX
                            npc.GetComponent<NPCClass>().SetState(NPCClass.NPCState.Select);
                            //��b�p�L�����o�X���\��
                            talkCanvas.SetActive(false);
                            //�Z���N�g�t���O��true��
                            isSelect = true;
                            print("�ʏ��b�I��1");
                            yield break;
                        }
                        //��b�̕��򂪑��݂��Ȃ�
                        else
                        {
                            while (!gamepad.buttonEast.isPressed)
                            {
                                yield return 0;
                            }
                            yield return new WaitForSeconds(newLineTime);
                            print("��b�I��");
                            npc.GetComponent<NPCClass>().SetState(npc.GetComponent<NPCClass>().GetNonSelectState());
                            //��b����؂�
                            isTalking = false;
                            yield break;
                        }
                    case NPCClass.NPCState.Friend:
                        while (!gamepad.buttonEast.isPressed)
                        {
                            yield return 0;
                        }
                        yield return new WaitForSeconds(newLineTime);
                        //��b�I����ԂɕύX
                        npc.GetComponent<NPCClass>().SetState(NPCClass.NPCState.FriendEventEnd);
                        //��b����؂�
                        isTalking = false;
                        yield break;
                    case NPCClass.NPCState.FriendEventEnd:
                        while (!gamepad.buttonEast.isPressed)
                        {
                            yield return 0;
                        }
                        yield return new WaitForSeconds(newLineTime);
                        //�e�L�X�g�{�b�N�X��\��
                        canvas.SetActive(false);
                        talkCanvas.SetActive(false);
                        //�{�^����������悤�ɂ���
                        buttonFlag = true;
                        //�v���C���[��ҋ@��ԂɕύX
                        playerStatus.SetState(PlayerStatus_Solo.State.Stay);
                        npc.GetComponent<NPCClass>().SetState(npc.GetComponent<NPCClass>().GetEndState());
                        //��b�I��
                        isTalking = false;

                        Debug.Log("��b�I��");
                        yield break;
                    case NPCClass.NPCState.Battle:
                        while (!gamepad.buttonEast.isPressed)
                        {
                            yield return 0;
                        }
                        yield return new WaitForSeconds(newLineTime);
                        //�K�v�ȃf�[�^��ۑ�
                        playerData.StoryEndPlayerData(playerStatus.GetHp(), playerStatus.GetPower(), player.transform.position);
                        npcData.StoryEndNPCData(npc.GetComponent<NPCClass>().GetEnemyName(), npc.GetComponent<NPCClass>().GetEventID());
                        // �t�F�[�h�J�n
                        fade.FadeSceneChange("StoryBattle", fadeColor.r, fadeColor.g, fadeColor.b, fadeSpeed);

                        // �t�F�[�h���ɃJ�����𓮂���
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
                        //�e�L�X�g�{�b�N�X��\��
                        canvas.SetActive(false);
                        talkCanvas.SetActive(false);
                        //��b�Ώۂ�null�ɂ���
                        npc = null;
                        //�{�^����������悤�ɂ���
                        buttonFlag = true;
                        //�v���C���[��ҋ@��ԂɕύX
                        playerStatus.SetState(PlayerStatus_Solo.State.Stay);
                        //��b�I��
                        isTalking = false;

                        Debug.Log("��b�I��");
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
