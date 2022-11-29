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
        serializedObject.FindProperty(nameof(NPCClass.name)).stringValue = EditorGUILayout.TextField("名前", serializedObject.FindProperty(nameof(NPCClass.name)).stringValue);
        //ID
        serializedObject.FindProperty(nameof(NPCClass.id)).intValue = EditorGUILayout.IntField("ID", serializedObject.FindProperty(nameof(NPCClass.id)).intValue);

        //NPCの初期状態と現在の状態表示
        //------------------------------------------------------------------------------------------
        npc.firstState = (NPCClass.NPCState)EditorGUILayout.EnumPopup("初期の状態", npc.firstState);
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("現在の状態");
        EditorGUILayout.LabelField(((NPCClass.NPCState)npc.nowState).ToString(), EditorStyles.label);

        EditorGUILayout.EndHorizontal();
        //------------------------------------------------------------------------------------------
        
        //選択肢の有無
        serializedObject.FindProperty(nameof(NPCClass.selectFlag)).boolValue = EditorGUILayout.Toggle("選択肢の有無", serializedObject.FindProperty(nameof(NPCClass.selectFlag)).boolValue);

        if (npc.selectFlag)
        {
            npc.selectTalkData[0] = EditorGUILayout.TextField("YESの選択肢", npc.selectTalkData[0]);
            npc.selectTalkData[1] = EditorGUILayout.TextField("NOの選択肢", npc.selectTalkData[1]);
        }

        //会話可能か否かの判定
        serializedObject.FindProperty(nameof(NPCClass.talkFlag)).boolValue = EditorGUILayout.Toggle("会話可能？", serializedObject.FindProperty(nameof(NPCClass.talkFlag)).boolValue);

        //通常時の会話　一番最初に話しかけたときの内容
        //-----------------------------------------------------------------------------------------------
        EditorGUILayout.LabelField("通常状態の会話");
        EditorGUILayout.BeginHorizontal();
        //配列の長さ変更
        if (npc.normalTalkData != null)
        {
            if (GUILayout.Button("行を追加", GUILayout.Width(100), GUILayout.Height(20)))
            {
                Array.Resize(ref npc.normalTalkData, npc.normalTalkData.Length + 1);
            }

            if (GUILayout.Button("末尾の行を削除", GUILayout.Width(100), GUILayout.Height(20)))
            {
                if (npc.normalTalkData.Length > 1)
                {
                    Array.Resize(ref npc.normalTalkData, npc.normalTalkData.Length - 1);
                }
                else
                {
                    npc.normalTalkData = null;
                }
            }
        }
        else
        {
            if (GUILayout.Button("このイベントは存在しません", GUILayout.Width(200), GUILayout.Height(20)))
            {
                npc.normalTalkData = new string[1];
            }
        }
        EditorGUILayout.EndHorizontal();
        if (npc.normalTalkData != null)
        {
            for (int i = 0; i < npc.normalTalkData.Length; i++)
            {
                npc.normalTalkData[i] = EditorGUILayout.TextField((i + 1).ToString() + "行目", npc.normalTalkData[i]);
            }

        }
        //-----------------------------------------------------------------------------------------------
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        //戦闘態勢時の会話
        //-----------------------------------------------------------------------------------------------
        EditorGUILayout.LabelField("戦闘態勢の時の会話");
        EditorGUILayout.BeginHorizontal();
        //配列の長さ変更
        if (npc.battlelTalkData != null)
        {
            if (GUILayout.Button("行を追加", GUILayout.Width(100), GUILayout.Height(20)))
            {
                Array.Resize(ref npc.battlelTalkData, npc.battlelTalkData.Length + 1);
            }

            if (GUILayout.Button("末尾の行を削除", GUILayout.Width(100), GUILayout.Height(20)))
            {
                if (npc.battlelTalkData.Length > 1)
                {
                    Array.Resize(ref npc.battlelTalkData, npc.battlelTalkData.Length - 1);
                }
                else
                {
                    npc.battlelTalkData = null;
                }
            }
        }
        else
        {
            if (GUILayout.Button("このイベントは存在しません", GUILayout.Width(200), GUILayout.Height(20)))
            {
                npc.battlelTalkData = new string[1];
            }
        }
        EditorGUILayout.EndHorizontal();
        if (npc.battlelTalkData != null)
        {
            for (int i = 0; i < npc.battlelTalkData.Length; i++)
            {
                npc.battlelTalkData[i] = EditorGUILayout.TextField((i + 1).ToString() + "行目", npc.battlelTalkData[i]);
            }
        }
        //-----------------------------------------------------------------------------------------------
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        //友好状態の時の会話
        //-----------------------------------------------------------------------------------------------
        EditorGUILayout.LabelField("友好状態の時の会話");
        EditorGUILayout.BeginHorizontal();
        //配列の長さ変更
        if (npc.friendTalkData != null)
        {
            if (GUILayout.Button("行を追加", GUILayout.Width(100), GUILayout.Height(20)))
            {
                Array.Resize(ref npc.friendTalkData, npc.friendTalkData.Length + 1);
            }

            if (GUILayout.Button("末尾の行を削除", GUILayout.Width(100), GUILayout.Height(20)))
            {
                if (npc.friendTalkData.Length > 1)
                {
                    Array.Resize(ref npc.friendTalkData, npc.friendTalkData.Length - 1);
                }
                else
                {
                    npc.friendTalkData = null;
                }
            }
        }
        else
        {
            if (GUILayout.Button("このイベントは存在しません", GUILayout.Width(200), GUILayout.Height(20)))
            {
                npc.friendTalkData = new string[1];
            }
        }
        EditorGUILayout.EndHorizontal();
        if (npc.friendTalkData != null)
        {
            for (int i = 0; i < npc.friendTalkData.Length; i++)
            {
                npc.friendTalkData[i] = EditorGUILayout.TextField((i + 1).ToString() + "行目", npc.friendTalkData[i]);
            }
        }
        //-----------------------------------------------------------------------------------------------
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        //友好状態の時の会話
        //-----------------------------------------------------------------------------------------------
        EditorGUILayout.LabelField("会話終了状態の時の会話");
        EditorGUILayout.BeginHorizontal();
        //配列の長さ変更
        if (npc.endTalkData != null)
        {
            if (GUILayout.Button("行を追加", GUILayout.Width(100), GUILayout.Height(20)))
            {
                Array.Resize(ref npc.endTalkData, npc.endTalkData.Length + 1);
            }

            if (GUILayout.Button("末尾の行を削除", GUILayout.Width(100), GUILayout.Height(20)))
            {
                if (npc.endTalkData.Length > 1)
                {
                    Array.Resize(ref npc.endTalkData, npc.endTalkData.Length - 1);
                }
                else
                {
                    npc.endTalkData = null;
                }
            }
        }
        else
        {
            if (GUILayout.Button("このイベントは存在しません", GUILayout.Width(200), GUILayout.Height(20)))
            {
                npc.endTalkData = new string[1];
            }
        }
        EditorGUILayout.EndHorizontal();
        if (npc.endTalkData != null)
        {
            for (int i = 0; i < npc.endTalkData.Length; i++)
            {
                npc.endTalkData[i] = EditorGUILayout.TextField((i + 1).ToString() + "行目", npc.endTalkData[i]);
            }
        }
        //現在の情報を保存
        serializedObject.ApplyModifiedProperties();
    }
}