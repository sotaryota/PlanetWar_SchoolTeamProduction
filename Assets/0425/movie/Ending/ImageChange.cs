using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChange : MonoBehaviour
{
    [SerializeField] Sprite[] imageArray;
    [SerializeField] int imageNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Image>().sprite = imageArray[imageNum];
    }
}
