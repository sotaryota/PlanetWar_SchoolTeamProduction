using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueLoad : MonoBehaviour
{
    SaveLoad saveLoad;
    BattleToStory battleToStory;
    [SerializeField] FadeManager fadeManager;
    [SerializeField] float fadeInSpeed;             // �t�F�[�h�̃X�s�[�h
    [SerializeField] private float fadeInInterval;  // �t�F�[�h�J�n�܂ł̃C���^�[�o��
    [Header("�V�[����")]
    public string nowScene;
    [SerializeField] string openingSceneName;   
    [SerializeField] string storySceneName;
    [SerializeField] string menuSceneName;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        nowScene = SceneManager.GetActiveScene().name;
    }
    public void Continue(Scene nextScene, LoadSceneMode mode)
    {
        // �I�[�v�j���O�Ȃ烍�[�h�����ɏ���
        if (nextScene.name == openingSceneName || nextScene.name == menuSceneName)
        {
            Destroy(gameObject);
            SceneManager.sceneLoaded -= Continue;
            return;
        }

        // �R���e�B�j���[����
        battleToStory = GameObject.Find("StoryManager").GetComponent<BattleToStory>();
        battleToStory.continueFlag = true;

        // �I�u�W�F�N�g�̎Q�Ƃƃf�[�^�̃��[�h
        saveLoad = GameObject.Find("SaveLoad").GetComponent<SaveLoad>();
        saveLoad.Load();


        fadeManager = GameObject.Find("FadeManager").GetComponent<FadeManager>();
        fadeManager.SceneFadeIn(0.0f, 0.0f, 0.0f, fadeInSpeed, fadeInInterval);
        SceneManager.sceneLoaded -= Continue;
        // ���[�h���I���������������
        Destroy(gameObject);
    }
}
