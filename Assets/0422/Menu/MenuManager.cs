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
        public float effectSize;        //�G�t�F�N�g�̑傫�� 
    };

    [SerializeField] 
    private PlanetRotate planetRotate;
    [SerializeField]
    private float lockValue;
    [SerializeField]
    private MenuSEManager menuSE;
    [SerializeField]
    private GameObject effectPrefab;
    [SerializeField]
    private FadeManager fade;
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
    /// �{�^�����������Ƃ��ɑI��ł���f���̈ʒu��
    /// �����̃G�t�F�N�g���o���ăV�[���ڍs����
    /// </summary>
    public void DecisionScene()
    {
        if(planetRotate.buttonLock)
        {
            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
                menuDatas[(int)nowSelect].selectPlanet.SetActive(false);

                GameObject effect = Instantiate(effectPrefab);

                //�G�t�F�N�g�̃T�C�Y�ƃ|�W�V�������w��
                effect.transform.localScale = new Vector3(menuDatas[(int)nowSelect].effectSize,
                    menuDatas[(int)nowSelect].effectSize, menuDatas[(int)nowSelect].effectSize);
                effect.transform.position = menuDatas[(int)nowSelect].selectPlanet.transform.position;

                menuSE.DecisionSE();

                //�V�[���؂�ւ�
                StartCoroutine("SceneChange", menuDatas[(int)nowSelect].sceneName);
            }
        }
    }


    [SerializeField] private float fadeInterval; // �t�F�[�h�܂ł̊Ԋu
    [SerializeField] private float fadeSpeed;    // �t�F�[�h�̃X�s�[�h
    [SerializeField] private Color fadeColor;    // �t�F�[�h�̃J���[

    IEnumerator SceneChange(string sceneName)
    {
        planetRotate.buttonLock = false;

        yield return new WaitForSeconds(fadeInterval);

        if (sceneName != "End")
        {
            fade.FadeOut(sceneName, fadeColor.r, fadeColor.g, fadeColor.b, fadeSpeed);
        }
        else
        {
            Application.Quit();
        }
    }
}
