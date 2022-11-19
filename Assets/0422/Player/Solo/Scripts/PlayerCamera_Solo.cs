using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera_Solo : MonoBehaviour
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

    [SerializeField]
    float angleUpLimit = 50f; //カメラのY方向の上限
    [SerializeField]
    float angleDownLimit = -40f; //カメラのY方向の下限

    void Start()
    {
        testInputActions = new TestInputActions();
        testInputActions.Enable();

        transform.position = player.transform.position;
    }

    void Update()
    {
        if (gamepad == null) { gamepad = Gamepad.current; }

        //カメラの上下の角度制限
        float angle_x = 180f <= transform.eulerAngles.x ? transform.eulerAngles.x - 360 : transform.eulerAngles.x;
        transform.eulerAngles = new Vector3(
            Mathf.Clamp(angle_x, angleDownLimit, angleUpLimit),
            transform.eulerAngles.y,
            transform.eulerAngles.z
        );

        StickValue();
        rotateCameraAngle();

        transform.position = Vector3.Lerp(transform.position, player.transform.position, followSpeed); //自分に追従
    }

    //private void FixedUpdate()
    //{
    //    transform.position = Vector3.Lerp(transform.position, player.transform.position, followSpeed); //自分に追従
    //}

    private void StickValue()
    {
        horizontal = gamepad.rightStick.x.ReadValue();
        vertical = gamepad.rightStick.y.ReadValue();
    }

    //カメラの回転
    private void rotateCameraAngle()
    {
        //transform.eulerAngles += new Vector3(0, horizontal * rotateSpeed * Time.deltaTime);

        Vector3 angle = new Vector3(
        horizontal * rotateSpeed,
        vertical * rotateSpeed,
        0
        );

        transform.eulerAngles += new Vector3(angle.y, angle.x);
    }
}