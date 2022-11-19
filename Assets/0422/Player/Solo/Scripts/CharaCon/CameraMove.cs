using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : MonoBehaviour
{
    Gamepad gamepad;
    TestInputActions testInputActions;
    [Header("プレイヤー")]
    [SerializeField] private PlayerStatus_Solo playerStatus;
    [SerializeField] private GameObject player;
    [Header("カメラ")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject cameraController;
    [SerializeField] float rotateSpeed;  //カメラの回転速度
    [SerializeField] float followSpeed;  //カメラの追従速度

    float horizontal;
    float vertical;

    void Start()
    {
        testInputActions = new TestInputActions();
        testInputActions.Enable();

        transform.position = player.transform.position;
    }

    void Update()
    {
        if (gamepad == null) { gamepad = Gamepad.current; }

        StickValue();
        rotateCameraAngle();
    }
    private void StickValue()
    {
        horizontal = gamepad.rightStick.x.ReadValue();
        vertical = gamepad.rightStick.y.ReadValue();
    }

    //カメラの回転
    private void rotateCameraAngle()
    {
        transform.eulerAngles += new Vector3(0, horizontal * rotateSpeed * Time.deltaTime);
    }
}