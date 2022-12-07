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
        serializedObject.FindProperty(nameof(NPCClass.name)).stringValue = EditorGUILayout.TextField("���O", serializedObject.FindProperty(nameof(NPCClass.name)).stringValue);
        //ID
        serializedObject.FindProperty(nameof(NPCClass.id)).intValue = EditorGUILayout.IntField("ID", serializedObject.FindProperty(nameof(NPCClass.id)).intValue);
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        //NPC�̏�����Ԃƌ��݂̏�ԕ\��
        //------------------------------------------------------------------------------------------
        npc.firstState = (NPCClass.NPCState)EditorGUILayout.EnumPopup("�����̏��", npc.firstState);
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("���݂̏��");
        EditorGUILayout.LabelField(((NPCClass.NPCState)npc.nowState).ToString(), EditorStyles.label);

        EditorGUILayout.EndHorizontal();
        //------------------------------------------------------------------------------------------
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        //�I�����̗L��
        serializedObject.FindProperty(nameof(NPCClass.selectFlag)).boolValue = EditorGUILayout.Toggle("�I�����̗L��", serializedObject.FindProperty(nameof(NPCClass.selectFlag)).boolValue);

        if (npc.selectFlag)
        {
            GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
            npc.selectTalkData[(int)NPCClass.SelectNom.First] = EditorGUILayout.TextField("�I�����̃e�L�X�g", npc.selectTalkData[(int)NPCClass.SelectNom.First]);
            npc.firstSelectState = (NPCClass.NPCState)EditorGUILayout.EnumPopup("�I����̏��", npc.firstSelectState);
            GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
            npc.selectTalkData[(int)NPCClass.SelectNom.Second] = EditorGUILayout.TextField("�I�����̃e�L�X�g", npc.selectTalkData[(int)NPCClass.SelectNom.Second]);
            npc.secondSelectState = (NPCClass.NPCState)EditorGUILayout.EnumPopup("�I����̏��", npc.secondSelectState);
        }
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        //��b�\���ۂ��̔���
        serializedObject.FindProperty(nameof(NPCClass.talkFlag)).boolValue = EditorGUILayout.Toggle("��b�\�H", serializedObject.FindProperty(nameof(NPCClass.talkFlag)).boolValue);

        //�ʏ펞�̉�b�@��ԍŏ��ɘb���������Ƃ��̓��e
        //-----------------------------------------------------------------------------------------------
        EditorGUILayout.LabelField("�ʏ��Ԃ̉�b");
        EditorGUILayout.BeginHorizontal();
        //�z��̒����ύX
        if (npc.normalTalkData.Length > 0)
        {
            if (GUILayout.Button("�s��ǉ�", GUILayout.Width(100), GUILayout.Height(20)))
            {
                Array.Resize(ref npc.normalTalkData, npc.normalTalkData.Length + 1);
            }

            if (GUILayout.Button("�����̍s���폜", GUILayout.Width(100), GUILayout.Height(20)))
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
            if (GUILayout.Button("���̃C�x���g�͑��݂��܂���", GUILayout.Width(200), GUILayout.Height(20)))
            {
                npc.normalTalkData = new string[1];
            }
        }
        EditorGUILayout.EndHorizontal();
        if (npc.normalTalkData.Length > 0)
        {
            for (int i = 0; i < npc.normalTalkData.Length; i++)
            {
                npc.normalTalkData[i] = EditorGUILayout.TextField((i + 1).ToString() + "�s��", npc.normalTalkData[i]);
            }

        }
        //-----------------------------------------------------------------------------------------------
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        //�퓬�Ԑ����̉�b
        //-----------------------------------------------------------------------------------------------
        EditorGUILayout.LabelField("�퓬�Ԑ��̎��̉�b");
        EditorGUILayout.BeginHorizontal();
        //�z��̒����ύX
        if (npc.battlelTalkData.Length > 0)
        {
            if (GUILayout.Button("�s��ǉ�", GUILayout.Width(100), GUILayout.Height(20)))
            {
                Array.Resize(ref npc.battlelTalkData, npc.battlelTalkData.Length + 1);
            }

            if (GUILayout.Button("�����̍s���폜", GUILayout.Width(100), GUILayout.Height(20)))
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
            if (GUILayout.Button("���̃C�x���g�͑��݂��܂���", GUILayout.Width(200), GUILayout.Height(20)))
            {
                npc.battlelTalkData = new string[1];
            }
        }
        EditorGUILayout.EndHorizontal();
        if (npc.battlelTalkData.Length > 0)
        {
            for (int i = 0; i < npc.battlelTalkData.Length; i++)
            {
                npc.battlelTalkData[i] = EditorGUILayout.TextField((i + 1).ToString() + "�s��", npc.battlelTalkData[i]);
            }
        }
        //-----------------------------------------------------------------------------------------------
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        //�F�D��Ԃ̎��̉�b
        //-----------------------------------------------------------------------------------------------
        EditorGUILayout.LabelField("�F�D��Ԃ̎��̉�b");
        EditorGUILayout.BeginHorizontal();
        //�z��̒����ύX
        if (npc.friendTalkData.Length > 0)
        {
            if (GUILayout.Button("�s��ǉ�", GUILayout.Width(100), GUILayout.Height(20)))
            {
                Array.Resize(ref npc.friendTalkData, npc.friendTalkData.Length + 1);
            }

            if (GUILayout.Button("�����̍s���폜", GUILayout.Width(100), GUILayout.Height(20)))
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
            if (GUILayout.Button("���̃C�x���g�͑��݂��܂���", GUILayout.Width(200), GUILayout.Height(20)))
            {
                npc.friendTalkData = new string[1];
            }
        }
        EditorGUILayout.EndHorizontal();
        if (npc.friendTalkData.Length != 0)
        {
            for (int i = 0; i < npc.friendTalkData.Length; i++)
            {
                npc.friendTalkData[i] = EditorGUILayout.TextField((i + 1).ToString() + "�s��", npc.friendTalkData[i]);
            }
        }
        //-----------------------------------------------------------------------------------------------
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        //�F�D��Ԃ̎��̉�b
        //-----------------------------------------------------------------------------------------------
        EditorGUILayout.LabelField("��b�I����Ԃ̎��̉�b");
        EditorGUILayout.BeginHorizontal();
        //�z��̒����ύX
        if (npc.endTalkData.Length > 0)
        {
            if (GUILayout.Button("�s��ǉ�", GUILayout.Width(100), GUILayout.Height(20)))
            {
                Array.Resize(ref npc.endTalkData, npc.endTalkData.Length + 1);
            }

            if (GUILayout.Button("�����̍s���폜", GUILayout.Width(100), GUILayout.Height(20)))
            {
                if (npc.endTalkData.Length > 1)
                {
                    Array.Resize(ref npc.endTalkData, npc.endTalkData.Length - 1);
                }
                else
                {
                    npc.endTalkData = new string[0];
                }
            }
        }
        else
        {
            if (GUILayout.Button("���̃C�x���g�͑��݂��܂���", GUILayout.Width(200), GUILayout.Height(20)))
            {
                npc.endTalkData = new string[1];
            }
        }
        EditorGUILayout.EndHorizontal();
        if (npc.endTalkData.Length > 0)
        {
            for (int i = 0; i < npc.endTalkData.Length; i++)
            {
                npc.endTalkData[i] = EditorGUILayout.TextField((i + 1).ToString() + "�s��", npc.endTalkData[i]);
            }
        }
        //���݂̏���ۑ�
        serializedObject.ApplyModifiedProperties();
    }
}