using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlSet : MonoBehaviour
{
    ////誰かがログインする度に生成するプレイヤーPrefab
    //public GameObject playerPrefab;

    public Gamepad gamepad;

    void Start()
    {
        if (!PhotonNetwork.connected)   //Phootnに接続されていなければ
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