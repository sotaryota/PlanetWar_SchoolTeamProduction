using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDataManager : MonoBehaviour
{
    #region ƒVƒ“ƒOƒ‹ƒgƒ“

    private static SceneDataManager sceneInstance;

    public static SceneDataManager SceneInstance
    {
        get
        {
            if (sceneInstance == null)
            {
                sceneInstance = (SceneDataManager)FindObjectOfType(typeof(SceneDataManager));

                if (sceneInstance == null)
                {
                    Debug.LogError("SceneDataManager Instance nothing");
                }
            }

            return sceneInstance;
        }
    }
    private void Awake()
    {
        if (this != SceneInstance)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    #endregion

    public void BattleSceneChange()
    {
        SceneManager.LoadScene("StoryBattle");
    }
}
