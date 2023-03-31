using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cartoon : MonoBehaviour
{
    public GameObject cut1;
    public GameObject cut2;
    public GameObject cut3;
    public GameObject cut4;
    public GameObject cut5;

    public GameObject text1;
    public GameObject text2;
    public GameObject text3;
    public GameObject text4;
    public GameObject text5;
    public GameObject text6;//��������â

    public GameObject chap1Btn;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CartoonScene());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator CartoonScene()
    {
        Invoke("cut1false", 10f);//10�� �ڿ� ��1 ������
        Invoke("cut2true", 10f);//10�� �� ��2 ������
        Invoke("cut2false", 20f);
        Invoke("cut3true", 20f);
        Invoke("cut3false", 30f);
        Invoke("cut4true", 30f);
        Invoke("cut4false", 40f);
        Invoke("OculusOn", 40f);

        yield return null;
    }

    public void cut1false()//��1, �ڸ�1 ������
    {
        cut1.SetActive(false);
        text1.SetActive(false);
    }

    //��2
    public void cut2true()
    {
        cut2.SetActive(true);
        text2.SetActive(true);
    }
    public void cut2false()//
    {
        cut2.SetActive(false);
        text2.SetActive(false);
    }

    //��3
    public void cut3true()
    {
        cut3.SetActive(true);
        text3.SetActive(true);
    }
    public void cut3false()//
    {
        cut3.SetActive(false);
        text3.SetActive(false);
    }

    //��4
    public void cut4true()
    {
        cut4.SetActive(true);
        text4.SetActive(true);
    }
    public void cut4false()//
    {
        cut4.SetActive(false);
        text4.SetActive(false);
    }

    //��� ���� �˸�
    public void OculusOn()
    {
        cut5.SetActive(true);
        text5.SetActive(true);
        chap1Btn.SetActive(true);
        text6.SetActive(false);
    }
}
