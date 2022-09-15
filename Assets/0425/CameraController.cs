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

    public float rotateSpeed = 1.0f;  //カメラの回転速度
    private const float angleLimitUp = 60f;     //カメラの角度の上側の上限
    private const float angleLimitDown = -60f;  //カメラの角度の下側の上限

    public GameObject target;  //ロックオン対象
    public bool lockOn1P = false;  //ロックオン中か否か（1P）
    public bool lockOn2P = false;  //ロックオン中か否か（2P）
   
    void Start()
    {
        testInputActions = new TestInputActions();
        testInputActions.Enable();

       transform.position = player.transform.position;

        playerID = playerStatus.GetID();  //プレイヤーIDを取得


    }

    void Update()
    {
        if (gamepad == null)
        {
            gamepad = Gamepad.all[playerID];
        }

        StickValue();

       //カメラの角度制限
        float angle_x = 180f <= transform.eulerAngles.x ? transform.eulerAngles.x - 360 : transform.eulerAngles.x;
        transform.eulerAngles = new Vector3(Mathf.Clamp(angle_x, angleLimitDown, angleLimitUp), transform.eulerAngles.y,transform.eulerAngles.z);

        rotateCmaeraAngle();


        //ロックオン-------------------------------------------------------------------------------
   
        //R1ボタンでロックオンする
        ButtonControl control = gamepad[UnityEngine.InputSystem.LowLevel.GamepadButton.RightShoulder];

        //1P
        if (playerID == 0) 
        {  
            //R1ボタンが押されたら
            if (control.wasPressedThisFrame)
            {
                //ロックオン状態を切り替える
                LockOn(); 
            }
            if (lockOn1P)
            {
                cameraController.transform.LookAt(target.transform.position); //ロックオン対象の方向に向き続ける
            }
        }
        //2P
        else if (playerID == 1) 
        {
            //R1ボタンが押されたら
            if (control.wasPressedThisFrame) 
            {
                print($"[{UnityEngine.InputSystem.LowLevel.GamepadButton.RightShoulder}] 2PisPressed = {control.isPressed}");
                //ロックオン状態を切り替える
                LockOn(); 
            }
            if (lockOn2P)
            {
                cameraController.transform.LookAt(target.transform.position); //ロックオン対象の方向に向き続ける
            }
        }

        //カメラの追従
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

    //カメラの回転
    private void rotateCmaeraAngle()
    {
        transform.eulerAngles += new Vector3(0, horizontal * rotateSpeed * Time.deltaTime);
    }   
}
