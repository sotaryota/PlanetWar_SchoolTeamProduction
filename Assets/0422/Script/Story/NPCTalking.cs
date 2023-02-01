using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NPCTalking : MonoBehaviour
{
    Gamepad gamepad;

    [Header("�v���C���[")]
    [SerializeField] GameObject player;

    [Header("�J����")]
    [SerializeField] GameObject playerCamera;

    [Header("NPC")]
    public GameObject npc;                       //�ڐG����NPC

    [Header("�X�N���v�g")]
    [SerializeField] PlayerStatus_Solo playerStatus;
    [SerializeField] PlayerDataManager playerData;
    private NPCDataManager npcData;

    [Header("�L�����o�X")]
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject talkCanvas;
    [SerializeField] GameObject selectCanvas;
    [SerializeField] Text talkTextObj;�@�@�@�@�@�@// �ʏ��b�p�̃e�L�X�g�{�b�N�X
    [SerializeField] Text[] selectTextObj;       // �I�����̃e�L�X�g�{�b�N�X
    [SerializeField] Text nameTextObj;           // ���O�\���p�e�L�X�g
    [SerializeField] GameObject[] selectImage;   // �I��\���̃A�C�R��
    private string talkText;                     // �ʏ��b�̕�����
    private string[] selectText = new string[2]; // �I�����̕�����

    [Header("��������Ɖ��s�̎���")]
    [SerializeField] float feedTime;             // �����\���̃X�s�[�h
    [SerializeField] float newLineTime;          // ���s�̃E�F�C�g
    [SerializeField] float selectWait;           // �I������\������܂ł̃E�F�C�g
    [SerializeField] float talkInterval;         // �ēx�b��������ۂ̊Ԋu
    private int visibleLength;                   // �\�����镶����

    [Header("�o�g���J�ڎ��̃t�F�[�h�ƃJ����")]
    [SerializeField] Camera camera;
    [SerializeField] float maxFOV;               // ����p�ő�l
    [SerializeField] float minFOV;               // ����p�ŏ��l
    [SerializeField] float cameraMoveSpeed;      // �J�����̈ړ����x
    [SerializeField] private FadeManager fade;   // �X�N���v�g
    [SerializeField] private float fadeSpeed;    // �t�F�[�h�̑���
    [SerializeField] private Color fadeColor;    // �t�F�[�h�̃J���[

    [Header("�t���O")]
    public bool isTalking;                       // ��b�����̃t���O
    public bool isSelect;                        // �Z���N�g���̃t���O
    public bool buttonFlag;                      // ��b���Ƀ{�^���������Ȃ�����t���O

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

        // �e�L�X�g�\�����łȂ���b���n�܂��Ă���Ȃ�
        if (!isTalking && npc.GetComponent<NPCClass>().GetTalkFlag())
        {
            //�e�L�X�g�{�b�N�X�\��
            nameTextObj.text = npc.GetComponent<NPCClass>().GetName();
            canvas.SetActive(true);
            talkCanvas.SetActive(true);

            // �e�L�X�g�\���J�n
            StartCoroutine("TextDisplay");

        }
        if (isSelect && npc.GetComponent<NPCClass>().GetTalkFlag())
        {
            StartCoroutine("SelectDisplay");
        }
    }

    // �e�L�X�g�X�V
    public void SetText(string newtext)
    {
        this.talkText = newtext;
        visibleLength = 0;
        //���݂̃e�L�X�g������
        talkTextObj.text = "";
    }

    // �I�����֘A
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

        //�����I������ڂɂ��摜��\��
        nowSelect = (int)TalkSelect.First;
        selectImage[(int)TalkSelect.First].SetActive(true);
        selectImage[(int)TalkSelect.Second].SetActive(false);

        // �e�L�X�g�̓ǂݍ���
        for (int i = 0; i < selectText.Length; ++i)
        {
            selectText[i] = npc.GetComponent<NPCClass>().GetTalk(npc.GetComponent<NPCClass>().GetState())[i];
            selectTextObj[i].text = selectText[i];
        }
        yield return new WaitForSeconds(selectWait);
        //�{�^���������܂Ń��[�v
        while (!gamepad.buttonEast.wasPressedThisFrame)
        {
            if (gamepad.leftStick.ReadValue().y > 0 || gamepad.leftStick.ReadValue().y < 0)
            {
                if (stickFlag)
                {
                    // ��ڂ̑I�����ɐ؂�ւ�
                    if (nowSelect == (int)TalkSelect.First)
                    {
                        nowSelect = (int)TalkSelect.Second;
                        selectImage[(int)TalkSelect.First].SetActive(false);
                        selectImage[(int)TalkSelect.Second].SetActive(true);
                    }
                    // ��ڂ̑I�����ɐ؂�ւ�
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
        // ���莞
        switch (nowSelect)
        {
            // ��ڂ̑I�����̏���
            case (int)TalkSelect.First:
                // ��Ԃ�ύX
                npc.GetComponent<NPCClass>().SetState(npc.GetComponent<NPCClass>().GetFirstSelectState());
                selectCanvas.SetActive(false);
                isTalking = false;

                yield break;

            // ��ڂ̑I�����̏���
            case (int)TalkSelect.Second:
                // ��Ԃ�ύX
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
        //��x�����Ăяo������
        //-------------------------------------------------
        visibleLength = 0;
        isTalking = true;
        //-------------------------------------------------

        for (int i = 0; i < npc.GetComponent<NPCClass>().GetTalk(npc.GetComponent<NPCClass>().GetState()).Length; ++i)
        {
            skipFlag = false;
            SetText(npc.GetComponent<NPCClass>().GetTalk(npc.GetComponent<NPCClass>().GetState())[i]);
            //�o�ĂȂ������������
            while (visibleLength < talkText.Length)
            {
                yield return new WaitForSeconds(feedTime);
                // 1���������₷
                visibleLength++;
                talkTextObj.text = talkText.Substring(0, visibleLength);
                audioSource.PlayOneShot(mojiokuri);
                StartCoroutine("Skip");
                // �{�^�����������炷�ׂĕ\��
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
            //��b�I��
            if (i == npc.GetComponent<NPCClass>().GetTalk(npc.GetComponent<NPCClass>().GetState()).Length - 1)
            {

                switch (npc.GetComponent<NPCClass>().GetState())
                {
                    case NPCClass.NPCState.Normal:
                        //�Z���N�g�̍��ڂ�����Ƃ�
                        if (npc.GetComponent<NPCClass>().GetSelectFlag())
                        {
                            while (!gamepad.buttonEast.isPressed)
                            {
                                yield return 0;
                            }
                            yield return new WaitForSeconds(selectWait);

                            //�Z���N�g��ԂɕύX
                            npc.GetComponent<NPCClass>().SetState(NPCClass.NPCState.Select);

                            //��b�p�L�����o�X���\��
                            talkCanvas.SetActive(false);

                            //�Z���N�g�t���O��true��
                            isSelect = true;

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

                        yield return new WaitForSeconds(talkInterval);
                        //��b�I��
                        isTalking = false;

                        yield break;
                    case NPCClass.NPCState.Battle:
                        while (!gamepad.buttonEast.isPressed)
                        {
                            yield return 0;
                        }
                        yield return new WaitForSeconds(newLineTime);

                        //�K�v�ȃf�[�^��ۑ�
                        playerData.StoryEndPlayerPos(player.transform.position, player.transform.rotation, playerCamera.transform.rotation);
                        npcData.StoryEndNPCData(npc.GetComponent<NPCClass>().GetEnemyName(), npc.GetComponent<NPCClass>().GetEventID());
                        // �t�F�[�h�J�n
                        fade.FadeSceneChange("StoryBattle", fadeColor.r, fadeColor.g, fadeColor.b, fadeSpeed);

                        // �t�F�[�h���ɃJ�����𓮂���
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

                        //�e�L�X�g�{�b�N�X��\��
                        canvas.SetActive(false);
                        talkCanvas.SetActive(false);

                        //��b�Ώۂ�null�ɂ���
                        npc = null;

                        //�{�^����������悤�ɂ���
                        buttonFlag = true;

                        //�v���C���[��ҋ@��ԂɕύX
                        playerStatus.SetState(PlayerStatus_Solo.State.Stay);

                        yield return new WaitForSeconds(talkInterval);
                        //��b�I��
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
