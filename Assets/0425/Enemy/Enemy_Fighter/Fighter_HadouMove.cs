using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter_HadouMove : MonoBehaviour
{
    [Header("��ԑ��x"), SerializeField]
    private float flySpeed;

    [SerializeField]
    GameObject fighter;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(0, 0, flySpeed * Time.deltaTime);

    }
}
