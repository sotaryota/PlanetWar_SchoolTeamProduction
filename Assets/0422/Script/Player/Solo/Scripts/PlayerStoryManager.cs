using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStoryManager : MonoBehaviour
{
    Gamepad gamepad;
    [SerializeField] NPCTalking talk;
    [SerializeField] PlayerStatus_Solo playerStatus;
    [SerializeField] PlayerGroundCheck ground;
    [SerializeField] PauseMenuSystem pause;
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
        if (pause.GetComponent<PauseMenuSystem>().PauseJudge()) { return; }
        if (talk.isTalking) { return;}
        if (!ground.GetComponent<PlayerGroundCheck>().isGround) { return; }
        if (other.tag == "NPC")
        {
            if (!other.GetComponent<NPCClass>().GetTalkFlag()) { return; }

            talk.npc = other.gameObject;
            if (!talk.buttonFlag) { return; }
            if (gamepad.buttonEast.isPressed)
            {
                talk.buttonFlag = false;
                //プレイヤーを会話状態に
                playerStatus.SetState(PlayerStatus_Solo.State.Talking);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "NPC")
        {
            talk.npc = null;
        }
    }
}
