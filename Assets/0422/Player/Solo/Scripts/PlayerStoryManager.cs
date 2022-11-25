using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerStoryManager : MonoBehaviour
{
    Gamepad gamepad;
    [SerializeField] Write_Effect effect;
    [SerializeField] PlayerStatus_Solo playerStatus;
    private void Update()
    {
        if(gamepad == null)
        {
            gamepad = Gamepad.current;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "NPC")
        {
            Debug.Log("NPCと接触");
            if (!other.GetComponent<NPCClass>().GetTalkFlag()) { return; }

            Debug.Log("NPCが会話可能状態");
            effect.npc = other.gameObject;
            if (!effect.buttonFlag) { return; }
            Debug.Log("会話できます");
            if (gamepad.buttonWest.isPressed)
            {
                Debug.Log("会話開始");
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
