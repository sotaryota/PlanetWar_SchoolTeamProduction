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
            Debug.Log("NPC�ƐڐG");
            if (!other.GetComponent<NPCClass>().GetTalkFlag()) { return; }

            Debug.Log("NPC����b�\���");
            effect.npc = other.gameObject;
            if (!effect.buttonFlag) { return; }
            Debug.Log("��b�ł��܂�");
            if (gamepad.buttonWest.isPressed)
            {
                Debug.Log("��b�J�n");
                effect.buttonFlag = false;
                //�v���C���[����b��Ԃ�
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
