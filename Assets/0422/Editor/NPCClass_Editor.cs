using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(NPCClass))]
public class NPCClass_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();//�ŐV��Ԃɏ㏑��
        NPCClass npc = target as NPCClass;

        //�w���v�{�b�N�X�ƃX�N���v�g�A�h���X
        EditorGUILayout.HelpBox("�@�X�N���v�g���J�� �� �uenum EnemyName�v�ɖ��O��ǉ� �� �������", MessageType.Info);
        DrawDefaultInspector();
        EditorGUILayout.Space();

        for(int i = 0; i  < npc.)
    }
}
