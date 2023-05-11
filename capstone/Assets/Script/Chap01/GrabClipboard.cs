using Oculus.Interaction.Grab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabClipboard : MonoBehaviour
{
    public GameObject clipboard;

    public void hideClipboard() // 카트 왼편에 있는 클립보드 가리기
    {
        Debug.Log("Hide Clipboard");
        clipboard.SetActive(false);
        showCenter(); // 가운데 클립보드 나오게
    }

    public void showClipboard() // 카트 왼편에 있는 클립보드 보이게
    {
        Debug.Log("Show Clipboard");
        GameObject.Find("GameManager").transform.Find("shopping cart").gameObject.SetActive(true);
        clipboard.SetActive(true);
    }

    public void showCenter() // 중앙에 나온 클립보드 보이게
    {
        Debug.Log("Show Center");
        cantGrab();
        GameObject.Find("GameManager").transform.Find("shopping cart").gameObject.SetActive(false);
        GameObject.Find("GameManager").transform.Find("ClipboardCenter").gameObject.SetActive(true);
        Invoke("hideCenter", 5f); // 나오고 5초 뒤에 가리게
    }

    public void hideCenter() // 중앙에 나온 클립보드 가리게
    {
        Debug.Log("Hide Center");
        canGrab();
        GameObject.Find("GameManager").transform.Find("ClipboardCenter").gameObject.SetActive(false);
        showClipboard(); // 왼편에 다시 클립보드 뜨게
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
