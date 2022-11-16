using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(EnemyCreater_Data))]
public class EnemyCreaterData_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();//�ŐV��Ԃɏ㏑��
        EnemyCreater_Data ect = target as EnemyCreater_Data;

        //�w���v�{�b�N�X�ƃX�N���v�g�A�h���X
        Color DefColor = GUI.backgroundColor;
        GUI.backgroundColor = Color.green;
        EditorGUILayout.HelpBox("�@�X�N���v�g���J�� �� �uenum EnemyName�v�ɖ��O��ǉ� �� �������", MessageType.Info);
        DrawDefaultInspector();
        GUI.backgroundColor = DefColor;
        EditorGUILayout.Space();

        //�����f�[�^�̕\���A�t�B�[���h�ݒu
        for (int i = 0; i < ect.enemyData.Length && i < (int)EnemyCreater_Data.EnemyName.ENUM_END; ++i)
        {
            //�GEnum�\��
            EditorGUILayout.LabelField(((EnemyCreater_Data.EnemyName)i).ToString(), EditorStyles.largeLabel);

            //�GPrefab���̓t�B�[���h
            ect.enemyData[i].prefab = (GameObject)EditorGUILayout.ObjectField(
             "��������Prefab", ect.enemyData[i].prefab, typeof(GameObject), true);

            //�G���W���̓t�B�[���h
            ect.enemyData[i].pos = EditorGUILayout.Vector3Field("����������W", ect.enemyData[i].pos);

            //�v�f�㉺�ړ�
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("�v�f�̓���ւ�");
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("��Ɠ���ւ�",
                GUILayout.Width(100), GUILayout.Height(20)))
            {
                if(i - 1 >= 0)
                {
                    EnemyCreater_Data.Enemys targetCopy = ect.enemyData[i - 1];
                    ect.enemyData[i - 1] = ect.enemyData[i];
                    ect.enemyData[i] = targetCopy;
                }
                
            }
            else if (GUILayout.Button("���Ɠ���ւ�",
                GUILayout.Width(100), GUILayout.Height(20)))
            {
                if (i + 1 < ect.enemyData.Length)
                {
                    EnemyCreater_Data.Enemys targetCopy = ect.enemyData[i + 1];
                    ect.enemyData[i + 1] = ect.enemyData[i];
                    ect.enemyData[i] = targetCopy;
                }
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
        }

        //�z��̒����ύX
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        //�v�f�𑝂₷�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[
        if(ect.enemyData.Length < (int)EnemyCreater_Data.EnemyName.ENUM_END)
        {
            GUI.backgroundColor = Color.cyan;
        }
        else
        {
            GUI.backgroundColor = Color.black;
        }
        if (GUILayout.Button("�f�[�^��V�K�ǉ�",
            GUILayout.Width(150), GUILayout.Height(20)))
        {
            if (ect.enemyData.Length < (int)EnemyCreater_Data.EnemyName.ENUM_END)
                Array.Resize(ref ect.enemyData, ect.enemyData.Length + 1);
        }
        //�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[

        //�v�f�����炷�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[
        if (ect.enemyData.Length > 0)
        {
            GUI.backgroundColor = Color.magenta;
        }
        else
        {
            GUI.backgroundColor = Color.black;
        }
        if (GUILayout.Button("�Ō���̃f�[�^���폜",
                GUILayout.Width(150), GUILayout.Height(20)))
        {
            if (ect.enemyData.Length > 0)
                Array.Resize(ref ect.enemyData, ect.enemyData.Length - 1);
        }
        //�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[

        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties(); //���݂̏���ۑ�
    }
}