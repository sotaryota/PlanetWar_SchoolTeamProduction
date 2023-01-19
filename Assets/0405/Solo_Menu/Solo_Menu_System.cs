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
    [System.Serializable]
    public class Button
    {
        public GameObject buttonImage;
        public menu menuCell;
    }
    [SerializeField]
    private Button[] buttonClass = new Button[1];

    //�I�[�f�B�I
    [SerializeField]
    AudioSource audioSource;
    public AudioClip selectSound;
    public AudioClip pushSound;
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
        buttonClass[0].buttonImage.GetComponent<Animator>().SetBool("selected", true);
    }

    void Update()
    {
        gamepad = Gamepad.current;
        SelectSystem();
        onClickAction();
        //���艟����Ă��Ȃ��@���@����͂Ȃ��@���@�����͂Ȃ�
        if (!gamepad.buttonEast.wasPressedThisFrame &&
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
            buttonClass[prevSelecting].buttonImage.GetComponent<Animator>().SetBool("selected", false);
            //�I�𒆂̃{�^���̃A�j���[�V�����J�n
            buttonClass[nowSelecting].buttonImage.GetComponent<Animator>().SetBool("selected", true);
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
                if (nowSelecting >= buttonClass.Length)
                    nowSelecting = 0;
                //���b�N��������
                selectLock = true;
            }
            //�����
            else if (gamepad.leftStick.ReadValue().y > 0.1f)
            {
                nowSelecting--;
                if (nowSelecting < 0)
                    nowSelecting = buttonClass.Length - 1;
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
            if (gamepad.buttonEast.wasPressedThisFrame)
            {
                //������~�߂�
                selectLock = true;
                onButton = true;
                //���艹
                audioSource.PlayOneShot(pushSound);
                //�I��ł��鍀�ڕʂɋ�����ς���
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
