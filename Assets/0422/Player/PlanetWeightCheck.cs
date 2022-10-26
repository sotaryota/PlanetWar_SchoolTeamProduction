using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetWeightCheck : MonoBehaviour
{
    [SerializeField] Text weightText;
    [SerializeField] GameObject text;
    [SerializeField] Camera playerCamera;
    [SerializeField] Color isCatchColor;
    [SerializeField] Color notCatchColor;

    private void Awake()
    {
        text.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag != "Planet") { return; }
        if(other.GetComponent<PlanetStateMachine>().GetState() != PlanetStateMachine.State.Idle)
        {
            text.SetActive(false);
            return;
        }

        text.SetActive(true);

        //持てる時の色と持てない時の色を設定
        if (this.GetComponent<PlayerStatus>().GetPower() >= other.GetComponent<PlanetData>().GetWeight())
        {
            weightText.color = isCatchColor;
        }
        else
        {
            weightText.color = notCatchColor;
        }

        int weight = Mathf.CeilToInt(other.GetComponent<PlanetData>().GetWeight());
        weightText.text = weight.ToString();

        //惑星の位置にテキストを表示
        Vector3 screen = RectTransformUtility.WorldToScreenPoint(playerCamera, other.transform.position);
        text.transform.position = screen;
    }
    private void OnTriggerExit(Collider other)
    {
        //惑星から離れたらテキストを消す
        if(other.gameObject.tag == "Planet")
        {
            text.SetActive(false);
        }
    }
}
