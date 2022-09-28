using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

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
        Non,           //�������

        Destroy,       //�j�󃂁[�h

        GETNUM_NOT_USE,//��Ԃ̐����擾����B���̏�Ԃ��g�p����̂͋֎~
        Now,           //�֐��̖߂�l�Ƃ��Ďg�p�B���݂̏�Ԃ��ێ�������
        Error,         //�G���[��f���o����ԁB���̒l�ɕύX����ƃG���[��f��
    }
    [Header("�������ɐݒ肷����")]
    [SerializeField]
    private State firstState;

    [Header("���݂̏�ԁi���ڕύX�̓f�o�b�O���݂̂ɂ���B�ύX����ChangeState�֐��Łj")]
    [SerializeField]
    private State nowState;

    [System.Serializable]
    private class ScriptData
    {
        [SerializeField]
        PlanetStateFanction[] scripts = new PlanetStateFanction[1];
        public ref PlanetStateFanction GetScript(int num) { return ref scripts[num]; }
        public int GetScriptLength() { return scripts.Length; }
    }

    [Header("��Ԃ��ƂɎg�p����X�N���v�g�B��ԕύX�͉��̂ق����D��x������")]
    [SerializeField]
    private ScriptData[] data = new ScriptData[(int)State.GETNUM_NOT_USE];

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

    [CustomEditor(typeof(PlanetStateMachine))]
    public class PlanetStateMachine_Editor : Editor
    {
        private bool isOpen = false;
        private bool[] isOpenScript = new bool[(int)PlanetStateMachine.State.GETNUM_NOT_USE]; 

        public override void OnInspectorGUI()
        {
            serializedObject.Update();//�ŐV��Ԃɏ㏑��

            PlanetStateMachine psm = target as PlanetStateMachine;

            //�ŏ��̏�Ԃ�ݒ�
            psm.firstState = (State)EditorGUILayout.EnumPopup("�����̏��", psm.firstState);
            psm.nowState = (State)EditorGUILayout.EnumPopup("���݂̏��", psm.nowState);
            
            isOpen = EditorGUILayout.Foldout(isOpen, "�X�N���v�g");
            if (isOpen)
            {
                for(int i = 0; i < psm.data.Length; ++i)
                {
                    EditorGUILayout.LabelField(((State)i).ToString());
                    for(int sc = 0; sc < psm.data[i].GetScriptLength(); ++sc)
                    {
                    }
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();
                }

            }

            serializedObject.ApplyModifiedProperties(); //���݂̏���ۑ�
        }
    }
}
