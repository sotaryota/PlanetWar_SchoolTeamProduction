using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart_Solo : MonoBehaviour
{
    [SerializeField]
    MonoBehaviour[] stopScript;

    [SerializeField]
    GameObject startText;

    // Start is called before the first frame update
    void Start()
    {
        ScriptStop();
        startText.SetActive(false);

        StartCoroutine("StartGame");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartGame()
    {
        startText.SetActive(true);

        yield return new WaitForSeconds(3);

        startText.SetActive(false);

        ScriptStart();
    }

    public void ScriptStart()
    {
        for (int i = 0; i < stopScript.Length; ++i)
        {
            stopScript[i].enabled = true;
        }
    }

    public void ScriptStop()
    {
        for (int i = 0; i < stopScript.Length; ++i)
        {
            stopScript[i].enabled = false;
        }
    }
}
