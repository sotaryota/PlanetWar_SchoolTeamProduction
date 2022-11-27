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
        //�ŐV��Ԃɏ㏑��
        serializedObject.Update();
        NPCClass npc = target as NPCClass;
        using (new EditorGUI.DisabledGroupScope(true))
        {
            EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((MonoBehaviour)target), typeof(MonoScript), false);
        }
        //NPC�̖��O����
        serializedObject.FindProperty(nameof(NPCClass.name)).stringValue = EditorGUILayout.TextField("Enemy name", serializedObject.FindProperty(nameof(NPCClass.name)).stringValue);
        //ID
        serializedObject.FindProperty(nameof(NPCClass.id)).intValue = EditorGUILayout.IntField("ID", serializedObject.FindProperty(nameof(NPCClass.id)).intValue);

        //NPC�̏�����Ԃƌ��݂̏�ԕ\��
        npc.firstState = (NPCClass.NPCState)EditorGUILayout.EnumPopup("�����̏��", npc.firstState);
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("���݂̏��");
        EditorGUILayout.LabelField(((NPCClass.NPCState)npc.nowState).ToString(), EditorStyles.label);

        EditorGUILayout.EndHorizontal();
        //�ʏ펞�̉�b�@��ԍŏ��ɘb���������Ƃ��̓��e
        //-----------------------------------------------------------------------------------------------
        EditorGUILayout.LabelField("�ʏ��Ԃ̉�b");
        EditorGUILayout.BeginHorizontal();
        //�z��̒����ύX
        if (npc.normalTalk != null)
        {
            if (GUILayout.Button("�s��ǉ�", GUILayout.Width(100), GUILayout.Height(20)))
            {
                Array.Resize(ref npc.normalTalk, npc.normalTalk.Length + 1);
            }

            if (GUILayout.Button("�����̍s���폜", GUILayout.Width(100), GUILayout.Height(20)))
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
            if (GUILayout.Button("���̃C�x���g�͑��݂��܂���", GUILayout.Width(200), GUILayout.Height(20)))
            {
                npc.normalTalk = new string[1];
            }
        }
        EditorGUILayout.EndHorizontal();
        if (npc.normalTalk != null)
        {
            for (int i = 0; i < npc.normalTalk.Length; i++)
            {
                npc.normalTalk[i] = EditorGUILayout.TextField((i + 1).ToString() + "�s��", npc.normalTalk[i]);
            }

        }
        //-----------------------------------------------------------------------------------------------
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        //�퓬�Ԑ����̉�b
        //-----------------------------------------------------------------------------------------------
        EditorGUILayout.LabelField("�퓬�Ԑ��̎��̉�b");
        EditorGUILayout.BeginHorizontal();
        //�z��̒����ύX
        if (npc.battlelTalk != null)
        {
            if (GUILayout.Button("�s��ǉ�", GUILayout.Width(100), GUILayout.Height(20)))
            {
                Array.Resize(ref npc.battlelTalk, npc.battlelTalk.Length + 1);
            }

            if (GUILayout.Button("�����̍s���폜", GUILayout.Width(100), GUILayout.Height(20)))
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
            if (GUILayout.Button("���̃C�x���g�͑��݂��܂���", GUILayout.Width(200), GUILayout.Height(20)))
            {
                npc.battlelTalk = new string[1];
            }
        }
        EditorGUILayout.EndHorizontal();
        if (npc.battlelTalk != null)
        {
            for (int i = 0; i < npc.battlelTalk.Length; i++)
            {
                npc.battlelTalk[i] = EditorGUILayout.TextField((i + 1).ToString() + "�s��", npc.battlelTalk[i]);
            }
        }
        //-----------------------------------------------------------------------------------------------
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        //�F�D��Ԃ̎��̉�b
        //-----------------------------------------------------------------------------------------------
        EditorGUILayout.LabelField("�F�D��Ԃ̎��̉�b");
        EditorGUILayout.BeginHorizontal();
        //�z��̒����ύX
        if (npc.friendTalk != null)
        {
            if (GUILayout.Button("�s��ǉ�", GUILayout.Width(100), GUILayout.Height(20)))
            {
                Array.Resize(ref npc.friendTalk, npc.friendTalk.Length + 1);
            }

            if (GUILayout.Button("�����̍s���폜", GUILayout.Width(100), GUILayout.Height(20)))
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
            if (GUILayout.Button("���̃C�x���g�͑��݂��܂���", GUILayout.Width(200), GUILayout.Height(20)))
            {
                npc.friendTalk = new string[1];
            }
        }
        EditorGUILayout.EndHorizontal();
        if (npc.friendTalk != null)
        {
            for (int i = 0; i < npc.friendTalk.Length; i++)
            {
                npc.friendTalk[i] = EditorGUILayout.TextField((i + 1).ToString() + "�s��", npc.friendTalk[i]);
            }
        }
        //-----------------------------------------------------------------------------------------------

        //���݂̏���ۑ�
        serializedObject.ApplyModifiedProperties();
    }
}