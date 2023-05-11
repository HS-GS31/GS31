using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipboardController : MonoBehaviour
{
    public GameObject clipboard;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("GameManager").transform.Find("shopping cart").gameObject.SetActive(false);
        Invoke("ShowClipboard", 7f); // 챕터 소개 UI 사라졌을 때 등장하게
        GetComponent<GrabClipboard>().cantGrab();

        Invoke("HideClipboard", 12f); // 화면 앞에 보이던 클립보드 false 처리
        Invoke("ShowShoppingCart", 12f);
    }

    // 클립보드 보여지게 하기
    void ShowClipboard()
    {
        GameObject.Find("GameManager").transform.Find("ClipboardCenter").gameObject.SetActive(true);
    }

    // 쇼핑카트 보여지게 하기
    void ShowShoppingCart()
    {
        Debug.Log("Show ShoppingCart");
        GameObject.Find("GameManager").transform.Find("shopping cart").gameObject.SetActive(true);
        GetComponent<GrabClipboard>().canGrab();
    }

    void HideClipboard()
    {
        GameObject.Find("GameManager").transform.Find("ClipboardCenter").gameObject.SetActive(false);
    }
}
