using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEntry : MonoBehaviour
{

    [SerializeField] GameObject animCamera;
    [SerializeField] GameObject playerCamera;

    [SerializeField] bool end = false;
    // Start is called before the first frame update
    void Start()
    {
        playerCamera.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        if(end)
        {
            animCamera.SetActive(false);
            //playerCamera.SetActive(true);
            end = false;
        }
    }
}
