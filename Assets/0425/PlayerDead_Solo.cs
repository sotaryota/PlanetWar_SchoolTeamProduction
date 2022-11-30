using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead_Solo : MonoBehaviour
{
    [SerializeField]
    PlayerStatus_Solo playerStatus;

    [SerializeField]
    PlayerAnimManeger playerAnimManeger;

    bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerStatus.GetState() == PlayerStatus_Solo.State.Dead)
        {
            if(dead == false)
            {
                playerAnimManeger.PlayAnimDie();
            }
    
           

            dead = true;
        }

        
    }
}
