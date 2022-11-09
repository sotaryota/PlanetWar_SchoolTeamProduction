using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetStateFanction : MonoBehaviour
{
    //継承用
    public virtual PlanetStateMachine.State Fanction(float deltaTime)
    {
        Debug.LogError("関数がオーバーライドされていません");
        //オーバーライドされていない場合はエラーを返す仕組み
        return PlanetStateMachine.State.Error;
    }
}
