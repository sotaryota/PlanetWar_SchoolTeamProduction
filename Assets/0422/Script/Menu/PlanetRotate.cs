using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlanetRotate : MonoBehaviour
{
    Gamepad gamepad;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private GameObject PlanetGameObject;
    [SerializeField] private MenuSEManager menuSE;
    public bool buttonLock;  //�I�𒆂ɃJ�[�\���𓮂����Ȃ��悤��

    [Header("�f���̉�]")]
    [SerializeField] private float rotateSpeed;    //��]����
    [SerializeField] private float planetDistance; //�f���̊Ԋu

    private void Awake()
    {
        buttonLock = true;
        planetDistance = 360 / (int)MenuManager.SelectMenu.enumEnd;
    }
    // Update is called once per frame
    void Update()
    {
        if(gamepad == null)
        {
            gamepad = Gamepad.current;
        }

        PlanetRotation();
    }

    //--------------------------------------
    //�J�����𒆐S�ɒu���l���̘f������]������
    //--------------------------------------

    public void PlanetRotation()
    {
        if (buttonLock)
        {
            //�E����
            if (gamepad.leftStick.ReadValue().x > 0)
            {
                StartCoroutine("RightRotation");
            }
            //������
            else if (gamepad.leftStick.ReadValue().x < 0)
            {
                StartCoroutine("LeftRotation");
            }
        }
    }

    //--------------------------------------------------------------
    //��]����
    //for�����Ɖ�]�X�s�[�h���X�y�b�N�ˑ��ɂȂ�̂ŉ�]���@��͍���
    //--------------------------------------------------------------


    //�E��]
    IEnumerator RightRotation()
    {
        buttonLock = false;
        menuSE.SelectSE();
        
        //��]�����[�v������if
        if (menuManager.nowSelect >= MenuManager.SelectMenu.enumEnd - 1)
        {
            menuManager.nowSelect = MenuManager.SelectMenu.Story;
        }
        else
        {
            menuManager.nowSelect += 1;
        }

        //�\������Ă����摜������
        menuManager.menuDatas[(int)menuManager.beforeSelect].menuImage.SetActive(false);

        //�f���̉�]
        for (int i = 0; i < planetDistance; ++i)
        {
            PlanetGameObject.transform.Rotate(0,-1, 0);

            //rotateTime�̒l��1�Ȃ瓙���A�傫���Ȃ��������]����
            yield return new WaitForSeconds(1 / (rotateSpeed * 60));
        }

        //�I���������j���[�ɉ������摜��\��
        menuManager.menuDatas[(int)menuManager.nowSelect].menuImage.SetActive(true);

        //���ݑI�𒆂̃��j���[��ۑ�
        menuManager.beforeSelect = menuManager.nowSelect;

        buttonLock = true;
    }

    //����]
    IEnumerator LeftRotation()
    {
        buttonLock = false;
        menuSE.SelectSE();

        //��]�����[�v������if
        if (menuManager.nowSelect <= MenuManager.SelectMenu.Story)
        {
            menuManager.nowSelect = MenuManager.SelectMenu.enumEnd - 1;
        }
        else
        {
            menuManager.nowSelect -= 1;
        }

        //�\������Ă����摜������
        menuManager.menuDatas[(int)menuManager.beforeSelect].menuImage.SetActive(false);


        //�f���̉�]
        for (int i = 0; i < planetDistance; ++i)
        {
            //1�t���[�����̉�]
            PlanetGameObject.transform.Rotate(0, 1, 0);

            //rotateTime�̒l��1�Ȃ瓙���A�傫���Ȃ��������]����
            yield return new WaitForSeconds(1 / (rotateSpeed * 60));
        }

        //�I���������j���[�ɉ������摜��\��
        menuManager.menuDatas[(int)menuManager.nowSelect].menuImage.SetActive(true);

        //���ݑI�𒆂̃��j���[��ۑ�
        menuManager.beforeSelect = menuManager.nowSelect;

        buttonLock = true;
    }
}
