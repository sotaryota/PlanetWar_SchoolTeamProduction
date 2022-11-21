using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ESC : MonoBehaviour
{
    #region �V���O���g��

    private static ESC instance;

    public static ESC Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (ESC)FindObjectOfType(typeof(ESC));

                if (instance == null)
                {
                    Debug.LogError("FadeManager Instance nothing");
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    #endregion

    Keyboard key;
    void Update()
    {
        key = Keyboard.current;

        DebugCommand();
    }

    void DebugCommand()
    {
        //�^�C�g��
        if (key.numpad1Key.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Title");
        }

        //���j���[
        if (key.numpad2Key.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Menu");
        }

        //�`���[�g���A��
        if (key.numpad3Key.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Tutorial");
        }

        //���C���Q�[��
        if (key.numpad4Key.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Main");
        }

        //���U���g
        if(key.numpad5Key.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Result");
        }

        //�X�g�[���[���j���[
        if(key.numpad6Key.wasPressedThisFrame)
        {
            SceneManager.LoadScene("StoryMenu");
        }

        //�X�g�[���[
        if(key.numpad7Key.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Story");
        }
        
        //�X�g�[���[�o�g��
        if(key.numpad8Key.wasPressedThisFrame)
        {
            SceneManager.LoadScene("StoryBattle");
        }

        //�Q�[���I��
        if (key.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
        }
    }
}
