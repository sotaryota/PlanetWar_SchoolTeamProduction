using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStop : MonoBehaviour
{
    [SerializeField]
    MonoBehaviour[] stopScript;

    // Start is called before the first frame update
    void Start()
    {
        ScriptStop();
        StartCoroutine("StartGame");
    }

    IEnumerator StartGame()
    {

        yield return new WaitForSeconds(3);

        ScriptStart();
    }
    void ScriptStart()
    {
        for (int i = 0; i < stopScript.Length; ++i)
        {
            stopScript[i].enabled = true;
        }
    }
    void ScriptStop()
    {
        for (int i = 0; i < stopScript.Length; ++i)
        {
            stopScript[i].enabled = false;
        }
    }
}
