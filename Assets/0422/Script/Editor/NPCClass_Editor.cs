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
        serializedObject.FindProperty(nameof(NPCClass.eventId)).intValue = EditorGUILayout.IntField("イベントID", serializedObject.FindProperty(nameof(NPCClass.eventId)).intValue);
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        //NPCの現在の状態表示
        //------------------------------------------------------------------------------------------
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("現在の状態");
        EditorGUILayout.LabelField(((NPCClass.NPCState)npc.nowState).ToString(), EditorStyles.label);

        EditorGUILayout.EndHorizontal();
        //------------------------------------------------------------------------------------------
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        //選択肢の有無
        serializedObject.FindProperty(nameof(NPCClass.selectFlag)).boolValue = EditorGUILayout.Toggle("選択肢の有無", serializedObject.FindProperty(nameof(NPCClass.selectFlag)).boolValue);

        if (npc.selectFlag)
        {
            GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
            npc.selectTalkData[(int)NPCClass.SelectNom.First] = EditorGUILayout.TextField("1つ目の選択肢", npc.selectTalkData[(int)NPCClass.SelectNom.First]);
            npc.firstSelectState = (NPCClass.NPCState)EditorGUILayout.EnumPopup("選択後の状態", npc.firstSelectState);
            GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
            npc.selectTalkData[(int)NPCClass.SelectNom.Second] = EditorGUILayout.TextField("2つ目の選択肢", npc.selectTalkData[(int)NPCClass.SelectNom.Second]);
            npc.secondSelectState = (NPCClass.NPCState)EditorGUILayout.EnumPopup("選択後の状態", npc.secondSelectState);
        }
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        //会話可能か否かの判定
        serializedObject.FindProperty(nameof(NPCClass.talkFlag)).boolValue = EditorGUILayout.Toggle("会話可能？", serializedObject.FindProperty(nameof(NPCClass.talkFlag)).boolValue);

        //通常時の会話　一番最初に話しかけたときの内容
        //-----------------------------------------------------------------------------------------------
        EditorGUILayout.LabelField("通常状態の会話");
        EditorGUILayout.BeginHorizontal();
        //配列の長さ変更
        if (npc.normalTalkData.Length > 0)
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
                    npc.normalTalkData = new string[0];
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
        if (npc.normalTalkData.Length > 0)
        {
            for (int i = 0; i < npc.normalTalkData.Length; i++)
            {
                npc.normalTalkData[i] = EditorGUILayout.TextField((i + 1).ToString() + "行目", npc.normalTalkData[i]);
            }

        }//-----------------------------------------------------------------------------------------------
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        //友好状態の時の会話
        //-----------------------------------------------------------------------------------------------
        EditorGUILayout.LabelField("友好状態の時の会話");
        EditorGUILayout.BeginHorizontal();
        //配列の長さ変更
        if (npc.friendTalkData.Length > 0)
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
                    npc.friendTalkData = new string[0];
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
        if (npc.friendTalkData.Length != 0)
        {
            for (int i = 0; i < npc.friendTalkData.Length; i++)
            {
                npc.friendTalkData[i] = EditorGUILayout.TextField((i + 1).ToString() + "行目", npc.friendTalkData[i]);
            }
        }

        //-----------------------------------------------------------------------------------------------
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        //会話終了状態の時の会話
        //-----------------------------------------------------------------------------------------------
        EditorGUILayout.LabelField("友好状態で会話終了");
        EditorGUILayout.BeginHorizontal();
        //配列の長さ変更
        if (npc.FriendEndTalkData.Length > 0)
        {
            if (GUILayout.Button("行を追加", GUILayout.Width(100), GUILayout.Height(20)))
            {
                Array.Resize(ref npc.FriendEndTalkData, npc.FriendEndTalkData.Length + 1);
            }

            if (GUILayout.Button("末尾の行を削除", GUILayout.Width(100), GUILayout.Height(20)))
            {
                if (npc.FriendEndTalkData.Length > 1)
                {
                    Array.Resize(ref npc.FriendEndTalkData, npc.FriendEndTalkData.Length - 1);
                }
                else
                {
                    npc.FriendEndTalkData = new string[0];
                }
            }
        }
        else
        {
            if (GUILayout.Button("このイベントは存在しません", GUILayout.Width(200), GUILayout.Height(20)))
            {
                npc.FriendEndTalkData = new string[1];
            }
        }
        EditorGUILayout.EndHorizontal();
        if (npc.FriendEndTalkData.Length > 0)
        {
            for (int i = 0; i < npc.FriendEndTalkData.Length; i++)
            {
                npc.FriendEndTalkData[i] = EditorGUILayout.TextField((i + 1).ToString() + "行目", npc.FriendEndTalkData[i]);
            }
        }
        //-----------------------------------------------------------------------------------------------
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        //戦闘態勢時の会話
        //-----------------------------------------------------------------------------------------------
        EditorGUILayout.LabelField("戦闘態勢の時の会話");
        EditorGUILayout.BeginHorizontal();
        //配列の長さ変更
        if (npc.battlelTalkData.Length > 0)
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
                    npc.battlelTalkData = new string[0];
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
        if (npc.battlelTalkData.Length > 0)
        {
            for (int i = 0; i < npc.battlelTalkData.Length; i++)
            {
                npc.battlelTalkData[i] = EditorGUILayout.TextField((i + 1).ToString() + "行目", npc.battlelTalkData[i]);
            }
            GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
            npc.enemyName = (EnemyCreater_Data.EnemyName)EditorGUILayout.EnumPopup("戦う敵の種類", npc.enemyName);
        }//-----------------------------------------------------------------------------------------------
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        //会話終了状態の時の会話
        //-----------------------------------------------------------------------------------------------
        EditorGUILayout.LabelField("バトル終了後の会話");
        EditorGUILayout.BeginHorizontal();
        //配列の長さ変更
        if (npc.BattleEndTalkData.Length > 0)
        {
            if (GUILayout.Button("行を追加", GUILayout.Width(100), GUILayout.Height(20)))
            {
                Array.Resize(ref npc.BattleEndTalkData, npc.BattleEndTalkData.Length + 1);
            }

            if (GUILayout.Button("末尾の行を削除", GUILayout.Width(100), GUILayout.Height(20)))
            {
                if (npc.BattleEndTalkData.Length > 1)
                {
                    Array.Resize(ref npc.BattleEndTalkData, npc.BattleEndTalkData.Length - 1);
                }
                else
                {
                    npc.BattleEndTalkData = new string[0];
                }
            }
        }
        else
        {
            if (GUILayout.Button("このイベントは存在しません", GUILayout.Width(200), GUILayout.Height(20)))
            {
                npc.BattleEndTalkData = new string[1];
            }
        }
        EditorGUILayout.EndHorizontal();
        if (npc.BattleEndTalkData.Length > 0)
        {
            for (int i = 0; i < npc.BattleEndTalkData.Length; i++)
            {
                npc.BattleEndTalkData[i] = EditorGUILayout.TextField((i + 1).ToString() + "行目", npc.BattleEndTalkData[i]);
            }
        }
        
        //現在の情報を保存
        serializedObject.ApplyModifiedProperties();
    }
}