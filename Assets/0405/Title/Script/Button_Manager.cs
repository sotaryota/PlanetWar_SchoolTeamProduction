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

    [SerializeField]
    Start_Button start_Button;
    Tutorial_Button tutorial_Button;
    End_Button end_Button;

    interface ButtonPressed
    {
        public abstract void Pressed();
    }
    class Gamestart : ButtonPressed
    {
        public Start_Button start_Button { get; private set; }

        public void Pressed()
        {
            start_Button.OnClick();
        }
    }
    class Tutorial : ButtonPressed
    {
        public Tutorial_Button tutorial_Button { get; private set; }
        public void Pressed()
        {
            tutorial_Button.OnClick();
        }
    }
    class End : ButtonPressed
    {
        public End_Button end_Button { get; private set; }
        public void Pressed()
        {
            end_Button.OnClick();
        }
    }

    ButtonPressed[] selectClass = new ButtonPressed[] { new Gamestart(), new Tutorial() };


    //�{�^���摜�̓ǂݍ���
    [SerializeField]
    GameObject[] button_Images = new GameObject[1];
    //�I�[�f�B�I
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
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Gamepad.all.Count; ++i)
        {
            gamepad = Gamepad.all[i];
            StageSelectSystem();
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
        }
    }

    void StageSelectSystem()
    {
        //���b�N���������Ă��Ȃ��ꍇ
        if (selectLock == false)
        {
            //����{�^��
            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
                //�X�e�[�W�I��
                selectClass[nowSelecting].Pressed();
                //���S���b�N
                selectLock = true;
            }
            //�����
            else if (gamepad.leftStick.ReadValue().y > 0.1f)
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
            else if (gamepad.leftStick.ReadValue().y < -0.1f)
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
}
