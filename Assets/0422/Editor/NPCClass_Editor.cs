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
        serializedObject.Update();//最新状態に上書き
        NPCClass npc = target as NPCClass;

        //ヘルプボックスとスクリプトアドレス
        EditorGUILayout.HelpBox("　スクリプトを開く → 「enum EnemyName」に名前を追加 → 情報を入力", MessageType.Info);
        DrawDefaultInspector();
        EditorGUILayout.Space();

        for(int i = 0; i  < npc.)
    }
}
