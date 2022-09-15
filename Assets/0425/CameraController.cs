using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class CameraController : MonoBehaviour
{
    Gamepad gamepad;
    TestInputActions testInputActions;
    public PlayerStatus playerStatus;
    int playerID;

    public GameObject player;
    public GameObject cameraController; 

    private float horizontal;
    private float vertical;

    public float rotateSpeed = 1.0f;  //�J�����̉�]���x
    private const float angleLimitUp = 60f;     //�J�����̊p�x�̏㑤�̏��
    private const float angleLimitDown = -60f;  //�J�����̊p�x�̉����̏��

    public GameObject target;  //���b�N�I���Ώ�
    public bool lockOn1P = false;  //���b�N�I�������ۂ��i1P�j
    public bool lockOn2P = false;  //���b�N�I�������ۂ��i2P�j
   
    void Start()
    {
        testInputActions = new TestInputActions();
        testInputActions.Enable();

       transform.position = player.transform.position;

        playerID = playerStatus.GetID();  //�v���C���[ID���擾


    }

    void Update()
    {
        if (gamepad == null)
        {
            gamepad = Gamepad.all[playerID];
        }

        StickValue();

       //�J�����̊p�x����
        float angle_x = 180f <= transform.eulerAngles.x ? transform.eulerAngles.x - 360 : transform.eulerAngles.x;
        transform.eulerAngles = new Vector3(Mathf.Clamp(angle_x, angleLimitDown, angleLimitUp), transform.eulerAngles.y,transform.eulerAngles.z);

        rotateCmaeraAngle();


        //���b�N�I��-------------------------------------------------------------------------------
   
        //R1�{�^���Ń��b�N�I������
        ButtonControl control = gamepad[UnityEngine.InputSystem.LowLevel.GamepadButton.RightShoulder];

        //1P
        if (playerID == 0) 
        {  
            //R1�{�^���������ꂽ��
            if (control.wasPressedThisFrame)
            {
                //���b�N�I����Ԃ�؂�ւ���
                LockOn(); 
            }
            if (lockOn1P)
            {
                cameraController.transform.LookAt(target.transform.position); //���b�N�I���Ώۂ̕����Ɍ���������
            }
        }
        //2P
        else if (playerID == 1) 
        {
            //R1�{�^���������ꂽ��
            if (control.wasPressedThisFrame) 
            {
                print($"[{UnityEngine.InputSystem.LowLevel.GamepadButton.RightShoulder}] 2PisPressed = {control.isPressed}");
                //���b�N�I����Ԃ�؂�ւ���
                LockOn(); 
            }
            if (lockOn2P)
            {
                cameraController.transform.LookAt(target.transform.position); //���b�N�I���Ώۂ̕����Ɍ���������
            }
        }

        //�J�����̒Ǐ]
        transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * 5.0f);
    }

    void LockOn()
    {
        if (playerID == 0) //1P
        {
            if (lockOn1P == false)
                lockOn1P = true;
            else
                lockOn1P = false;
        }
        else
        if (playerID == 1) //2P
        {
            if (lockOn2P == false)
                lockOn2P = true;
            else
                lockOn2P = false;
        }
    } 


    private void StickValue()
    {
        horizontal = gamepad.rightStick.x.ReadValue();
        vertical = gamepad.rightStick.y.ReadValue();
    }

    //�J�����̉�]
    private void rotateCmaeraAngle()
    {
        transform.eulerAngles += new Vector3(0, horizontal * rotateSpeed * Time.deltaTime);
    }   
}
