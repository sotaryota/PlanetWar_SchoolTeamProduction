using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearJudge_Solo : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!HasChild(this.gameObject))
        {
            Destroy(gameObject);
        }
    }

    public bool HasChild(GameObject gameObject)
    {
        return 0 < gameObject.transform.childCount;
    }
}
