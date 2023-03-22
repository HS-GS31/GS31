using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipboardController : MonoBehaviour
{
    public GameObject clipboard;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("HideClipboard", 5f); // ȭ�� �տ� ���̴� Ŭ������ false ó��
        Invoke("ShowShoppingCart", 5f);
    }

    // ����īƮ �������� �ϱ�
    void ShowShoppingCart()
    {
        Debug.Log("Show ShoppingCart");
        GameObject.Find("GameManager").transform.Find("shopping cart").gameObject.SetActive(true);
    }

    void HideClipboard()
    {
        GameObject.Find("GameManager").transform.Find("ClipboardCenter").gameObject.SetActive(false);
    }
}
