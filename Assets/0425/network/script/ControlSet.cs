using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlSet : MonoBehaviour
{
    ////�N�������O�C������x�ɐ�������v���C���[Prefab
    //public GameObject playerPrefab;

    public Gamepad gamepad;

    void Start()
    {
        if (!PhotonNetwork.connected)   //Phootn�ɐڑ�����Ă��Ȃ����
        { return; }

        if (Gamepad.all[0] == null)
        {
            gamepad = Gamepad.all[0];
        }
        else
        {
            gamepad = Gamepad.all[1];
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}