using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipboardController : MonoBehaviour
{
    public GameObject clipboard;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(clipboard, 5f); // 화면 앞에 보이던 클립보드 삭제
        Invoke("ShowShoppingCart", 5f);
    }

    // 쇼핑카트 보여지게 하기
    void ShowShoppingCart()
    {
        Debug.Log("Show ShoppingCart");
        GameObject.Find("Main Camera").transform.Find("shopping cart").gameObject.SetActive(true);
    }
}
