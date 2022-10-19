using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    [SerializeField]
    PlayerStatus status;

    public enum DeadState
    {
        Collide,
        Fire,
    };

    [SerializeField]
    DeadState deadState;

    public void SetDeadState(DeadState state)
    {
        deadState = state;
    }


    private void Update()
    {
        Dead();
    }

    bool DeadCheck()
    {
        return status.GetState() == PlayerStatus.State.Dead;
    }

    void Dead()
    {
        if (!DeadCheck()) { return; }

        switch (deadState)
        {
            case DeadState.Collide:
                break;
            case DeadState.Fire:
                break;
        }
    }
}

