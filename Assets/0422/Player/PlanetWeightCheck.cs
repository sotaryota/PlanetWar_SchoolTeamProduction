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

        //���Ă鎞�̐F�Ǝ��ĂȂ����̐F��ݒ�
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

        //�f���̈ʒu�Ƀe�L�X�g��\��
        Vector3 screen = RectTransformUtility.WorldToScreenPoint(playerCamera, other.transform.position);
        text.transform.position = screen;
    }
    private void OnTriggerExit(Collider other)
    {
        //�f�����痣�ꂽ��e�L�X�g������
        if(other.gameObject.tag == "Planet")
        {
            text.SetActive(false);
        }
    }
}
