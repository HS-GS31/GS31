using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Chap4Btn : MonoBehaviour
{
    //1234 버튼
    public GameObject btn1;
    public GameObject btn2;
    public GameObject btn3;
    public GameObject btn4;

    //나오는 창
    public GameObject odap;//오답
    public GameObject dap;//답

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //1,2,4버튼을 누르면 오답화면 true, 3초 뒤 false
        //3 버튼 누르면 정답화면 true, 3초 뒤 수고화면 ture

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
