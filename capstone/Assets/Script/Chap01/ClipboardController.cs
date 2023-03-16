using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipboardController : MonoBehaviour
{
    public GameObject clipboard;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(clipboard, 5f); // ȭ�� �տ� ���̴� Ŭ������ ����
        Invoke("ShowShoppingCart", 5f);
    }

    // ����īƮ �������� �ϱ�
    void ShowShoppingCart()
    {
        Debug.Log("Show ShoppingCart");
        GameObject.Find("Main Camera").transform.Find("shopping cart").gameObject.SetActive(true);
    }
}
