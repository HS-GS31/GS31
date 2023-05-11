using Oculus.Interaction.Grab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabClipboard : MonoBehaviour
{
    public GameObject clipboard;

    public void hideClipboard() // īƮ ���� �ִ� Ŭ������ ������
    {
        Debug.Log("Hide Clipboard");
        clipboard.SetActive(false);
        showCenter(); // ��� Ŭ������ ������
    }

    public void showClipboard() // īƮ ���� �ִ� Ŭ������ ���̰�
    {
        Debug.Log("Show Clipboard");
        GameObject.Find("GameManager").transform.Find("shopping cart").gameObject.SetActive(true);
        clipboard.SetActive(true);
    }

    public void showCenter() // �߾ӿ� ���� Ŭ������ ���̰�
    {
        Debug.Log("Show Center");
        cantGrab();
        GameObject.Find("GameManager").transform.Find("shopping cart").gameObject.SetActive(false);
        GameObject.Find("GameManager").transform.Find("ClipboardCenter").gameObject.SetActive(true);
        Invoke("hideCenter", 5f); // ������ 5�� �ڿ� ������
    }

    public void hideCenter() // �߾ӿ� ���� Ŭ������ ������
    {
        Debug.Log("Hide Center");
        canGrab();
        GameObject.Find("GameManager").transform.Find("ClipboardCenter").gameObject.SetActive(false);
        showClipboard(); // ���� �ٽ� Ŭ������ �߰�
    }

    public void cantGrab()
    {
        Debug.Log("Can't Grab");
        GameObject VagetableStand = GameObject.Find("VagetableStand");
        for (int i =0; i< VagetableStand.transform.childCount; i++)
        {
            if(VagetableStand.transform.GetChild(i).CompareTag("Vegetable"))
                VagetableStand.transform.GetChild(i).GetComponent<MeshCollider>().enabled = false;
        }

        GameObject MeatStand = GameObject.Find("MeatStand");
        for (int i = 0; i < MeatStand.transform.childCount; i++)
        {
            if (MeatStand.transform.GetChild(i).CompareTag("Meat"))
                MeatStand.transform.GetChild(i).GetComponent<MeshCollider>().enabled = false;
        }
    }

    public void canGrab()
    {
        Debug.Log("Can Grab");
        GameObject VagetableStand = GameObject.Find("VagetableStand");
        for (int i = 0; i < VagetableStand.transform.childCount; i++)
        {
            if(VagetableStand.transform.GetChild(i).CompareTag("Vegetable"))
                VagetableStand.transform.GetChild(i).GetComponent<MeshCollider>().enabled = true;
        }

        GameObject MeatStand = GameObject.Find("MeatStand");
        for (int i = 0; i < MeatStand.transform.childCount; i++)
        {
            if (MeatStand.transform.GetChild(i).CompareTag("Meat"))
                MeatStand.transform.GetChild(i).GetComponent<MeshCollider>().enabled = true;
        }
    }

}
