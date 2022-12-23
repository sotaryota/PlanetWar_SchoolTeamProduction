using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

//�|�[�Y���������V�[���ɂ����u���B
//�t�F�[�h����A�{�^���ɂ������摜�A���̃{�^�����܂Ƃ߂�I�u�W�F�N�g�����A�^�b�`����΂���
//�{�^���ɂ̓A�j���[�V�������������邱�ƁB�Ȃ��A�uUpdateMode�v���uUnscaledTime�v�ɂ��邱�ƁI
//���̃V�[���̃}�l�[�W���[��Update�ɉ��L��u���Ƃ��Ȃ��Ɠ������肷��̂Œ���

//PauseMenuSystem pauseMenuSystem;
//if (pauseMenuSystem.pausejudge()) { }

//SetCanPause(_bool_); �Ń|�[�Y�s�ɂł���B
//��b���n�܂�Ƃ���False�𑗂�A�I�������True�𑗂铙

public class GameoverMenu : MonoBehaviour
{
    public enum menu
    {
        retry = 0,
        backmenu
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
        public string nextScene;
    }
    [SerializeField]
    private Button[] buttonClass = new Button[1];

    //�{�^���摜��u���Ă���p�l����摜
    [SerializeField]
    GameObject PausePanel;
    //�I�[�f�B�I
    [SerializeField]
    AudioSource audioSource;
    public AudioClip selectSound;
    public AudioClip pushSound;
    //�I�𒆂̃{�^��
    int nowSelecting;
    //���O�ɑI�������{�^��
    int prevSelecting;
    //�|�[�Y����
    bool ispauseNow;
    //�|�[�Y���j���[��W�J�ł����Ԃ��ǂ���
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

        //�����ʒu
        nowSelecting = 0;
        prevSelecting = nowSelecting;
        audioSource = GetComponent<AudioSource>();
        PausePanel.SetActive(false);
    }

    void Update()
    {
        gamepad = Gamepad.current;



        //���j���[��\�����Ă���ꍇ�̂�
        if (ispauseNow)
        {
            SelectSystem();
            onClickAction();
        }

        //���艟����Ă��Ȃ��@���@����͂Ȃ��@���@�����͂Ȃ�
        if (!gamepad.buttonSouth.wasPressedThisFrame &&
            gamepad.leftStick.ReadValue().x <= 0.1f &&
            gamepad.leftStick.ReadValue().x >= -0.1f
            )
        {
            //���b�N����
            selectLock = false;
        }

        //�I�𒆂̃{�^�����؂�ւ������
        if (nowSelecting != prevSelecting)
        {
            //���O�̃{�^����������
            buttonClass[prevSelecting].buttonImage.GetComponent<Animator>().SetBool("selected", false);
            //�I�𒆂�傫��
            buttonClass[nowSelecting].buttonImage.GetComponent<Animator>().SetBool("selected", true);
            //���ʉ��Đ�
            audioSource.PlayOneShot(selectSound);
            //���O�̑I�����X�V
            prevSelecting = nowSelecting;
        }
        //�t�F�[�h���I������烁�j���[�ɖ߂�
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
            if (gamepad.leftStick.ReadValue().x < -0.1f)
            {
                nowSelecting++;
                //�ő�l�𒴂�����0�ɂ���
                if (nowSelecting >= buttonClass.Length)
                    nowSelecting = 0;
                //���b�N��������
                selectLock = true;
            }
            //�����
            else if (gamepad.leftStick.ReadValue().x > 0.1f)
            {
                nowSelecting--;
                if (nowSelecting < 0)
                    nowSelecting = buttonClass.Length - 1;
                //���b�N��������
                selectLock = true;
            }
        }
    }

    //�N���b�N�����Ƃ��̋���
    void onClickAction()
    {
        //�A�Ŗh�~�p
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

    //�|�[�Y������Ԃ�
    public bool pausejudge()
    {
        return ispauseNow;
    }
}
