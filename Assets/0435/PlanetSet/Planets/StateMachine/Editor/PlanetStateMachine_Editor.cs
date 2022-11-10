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
        serializedObject.Update();//最新状態に上書き

        PlanetStateMachine psm = target as PlanetStateMachine;

        //射程距離の設定
        psm.middleObject = (GameObject)EditorGUILayout.ObjectField("中心とするオブジェクト（null = Vector3.zero）", psm.middleObject, typeof(GameObject), true);
        psm.deleteDistance = EditorGUILayout.FloatField("射程距離", psm.deleteDistance);

        //最初の状態を設定
        psm.firstState = (PlanetStateMachine.State)EditorGUILayout.EnumPopup("初期の状態", psm.firstState);
        psm.nowState = (PlanetStateMachine.State)EditorGUILayout.EnumPopup("現在の状態", psm.nowState);

        isOpen = EditorGUILayout.Foldout(isOpen, "スクリプト");
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
                if (GUILayout.Button("追加"))
                {
                    Array.Resize(ref psm.data[i].GetScripts(), psm.data[i].GetScriptLength() + 1);
                }
                GUI.backgroundColor = Color.magenta;
                if (GUILayout.Button("削除"))
                {
                    Array.Resize(ref psm.data[i].GetScripts(), psm.data[i].GetScriptLength() - 1);
                }
                GUI.backgroundColor = saveColor;

                EditorGUILayout.Space();
            }

            EditorGUILayout.HelpBox("Prefabを開かず、シーン上で変更を行い、Applyで行うこと", MessageType.Info);
        }

        if (psm.data.Length != (int)PlanetStateMachine.State.GETNUM_NOT_USE)
        {
            EditorGUILayout.HelpBox("スクリプトをアタッチし直してください！", MessageType.Error);
        }
        serializedObject.ApplyModifiedProperties(); //現在の情報を保存
    }
}
