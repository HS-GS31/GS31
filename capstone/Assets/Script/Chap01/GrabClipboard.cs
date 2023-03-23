using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabClipboard : MonoBehaviour
{
    public void hideClipboard() // īƮ ���� �ִ� Ŭ������ ������
    {
        Debug.Log("Hide Clipboard");
        GameObject.Find("shopping cart").transform.Find("Clipboard").gameObject.SetActive(false);
        showCenter(); // ��� Ŭ������ ������
    }

    public void showClipboard() // īƮ ���� �ִ� Ŭ������ ���̰�
    {
        Debug.Log("Show Clipboard");
        GameObject.Find("GameManager").transform.Find("shopping cart").gameObject.SetActive(true);
        GameObject.Find("shopping cart").transform.Find("Clipboard").gameObject.SetActive(true);
    }

    public void showCenter() // �߾ӿ� ���� Ŭ������ ���̰�
    {
        Debug.Log("Show Center");
        GameObject.Find("GameManager").transform.Find("shopping cart").gameObject.SetActive(false);
        GameObject.Find("GameManager").transform.Find("ClipboardCenter").gameObject.SetActive(true);
        Invoke("hideCenter", 5f); // ������ 5�� �ڿ� ������
    }

    public void hideCenter() // �߾ӿ� ���� Ŭ������ ������
    {
        Debug.Log("Hide Center");
        GameObject.Find("GameManager").transform.Find("ClipboardCenter").gameObject.SetActive(false);
        showClipboard(); // ���� �ٽ� Ŭ������ �߰�
    }

}
