using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Button_Manager : MonoBehaviour
{
    Gamepad gamepad;
    Gamepad gamepadInputed;
    bool selectLock;

    //�t�F�[�h
    [SerializeField]
    Fade fadeScript;
    //�����摜�̈ʒu
    [SerializeField]
    Image TutorialImage;
    //�\������摜�̓ǂݍ���
    [SerializeField]
    Sprite[] selectSprites = new Sprite[5];
    //�{�^���摜�̓ǂݍ���
    [SerializeField]
    GameObject[] button_Images = new GameObject[1];
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
        nowSelecting = 0;
        prevSelecting = nowSelecting;
        audioSource = GetComponent<AudioSource>();
        button_Images[0].GetComponent<Animator>().SetBool("selected", true);
        TutorialImage.sprite = selectSprites[0];
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Gamepad.all.Count; ++i)
        {
            gamepad = Gamepad.all[i];
            StageSelectSystem();
            BackMenu();
        }

        //���b�N�����̔���
        if (gamepadInputed != null)
        {
            //���艟����Ă��Ȃ��@���@����͂Ȃ��@���@�����͂Ȃ�
            if (!gamepadInputed.buttonSouth.wasPressedThisFrame &&
                gamepadInputed.leftStick.ReadValue().y <= 0.1f &&
                gamepadInputed.leftStick.ReadValue().y >= -0.1f
                )
            {
                //���b�N����
                selectLock = false;
                gamepadInputed = null;
            }
        }

        //�I�𒆂̃{�^�����؂�ւ������
        if (nowSelecting != prevSelecting)
        {
            //���O�̃{�^����������
            button_Images[prevSelecting].GetComponent<Animator>().SetBool("selected", false);
            //�I�𒆂�傫��
            button_Images[nowSelecting].GetComponent<Animator>().SetBool("selected", true);
            //���ʉ��Đ�
            audioSource.PlayOneShot(selectSound);
            //���O�̑I�����X�V
            prevSelecting = nowSelecting;
            //�C���[�W�ύX
            TutorialImage.sprite = selectSprites[nowSelecting];
        }

        //�t�F�[�h���I������烁�j���[�ɖ߂�
        if (fadeScript.FadeOut())
        {
            SceneManager.LoadScene("Menu");
        }
    }

    void StageSelectSystem()
    {
        //���b�N���������Ă��Ȃ��ꍇ
        if (selectLock == false)
        {
            if (gamepad.leftStick.ReadValue().y < -0.1f)
            {
                //���I��
                nowSelecting++;
                //�ő�l�𒴂�����0�ɂ���
                if (nowSelecting > button_Images.Length - 1)
                    nowSelecting = 0;
                //���b�N��������
                selectLock = true;
                gamepadInputed = gamepad;
            }
            //������
            else if (gamepad.leftStick.ReadValue().y > 0.1f)
            {
                //����I��
                nowSelecting--;
                if (nowSelecting < 0)
                    nowSelecting = button_Images.Length - 1;
                //���b�N��������
                selectLock = true;
                gamepadInputed = gamepad;
            }
        }
    }

    void BackMenu()
    {
        if (nowSelecting == button_Images.Length - 1)
        {
            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
                selectLock = true;
                fadeScript.fademode = true;
            }
        }
    }
}
