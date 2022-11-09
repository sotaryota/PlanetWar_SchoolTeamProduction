using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// �f�����������Ă���ۂ̓���(Rigidbody�Ȃ��̏ꍇ)
/// 
/// </summary>

public class PlanetThrowMove : PlanetStateFanction
{
    [Header("�f���̓�����ꂽ�ۂ̑��x")]
    [SerializeField]
    private Vector3 beThrowSpeed;

    [Header("������ꂽ�ۂ̕���(0~360)�B�f���͂��̕����������悤�ɂȂ�")]
    [SerializeField]
    private Vector3 beThrowAngleValue;

    [Header("������ꂽ�ۂ̃G�t�F�N�g")]
    [SerializeField]
    private GameObject effectObject;

    private bool throwOnce;//�ŏ��̂P��̂ݏ������s�킹�邽�߂̃t���O

    public void ThrowMoveSetting(Vector3 speed, Vector3 angle)
    {
        beThrowSpeed = speed;
        beThrowAngleValue = angle;
    }

    private void Start()
    {
        throwOnce = true;
    }

    public override PlanetStateMachine.State Fanction(float deltaTime)
    {
        //=====�ŏ��̂P�t���[���̂ݏ������s��(�v�����Start()�Ɠ��`)=======================================
        if (throwOnce)
        {
            //������������Ɍ�����
            this.transform.rotation = Quaternion.Euler(beThrowAngleValue);
            //�G�t�F�N�g�L����
            if(effectObject)
                effectObject.SetActive(true);

            throwOnce = false;
        }
        //================================================================================================


        //=====�ȉ��A���t���[���������s��(�v�����Update())====================================================

        //�����Ă�������ֈړ�
        this.transform.Translate(beThrowSpeed * Time.deltaTime);
        

        //================================================================================================
        //�I��
        return PlanetStateMachine.State.Now;
    }
}
