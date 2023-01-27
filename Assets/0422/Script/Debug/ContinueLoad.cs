using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueLoad : MonoBehaviour
{
    SaveLoad saveLoad;
    BattleToStory battleToStory;
    [Header("�V�[����")]
    public string nowScene;
    [SerializeField] string menuSceneName;     
    [SerializeField] string openingSceneName;   
    [SerializeField] string storySceneName;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        nowScene = SceneManager.GetActiveScene().name;
    }
    private void Update()
    {
        // ���j���[��ʂł͏��������Ȃ�
        if (nowScene == menuSceneName) { return; }
    }
    public void Continue(Scene nextScene, LoadSceneMode mode)
    {
        // �I�[�v�j���O�Ȃ烍�[�h�����ɏ���
        if (nextScene.name == openingSceneName)
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

        SceneManager.sceneLoaded -= Continue;
        // ���[�h���I���������������
        Destroy(gameObject);
    }
}