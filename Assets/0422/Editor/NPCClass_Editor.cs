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
        //最新状態に上書き
        serializedObject.Update();
        NPCClass npc = target as NPCClass;
        using (new EditorGUI.DisabledGroupScope(true))
        {
            EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((MonoBehaviour)target), typeof(MonoScript), false);
        }
        //NPCの名前入力
        serializedObject.FindProperty(nameof(NPCClass.name)).stringValue = EditorGUILayout.TextField("Enemy name", serializedObject.FindProperty(nameof(NPCClass.name)).stringValue);
        //ID
        serializedObject.FindProperty(nameof(NPCClass.id)).intValue = EditorGUILayout.IntField("ID", serializedObject.FindProperty(nameof(NPCClass.id)).intValue);

        //NPCの初期状態と現在の状態表示
        npc.firstState = (NPCClass.NPCState)EditorGUILayout.EnumPopup("初期の状態", npc.firstState);
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("現在の状態");
        EditorGUILayout.LabelField(((NPCClass.NPCState)npc.nowState).ToString(), EditorStyles.label);

        EditorGUILayout.EndHorizontal();
        //通常時の会話　一番最初に話しかけたときの内容
        //-----------------------------------------------------------------------------------------------
        EditorGUILayout.LabelField("通常状態の会話");
        EditorGUILayout.BeginHorizontal();
        //配列の長さ変更
        if (npc.normalTalk != null)
        {
            if (GUILayout.Button("行を追加", GUILayout.Width(100), GUILayout.Height(20)))
            {
                Array.Resize(ref npc.normalTalk, npc.normalTalk.Length + 1);
            }

            if (GUILayout.Button("末尾の行を削除", GUILayout.Width(100), GUILayout.Height(20)))
            {
                if (npc.normalTalk.Length > 1)
                {
                    Array.Resize(ref npc.normalTalk, npc.normalTalk.Length - 1);
                }
                else
                {
                    npc.normalTalk = null;
                }
            }
        }
        else
        {
            if (GUILayout.Button("このイベントは存在しません", GUILayout.Width(200), GUILayout.Height(20)))
            {
                npc.normalTalk = new string[1];
            }
        }
        EditorGUILayout.EndHorizontal();
        if (npc.normalTalk != null)
        {
            for (int i = 0; i < npc.normalTalk.Length; i++)
            {
                npc.normalTalk[i] = EditorGUILayout.TextField((i + 1).ToString() + "行目", npc.normalTalk[i]);
            }

        }
        //-----------------------------------------------------------------------------------------------
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        //戦闘態勢時の会話
        //-----------------------------------------------------------------------------------------------
        EditorGUILayout.LabelField("戦闘態勢の時の会話");
        EditorGUILayout.BeginHorizontal();
        //配列の長さ変更
        if (npc.battlelTalk != null)
        {
            if (GUILayout.Button("行を追加", GUILayout.Width(100), GUILayout.Height(20)))
            {
                Array.Resize(ref npc.battlelTalk, npc.battlelTalk.Length + 1);
            }

            if (GUILayout.Button("末尾の行を削除", GUILayout.Width(100), GUILayout.Height(20)))
            {
                if (npc.battlelTalk.Length > 1)
                {
                    Array.Resize(ref npc.battlelTalk, npc.battlelTalk.Length - 1);
                }
                else
                {
                    npc.battlelTalk = null;
                }
            }
        }
        else
        {
            if (GUILayout.Button("このイベントは存在しません", GUILayout.Width(200), GUILayout.Height(20)))
            {
                npc.battlelTalk = new string[1];
            }
        }
        EditorGUILayout.EndHorizontal();
        if (npc.battlelTalk != null)
        {
            for (int i = 0; i < npc.battlelTalk.Length; i++)
            {
                npc.battlelTalk[i] = EditorGUILayout.TextField((i + 1).ToString() + "行目", npc.battlelTalk[i]);
            }
        }
        //-----------------------------------------------------------------------------------------------
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        //友好状態の時の会話
        //-----------------------------------------------------------------------------------------------
        EditorGUILayout.LabelField("友好状態の時の会話");
        EditorGUILayout.BeginHorizontal();
        //配列の長さ変更
        if (npc.friendTalk != null)
        {
            if (GUILayout.Button("行を追加", GUILayout.Width(100), GUILayout.Height(20)))
            {
                Array.Resize(ref npc.friendTalk, npc.friendTalk.Length + 1);
            }

            if (GUILayout.Button("末尾の行を削除", GUILayout.Width(100), GUILayout.Height(20)))
            {
                if (npc.friendTalk.Length > 1)
                {
                    Array.Resize(ref npc.friendTalk, npc.friendTalk.Length - 1);
                }
                else
                {
                    npc.friendTalk = null;
                }
            }
        }
        else
        {
            if (GUILayout.Button("このイベントは存在しません", GUILayout.Width(200), GUILayout.Height(20)))
            {
                npc.friendTalk = new string[1];
            }
        }
        EditorGUILayout.EndHorizontal();
        if (npc.friendTalk != null)
        {
            for (int i = 0; i < npc.friendTalk.Length; i++)
            {
                npc.friendTalk[i] = EditorGUILayout.TextField((i + 1).ToString() + "行目", npc.friendTalk[i]);
            }
        }
        //-----------------------------------------------------------------------------------------------

        //現在の情報を保存
        serializedObject.ApplyModifiedProperties();
    }
}