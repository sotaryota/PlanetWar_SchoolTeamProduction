using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    [SerializeField] PlayerJump jump;
    [SerializeField] PlayerAnimManeger playerAnimation;
    public bool isGround;

    //�n�ʂƂ̐ڐG����
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
            //�W�����v���[�V����
            playerAnimation.PlayAnimSetJump(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGround = false;
            //�W�����v���[�V����
            playerAnimation.PlayAnimSetJump(true);
        }
    }
}
