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
        public GameObject menuImage;    //�\������摜
        public GameObject selectPlanet; //�I�𒆂̃��j���[�ɑΉ������f��
        public string sceneName;        //�ڍs�������V�[���̖��O
    };

    [SerializeField] 
    private PlanetRotate planetRotate;
    [SerializeField]
    private float lockValue;
    [SerializeField]
    private MenuSEManager menuSE;
    [SerializeField]
    private GameObject effectPrefab;
    private Gamepad gamepad;
    public MenuData[] menuDatas;
    public SelectMenu nowSelect;    //���ݑI������Ă��郁�j���[
    public SelectMenu beforeSelect; //�I�𒆂̃��j���[���ꎞ�ۑ�

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

        DecisionScene();
    }

    /// <summary>
    /// ���j���[�̐؂�ւ������Ă��Ȃ���
    /// �{�^���������ƈ����̖��O�̃V�[���Ɉڍs
    /// End�̏ꍇ�̓Q�[���I��
    /// </summary>
    /// <param name="sceneName">�V�[����</param>
    public void DecisionScene()
    {
        if(planetRotate.buttonLock)
        {
            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
                menuDatas[(int)nowSelect].selectPlanet.SetActive(false);
                GameObject effect = Instantiate(effectPrefab);
                effect.transform.position = menuDatas[(int)nowSelect].selectPlanet.transform.position;
                StartCoroutine("SceneChange", menuDatas[(int)nowSelect].sceneName);
            }
        }
    }


    [SerializeField]
    private float decisionWait;

    IEnumerator SceneChange(string sceneName)
    {
        yield return new WaitForSeconds(decisionWait);

        if (sceneName != "End")
        {
            menuSE.DecisionSE();
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            menuSE.DecisionSE();
            Application.Quit();
        }
    }
}
