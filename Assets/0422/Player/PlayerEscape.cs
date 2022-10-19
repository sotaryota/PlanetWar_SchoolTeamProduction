using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEscape : MonoBehaviour
{
    private Gamepad gamepad;
    private Rigidbody rb;

    [Header("参照スクリプト")]
    [SerializeField] private PlayerStatus playerStatus;

    [Header("プロパティ")]
    [SerializeField] private GameObject player;
    [SerializeField] private float escapeSpeed;
    [SerializeField] private float escapeCost;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gamepad == null)
        {
            gamepad = Gamepad.all[playerStatus.GetID()];
        }
        EscapeMove();
    }

    private void EscapeMove()
    {
        //Xボタンを押したとき
        if(gamepad.buttonWest.wasPressedThisFrame)
        {
            //ディフェンスパラメーターが回避コストより多いか
            if(playerStatus.GetDefense() >= escapeCost)
            {
                //向いている方向に加速しコスト分ディフェンスを消費
                rb.AddForce(player.transform.forward * escapeSpeed, ForceMode.Impulse);
                playerStatus.Escape(escapeCost);
            }
        }
    }
}
