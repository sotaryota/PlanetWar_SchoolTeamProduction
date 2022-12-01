using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Solo_Menu_System : MonoBehaviour
{
    enum menu
    {
        NewGame = 0,
        Continue,
        BackMenu,

        ENUMEND
    }

    Gamepad gamepad;
    //�J�[�\���ړ�
    bool selectLock;
    //���d�N���b�N�h�~��bool
    bool onButton;

    //�t�F�[�h
    [SerializeField]
    Fade fadeScript;
    //�ڍs����V�[����ۑ�����ꏊ
    string nextscene;
    //�{�^���摜�̓ǂݍ���
    [Header("�A�j���[�V����������I�u�W�F�N�g")]
    [SerializeField]
    GameObject[] button_Images = new GameObject[(int)menu.ENUMEND];
    //�I�[�f�B�I
    [SerializeField]
    AudioSource audioSource;
    public AudioClip selectSound;
    //�I�𒆂̃{�^��
    int nowSelecting;
    //���O�ɑI�������{�^��
    int prevSelecting;

    void Start()
    {
        selectLock = false;
        onButton = false;
        nextscene = "";

        //�����ʒu
        nowSelecting = 0;
        prevSelecting = nowSelecting;
        audioSource = GetComponent<AudioSource>();
        button_Images[0].GetComponent<Animator>().SetBool("selected", true);
    }

    void Update()
    {
        gamepad = Gamepad.current;
        SelectSystem();
        onClickAction();
        //���艟����Ă��Ȃ��@���@����͂Ȃ��@���@�����͂Ȃ�
        if (!gamepad.buttonSouth.wasPressedThisFrame &&
            gamepad.leftStick.ReadValue().y <= 0.1f &&
            gamepad.leftStick.ReadValue().y >= -0.1f
            )
        {
            //���b�N����
            selectLock = false;
        }

        //�I�𒆂̃{�^�����؂�ւ������
        if (nowSelecting != prevSelecting)
        {
            //���O�̃{�^���̃A�j���[�V�����I������
            button_Images[prevSelecting].GetComponent<Animator>().SetBool("selected", false);
            //�I�𒆂̃{�^���̃A�j���[�V�����J�n
            button_Images[nowSelecting].GetComponent<Animator>().SetBool("selected", true);
            //���ʉ��Đ�
            audioSource.PlayOneShot(selectSound);
            //���O�̑I�����X�V
            prevSelecting = nowSelecting;
        }

        //�t�F�[�h���I�������V�[���J��
        if (fadeScript.FadeOut())
        {
            SceneManager.LoadScene(nextscene);
        }
    }

    void SelectSystem()
    {
        //�������b�N���������Ă��Ȃ��ꍇ
        if (selectLock == false && onButton == false)
        {
            //������
            if (gamepad.leftStick.ReadValue().y < -0.1f)
            {
                nowSelecting++;
                //�ő�l�𒴂�����0�ɂ���
                if (nowSelecting >= (int)menu.ENUMEND)
                    nowSelecting = 0;
                //���b�N��������
                selectLock = true;
            }
            //�����
            else if (gamepad.leftStick.ReadValue().y > 0.1f)
            {
                nowSelecting--;
                if (nowSelecting < 0)
                    nowSelecting = (int)menu.ENUMEND - 1;
                //���b�N��������
                selectLock = true;
            }
        }
    }
    void onClickAction()
    {
        //�A�ŋ֎~�p
        if (!onButton)
        {
            //��������
            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
                //������~�߂�
                selectLock = true;
                onButton = true;
                //�I��ł��鍀�ڕʂɋ�����ς���
                switch (nowSelecting)
                {
                    case (int)menu.NewGame:
                        fadeScript.fademode = true;
                        nextscene = "Title";
                        break;
                    case (int)menu.Continue:
                        fadeScript.fademode = true;
                        nextscene = "Main";
                        break;
                    case (int)menu.BackMenu:
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
