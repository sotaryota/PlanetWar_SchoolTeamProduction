using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossSceneChange : MonoBehaviour
{
    [Header("�t�F�[�h")]
    [SerializeField] private FadeManager fade; // �X�N���v�g
    [SerializeField] private float fadeSpeed;  // �t�F�[�h�̑���
    [SerializeField] private Color fadeColor;  // �t�F�[�h�̃J���[
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            fade.FadeSceneChange("StoryBoss", fadeColor.r, fadeColor.g, fadeColor.b, fadeSpeed);
        }
    }
}
