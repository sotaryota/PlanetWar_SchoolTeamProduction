using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetStateFanction : MonoBehaviour
{
    //�p���p
    public virtual PlanetStateMachine.State Fanction(float deltaTime)
    {
        Debug.LogError("�֐����I�[�o�[���C�h����Ă��܂���");
        //�I�[�o�[���C�h����Ă��Ȃ��ꍇ�̓G���[��Ԃ��d�g��
        return PlanetStateMachine.State.Error;
    }
}
