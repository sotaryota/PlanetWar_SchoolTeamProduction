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
        serializedObject.Update();//最新状態に上書き
        EnemyCreater_Data ect = target as EnemyCreater_Data;

        //ヘルプボックスとスクリプトアドレス
        Color DefColor = GUI.backgroundColor;
        GUI.backgroundColor = Color.green;
        EditorGUILayout.HelpBox("　スクリプトを開く → 「enum EnemyName」に名前を追加 → 情報を入力", MessageType.Info);
        DrawDefaultInspector();
        GUI.backgroundColor = DefColor;
        EditorGUILayout.Space();

        //生成データの表示、フィールド設置
        for (int i = 0; i < ect.enemyData.Length && i < (int)EnemyCreater_Data.EnemyName.ENUM_END; ++i)
        {
            //敵Enum表示
            EditorGUILayout.LabelField(((EnemyCreater_Data.EnemyName)i).ToString(), EditorStyles.largeLabel);

            //敵Prefab入力フィールド
            ect.enemyData[i].prefab = (GameObject)EditorGUILayout.ObjectField(
             "生成するPrefab", ect.enemyData[i].prefab, typeof(GameObject), true);

            //敵座標入力フィールド
            ect.enemyData[i].pos = EditorGUILayout.Vector3Field("生成する座標", ect.enemyData[i].pos);

            //要素上下移動
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("要素の入れ替え");
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("上と入れ替え",
                GUILayout.Width(100), GUILayout.Height(20)))
            {
                if(i - 1 >= 0)
                {
                    EnemyCreater_Data.Enemys targetCopy = ect.enemyData[i - 1];
                    ect.enemyData[i - 1] = ect.enemyData[i];
                    ect.enemyData[i] = targetCopy;
                }
                
            }
            else if (GUILayout.Button("下と入れ替え",
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

        //配列の長さ変更
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        //要素を増やすーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
        if(ect.enemyData.Length < (int)EnemyCreater_Data.EnemyName.ENUM_END)
        {
            GUI.backgroundColor = Color.cyan;
        }
        else
        {
            GUI.backgroundColor = Color.black;
        }
        if (GUILayout.Button("データを新規追加",
            GUILayout.Width(150), GUILayout.Height(20)))
        {
            if (ect.enemyData.Length < (int)EnemyCreater_Data.EnemyName.ENUM_END)
                Array.Resize(ref ect.enemyData, ect.enemyData.Length + 1);
        }
        //ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー

        //要素を減らすーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
        if (ect.enemyData.Length > 0)
        {
            GUI.backgroundColor = Color.magenta;
        }
        else
        {
            GUI.backgroundColor = Color.black;
        }
        if (GUILayout.Button("最後尾のデータを削除",
                GUILayout.Width(150), GUILayout.Height(20)))
        {
            if (ect.enemyData.Length > 0)
                Array.Resize(ref ect.enemyData, ect.enemyData.Length - 1);
        }
        //ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー

        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties(); //現在の情報を保存
    }
}