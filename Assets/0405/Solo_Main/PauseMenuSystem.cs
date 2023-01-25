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

public class PauseMenuSystem : MonoBehaviour
{
    public enum menu
    {
        resume = 0,
        retry,
        back
    }

    protected Gamepad gamepad;
    //�J�[�\���ړ�
    protected bool selectLock;
    //���d�N���b�N�h�~��bool
    protected bool onButton;
    //��b���Ƀ|�[�Y�s�ɂ���
    [SerializeField] PlayerStatus_Solo playerStatus_Solo;
    //�t�F�[�h
    [SerializeField]
    protected Fade fadeScript;
    //�ڍs����V�[����ۑ�����ꏊ
    protected string nextscene;
    //�{�^���摜�̓ǂݍ���
    [System.Serializable]
    public class Button
    {
        public GameObject buttonImage;
        public menu menuCell;
    }
    [SerializeField]
    protected Button[] buttonClass = new Button[1];

    //�{�^���摜��u���Ă���p�l����摜
    [SerializeField]
    protected GameObject PausePanel;
    //�I�[�f�B�I
    [SerializeField]
    protected AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip selectSound;
    public AudioClip pushSound;
    //�I�𒆂̃{�^��
    protected int nowSelecting;
    //���O�ɑI�������{�^��
    protected int prevSelecting;
    //�|�[�Y����
    protected bool ispauseNow;
    //�|�[�Y���j���[��W�J�ł����Ԃ��ǂ���
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

        //�����ʒu
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

        //���j���[��\�����Ă���ꍇ�̂�
        if (ispauseNow)
        {
            SelectSystem();
            onClickAction();
        }

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

    //�N���b�N�����Ƃ��̋���
    protected virtual void onClickAction()
    {
        //�A�Ŗh�~�p
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
        //��������|�[�Y�؂�ւ�
        if (gamepad.startButton.wasPressedThisFrame)
        {
            ispauseNow = !ispauseNow;


            //�|�[�Y���ɂȂ�����
            if (ispauseNow)
            {
                audioSource.PlayOneShot(openSound);
                PausePanel.SetActive(true);
                nowSelecting = 0;
                buttonClass[nowSelecting].buttonImage.GetComponent<Animator>().SetBool("selected", true);
                Time.timeScale = 0.0f;

            }
            //��|�[�Y���ɂȂ�����
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

    //�|�[�Y������Ԃ�
    public bool pausejudge()
    {
        return ispauseNow;
    }
}
