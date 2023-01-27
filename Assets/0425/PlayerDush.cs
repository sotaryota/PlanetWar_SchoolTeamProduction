using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDush : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] PlayerStatus_Solo playerStatus_Solo;
    [SerializeField] PlayerAnimManeger playerAnimManeger;

    [SerializeField] float dushSpeed;
    float speed;

    Gamepad gamepad;

    // Start is called before the first frame update
    void Start()
    {
        if (gamepad == null) { gamepad = Gamepad.current; }
        speed = playerStatus_Solo.GetSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        if(gamepad.rightShoulder.isPressed)
        {
            print("ƒ_ƒbƒVƒ…");
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
