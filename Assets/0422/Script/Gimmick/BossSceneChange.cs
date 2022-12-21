using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossSceneChange : MonoBehaviour
{
    [Header("フェード")]
    [SerializeField] private FadeManager fade; // スクリプト
    [SerializeField] private float fadeSpeed;  // フェードの速さ
    [SerializeField] private Color fadeColor;  // フェードのカラー
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            fade.FadeSceneChange("StoryBoss", fadeColor.r, fadeColor.g, fadeColor.b, fadeSpeed);
        }
    }
}
