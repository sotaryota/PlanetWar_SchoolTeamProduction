using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetStateMachine : MonoBehaviour
{
    private float deleteDistance = 40;

    //�X�e�[�g�}�V���̎���
    public enum State
    {
        Idle,          //�ʏ���
        Catch,         //�L���b�`���
        Throw,         //���Ă����
        Hit,           //�q�b�g��

        GETNUM_NOT_USE,//��Ԃ̐����擾����B���̏�Ԃ��g�p����̂͋֎~

        Non,           //�������
        Destroy,       //�j�󃂁[�h

        Now,           //�֐��̖߂�l�Ƃ��Ďg�p�B���݂̏�Ԃ��ێ�������
        Error,         //�G���[��f���o����ԁB���̒l�ɕύX����ƃG���[��f��
    }
    [Header("�������ɐݒ肷����")]
    public State firstState;

    [Header("���݂̏�ԁi���ڕύX�̓f�o�b�O���݂̂ɂ���B�ύX����ChangeState�֐��Łj")]
    public State nowState;

    [System.Serializable]
    public class ScriptData
    {
        [SerializeField]
        PlanetStateFanction[] scripts = new PlanetStateFanction[1];
        public ref PlanetStateFanction GetScript(int num) { return ref scripts[num]; }
        public ref PlanetStateFanction[] GetScripts() { return ref scripts; }
        public int GetScriptLength() { return scripts.Length; }
    }

    [Header("��Ԃ��ƂɎg�p����X�N���v�g�B��ԕύX�͉��̂ق����D��x������")]
    [SerializeField]
    public ScriptData[] data = new ScriptData[(int)State.GETNUM_NOT_USE];

    // Start is called before the first frame update
    void Start()
    {
        nowState = firstState;
    }

    // Update is called once per frame
    void Update()
    {
        float tdt = Time.deltaTime;//deltaTime

        //�X�N���v�g���s(���̏�Ԃ������Ɍ���)
        State nextState = UpdateStateFanction(tdt);

        //��ԕω�
        ChangeState(nextState);
        //�����ō폜
        PlantDestroyByPos();

        //�j���ԂȂ�j��
        if (nowState == State.Destroy)
        {
            Destroy(gameObject);
        }
    }

    private State UpdateStateFanction(float tdt)
    {
        //��Ԃ����݂ɕύX����
        State Next = State.Now;

        //�X�N���v�g�����s����
        for (int i = 0; i < data[(int)nowState].GetScriptLength(); ++i)
        {
            Next = data[(int)nowState].GetScript(i).Fanction(tdt);
        }
        return Next;
    }

    private void ChangeState(State state)
    {
        if (state != State.Error)//�G���[�ł͂Ȃ��ꍇ�iNow�ȊO�̏ꍇ�j
        {
            if (state != State.Now)//���݂̏�Ԃ��ێ����Ȃ��ꍇ�iNow�ȊO�̏ꍇ�j
            {
                //��ԕύX
                nowState = state;
            }
        }
        else
        {
            Debug.Log("�G���[�F���(State)��Error�ɕύX����܂����B");
        }
    }

    public void SetState(State state)
    {
        if (state != State.Error)//�G���[�ł͂Ȃ��ꍇ�iNow�ȊO�̏ꍇ�j
        {
            if (state != State.Now)//���݂̏�Ԃ��ێ����Ȃ��ꍇ�iNow�ȊO�̏ꍇ�j
            {
                //��ԕύX
                nowState = state;
            }
        }
        else
        {
            Debug.Log("�G���[�F���(State)��Error�ɕύX����܂����B");
        }
    }

    public PlanetStateMachine.State GetState()
    {
        return nowState;
    }

    private void PlantDestroyByPos()
    {
        Vector3 pos = this.transform.position;

        if (pos.x >= deleteDistance ||
            pos.x <= -deleteDistance ||
            pos.z >= deleteDistance ||
            pos.z <= -deleteDistance)
        {
            SetState(State.Destroy);
        }
    }
}
