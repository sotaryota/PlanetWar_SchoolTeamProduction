using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    [SerializeField] PlayerJump jump;
    [SerializeField] PlayerMove_Solo move;
    [SerializeField] PlayerAnimManeger playerAnimation;
    public bool isGround;  //�ڒn����
    [Header("���C���[")]
    [SerializeField] LayerMask iron;
    [SerializeField] LayerMask sand;
    public AudioClip ironMoveSE;
    public AudioClip sandMoveSE;
    public bool ironGround = false;
    public bool sandGround = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            print("stay");
            print("obj : " + other.gameObject.name);
            print("layer : " + other.gameObject.layer);
            isGround = true;
            //�W�����v���[�V����
            playerAnimation.PlayAnimSetJump(false);
            switch (other.gameObject.layer)
            {
                case 6:
                    move.moveSE = ironMoveSE;
                    break;
                case 3:
                    move.moveSE = sandMoveSE;
                    break;
            }
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
