using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public enum SelectMenu
    {
        Start,      //�X�^�[�g
        Tutorial,   //�`���[�g���A��
        Title,      //�^�C�g��
        End,        //�Q�[���I��
    };

    [System.Serializable]
    public class MenuData
    {
        public GameObject menuImage;  //�\������摜
        public string sceneName; //�ڍs�������V�[���̖��O
    };

    private Gamepad gamepad;
    [SerializeField] 
    private CameraManager cameraManager;
    [SerializeField]
    private float lockValue;
    public MenuData[] menuDatas;
    public SelectMenu nowSelect;
    public SelectMenu beforeSelect;

    private void Start()
    {
        nowSelect = SelectMenu.Start;
        beforeSelect = nowSelect;
    }

    private void Update()
    {
        if(gamepad == null)
        {
            gamepad = Gamepad.current;
        }

        SceneChange(menuDatas[(int)nowSelect].sceneName);
    }
    /// <summary>
    /// ���j���[�̐؂�ւ������Ă��Ȃ���
    /// �{�^���������ƈ����̖��O�̃V�[���Ɉڍs
    /// End�̏ꍇ�̓Q�[���I��
    /// </summary>
    /// <param name="sceneName">�V�[����</param>
    public void SceneChange(string sceneName)
    {
        if(cameraManager.buttonLock)
        {
            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
                if (sceneName != "End")
                {
                    SceneManager.LoadScene(sceneName);
                }
                else
                {
                    Application.Quit();
                }
            }
        }
    }
}
