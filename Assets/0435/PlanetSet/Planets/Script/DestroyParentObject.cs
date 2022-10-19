using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParentObject : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (!HasChild(this.gameObject))
        {
            Destroy(gameObject);
        }
    }

    public bool HasChild(GameObject gameObject)
    {
        return 0 < gameObject.transform.childCount;
    }
}
