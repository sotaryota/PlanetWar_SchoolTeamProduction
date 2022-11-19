using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : MonoBehaviour
{
    Gamepad gamepad;
    TestInputActions testInputActions;
    [Header("�v���C���[")]
    [SerializeField] private PlayerStatus_Solo playerStatus;
    [SerializeField] private GameObject player;
    [Header("�J����")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject cameraController;
    [SerializeField] float rotateSpeed;  //�J�����̉�]���x
    [SerializeField] float followSpeed;  //�J�����̒Ǐ]���x

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

    //�J�����̉�]
    private void rotateCameraAngle()
    {
        transform.eulerAngles += new Vector3(0, horizontal * rotateSpeed * Time.deltaTime);
    }
}