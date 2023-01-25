using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueLoad : MonoBehaviour
{
    public string nextSceneName;    
    [SerializeField] string storySceneName;
    [SerializeField] string openingSceneName;
    [SerializeField] string menuSceneName;
    SaveLoad saveLoad;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if (nextSceneName == menuSceneName) { return; }
        if (nextSceneName == openingSceneName)
        {
            print("�I�[�v�j���O����������܂�");
            Destroy(gameObject);
            return;
        }
        print("��[��");
        saveLoad = GameObject.Find("SaveLoad").GetComponent<SaveLoad>();
        saveLoad.Load();
        Destroy(gameObject);
    }
}
