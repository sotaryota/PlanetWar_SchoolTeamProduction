using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    [SerializeField] PlayerJump jump;
    [SerializeField] PlayerAnimManeger playerAnimation;
    public bool isGround;

    //地面との接触判定
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            jump.jumping.y = 0;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGround = true;
            //ジャンプモーション
            playerAnimation.PlayAnimSetJump(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGround = false;
            //ジャンプモーション
            playerAnimation.PlayAnimSetJump(true);
        }
    }
}
