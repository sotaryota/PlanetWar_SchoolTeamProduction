using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueLoad : MonoBehaviour
{
    SaveLoad saveLoad;
    BattleToStory battleToStory;
    [Header("シーン名")]
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
        // メニュー画面では処理をしない
        if (nowScene == menuSceneName) { return; }
    }
    public void Continue(Scene nextScene, LoadSceneMode mode)
    {
        // オープニングならロードせずに消去
        if (nextScene.name == openingSceneName)
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

        SceneManager.sceneLoaded -= Continue;
        // ロードが終了したら消去する
        Destroy(gameObject);
    }
}
