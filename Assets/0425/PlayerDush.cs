using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerDush : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] PlayerStatus_Solo playerStatus_Solo;
    [SerializeField] PlayerAnimManeger playerAnimManeger;

    [SerializeField] float dushSpeed;
    float speed;

    Gamepad gamepad;

    [SerializeField] GameObject playerFoot; //�����̔���

    // Start is called before the first frame update
    void Start()
    {
        if (gamepad == null) { gamepad = Gamepad.current; }
        if (SceneManager.GetActiveScene().name == "Story" || SceneManager.GetActiveScene().name == "StoryBoss")
        {
            speed = playerStatus_Solo.GetSpeed();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (playerStatus_Solo.GetState() == PlayerStatus_Solo.State.Talking) 
        {
            playerStatus_Solo.SetSpeed(speed);
            playerAnimManeger.PlayAnimSetSprint(false);
            return; 
        }
        if((gamepad.leftStick.ReadValue().x < -0.2f || 0.2f < gamepad.leftStick.ReadValue().x) ||
           (gamepad.leftStick.ReadValue().y < -0.2f || 0.2f < gamepad.leftStick.ReadValue().y))
        {
            if (SceneManager.GetActiveScene().name == "Story" || SceneManager.GetActiveScene().name == "StoryBoss")
            {
                if (playerFoot.GetComponent<PlayerGroundCheck>().isGround)
                {
                    if (gamepad.rightShoulder.isPressed)
                    {
                        print("�_�b�V��");
                        playerStatus_Solo.SetSpeed(dushSpeed);
                        playerAnimManeger.PlayAnimSetSprint(true);
                    }
                    else
                    {
                        playerStatus_Solo.SetSpeed(speed);
                        playerAnimManeger.PlayAnimSetSprint(false);
                    }
                }
            }
        }
        else
        {
            playerStatus_Solo.SetSpeed(speed);
            playerAnimManeger.PlayAnimSetSprint(false);
        }
    }
}
