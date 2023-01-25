using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStoryManager : MonoBehaviour
{
    Gamepad gamepad;
    [SerializeField] Write_Effect effect;
    [SerializeField] PlayerStatus_Solo playerStatus;
    [SerializeField] PlayerGroundCheck ground;
    private void Awake()
    {
        if (gamepad == null)
        {
            gamepad = Gamepad.current;
        }
    }
    private void Update()
    {
        if (gamepad == null)
        {
            gamepad = Gamepad.current;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (effect.isTalking) { return;}
        if (!ground.GetComponent<PlayerGroundCheck>().isGround) { return; }
        if (other.tag == "NPC")
        {
            if (!other.GetComponent<NPCClass>().GetTalkFlag()) { return; }

            effect.npc = other.gameObject;
            if (!effect.buttonFlag) { return; }
            if (gamepad.buttonEast.isPressed)
            {
                effect.buttonFlag = false;
                //プレイヤーを会話状態に
                playerStatus.SetState(PlayerStatus_Solo.State.Talking);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "NPC")
        {
            effect.npc = null;
        }
    }
}
