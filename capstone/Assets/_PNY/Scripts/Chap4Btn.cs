using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Chap4Btn : MonoBehaviour
{
    //1234 ��ư
    public GameObject btn1;
    public GameObject btn2;
    public GameObject btn3;
    public GameObject btn4;

    //������ â
    public GameObject odap;//����
    public GameObject dap;//��

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //1,2,4��ư�� ������ ����ȭ�� true, 3�� �� false
        //3 ��ư ������ ����ȭ�� true, 3�� �� ����ȭ�� ture

    }
    public void odapOn()
    {
        odap.SetActive(true);
        Invoke("odapfalse", 3f);
    }
    public void odapfalse()
    {
        odap.SetActive(false);
    }
    public void dapOn()
    {
        dap.SetActive(true);
        Invoke("dapfalse", 3f);
    }
    public void dapfalse()
    {
        dap.SetActive(false);
    }
}
