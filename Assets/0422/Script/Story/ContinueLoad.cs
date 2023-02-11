using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueLoad : MonoBehaviour
{
    SaveLoad saveLoad;
    BattleToStory battleToStory;
    [SerializeField] FadeManager fadeManager;
    [SerializeField] float fadeInSpeed;             // フェードのスピード
    [SerializeField] private float fadeInInterval;  // フェード開始までのインターバル
    [Header("シーン名")]
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
        // オープニングならロードせずに消去
        if (nextScene.name == openingSceneName || nextScene.name == menuSceneName)
        {
            Destroy(gameObject);
            SceneManager.sceneLoaded -= Continue;
            return;
        }

        // コンティニュー時に
        battleToStory = GameObject.Find("StoryManager").GetComponent<BattleToStory>();
        battleToStory.continueFlag = true;

        // オブジェクトの参照とデータのロード
        saveLoad = GameObject.Find("SaveLoad").GetComponent<SaveLoad>();
        saveLoad.Load();


        fadeManager = GameObject.Find("FadeManager").GetComponent<FadeManager>();
        fadeManager.SceneFadeIn(0.0f, 0.0f, 0.0f, fadeInSpeed, fadeInInterval);
        SceneManager.sceneLoaded -= Continue;
        // ロードが終了したら消去する
        Destroy(gameObject);
    }
}
