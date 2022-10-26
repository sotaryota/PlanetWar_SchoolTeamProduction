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

    private void Awake()
    {
        buttonLock = true;
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

    //--------------------------------------
    //��]����
    //--------------------------------------

    //��]����
    [SerializeField] private float rotateTime; 

    //�E��]
    IEnumerator RightRotation()
    {
        //��]�����b�N
        buttonLock = false;
        
        //���ʉ�
        menuSE.SelectSE();
        
        //��]�����[�v������if
        if (menuManager.nowSelect >= MenuManager.SelectMenu.End)
        {
            menuManager.nowSelect = MenuManager.SelectMenu.Start;
        }
        else
        {
            menuManager.nowSelect += 1;
        }

        //�\������Ă����摜������
        menuManager.menuDatas[(int)menuManager.beforeSelect].menuImage.SetActive(false);

        //�f���̉�]�ArotateTime�̎��Ԃŉ�]���x��ύX
        for (int i = 0; i < 45; ++i)
        {
            PlanetGameObject.transform.Rotate(0,-2,0);
            yield return new WaitForSeconds(1 / (rotateTime * 60));
        }

        //�I���������j���[�ɉ������摜��\��
        menuManager.menuDatas[(int)menuManager.nowSelect].menuImage.SetActive(true);

        //���ݑI�𒆂̃��j���[��ۑ�
        menuManager.beforeSelect = menuManager.nowSelect;

        //���b�N����
        buttonLock = true;
    }

    //����]
    IEnumerator LeftRotation()
    {
        //��]�����b�N
        buttonLock = false;

        //���ʉ�
        menuSE.SelectSE();

        //��]�����[�v������if
        if (menuManager.nowSelect <= MenuManager.SelectMenu.Start)
        {
            menuManager.nowSelect = MenuManager.SelectMenu.End;
        }
        else
        {
            menuManager.nowSelect -= 1;
        }

        //�\������Ă����摜������
        menuManager.menuDatas[(int)menuManager.beforeSelect].menuImage.SetActive(false);

        //�f���̉�]�ArotateTime�̎��Ԃŉ�]���x��ύX
        for (int i = 0; i < 45; ++i)
        {
            PlanetGameObject.transform.Rotate(0, 2, 0);
            yield return new WaitForSeconds(1 / (rotateTime * 60));
        }

        //�I���������j���[�ɉ������摜��\��
        menuManager.menuDatas[(int)menuManager.nowSelect].menuImage.SetActive(true);

        //���ݑI�𒆂̃��j���[��ۑ�
        menuManager.beforeSelect = menuManager.nowSelect;

        //���b�N����
        buttonLock = true;
    }
}
