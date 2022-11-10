using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(PlanetStateMachine))]
public class PlanetStateMachine_Editor : Editor
{
    private bool isOpen = false;
    private bool[] isOpenScript = new bool[(int)PlanetStateMachine.State.GETNUM_NOT_USE];

    public override void OnInspectorGUI()
    {
        serializedObject.Update();//�ŐV��Ԃɏ㏑��

        PlanetStateMachine psm = target as PlanetStateMachine;

        //�˒������̐ݒ�
        psm.middleObject = (GameObject)EditorGUILayout.ObjectField("���S�Ƃ���I�u�W�F�N�g�inull = Vector3.zero�j", psm.middleObject, typeof(GameObject), true);
        psm.deleteDistance = EditorGUILayout.FloatField("�˒�����", psm.deleteDistance);

        //�ŏ��̏�Ԃ�ݒ�
        psm.firstState = (PlanetStateMachine.State)EditorGUILayout.EnumPopup("�����̏��", psm.firstState);
        psm.nowState = (PlanetStateMachine.State)EditorGUILayout.EnumPopup("���݂̏��", psm.nowState);

        isOpen = EditorGUILayout.Foldout(isOpen, "�X�N���v�g");
        if (isOpen)
        {
            for (int i = 0; i < psm.data.Length; ++i)
            {
                EditorGUILayout.LabelField(((PlanetStateMachine.State)i).ToString());
                for (int sc = 0; sc < psm.data[i].GetScriptLength(); ++sc)
                {
                    psm.data[i].GetScript(sc) =
                        (PlanetStateFanction)EditorGUILayout.ObjectField(
                            "script" + sc,
                            psm.data[i].GetScript(sc),
                            typeof(PlanetStateFanction),
                            true
                            );
                }

                Color saveColor = GUI.backgroundColor;
                GUI.backgroundColor = Color.cyan;
                if (GUILayout.Button("�ǉ�"))
                {
                    Array.Resize(ref psm.data[i].GetScripts(), psm.data[i].GetScriptLength() + 1);
                }
                GUI.backgroundColor = Color.magenta;
                if (GUILayout.Button("�폜"))
                {
                    Array.Resize(ref psm.data[i].GetScripts(), psm.data[i].GetScriptLength() - 1);
                }
                GUI.backgroundColor = saveColor;

                EditorGUILayout.Space();
            }

            EditorGUILayout.HelpBox("Prefab���J�����A�V�[����ŕύX���s���AApply�ōs������", MessageType.Info);
        }

        if (psm.data.Length != (int)PlanetStateMachine.State.GETNUM_NOT_USE)
        {
            EditorGUILayout.HelpBox("�X�N���v�g���A�^�b�`�������Ă��������I", MessageType.Error);
        }
        serializedObject.ApplyModifiedProperties(); //���݂̏���ۑ�
    }
}
