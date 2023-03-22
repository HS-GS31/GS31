using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabClipboard : MonoBehaviour
{
    public void hideClipboard() // 카트 왼편에 있는 클립보드 가리기
    {
        Debug.Log("Hide Clipboard");
        GameObject.Find("shopping cart").transform.Find("Clipboard").gameObject.SetActive(false);
        showCenter(); // 가운데 클립보드 나오게
    }

    public void showClipboard() // 카트 왼편에 있는 클립보드 보이게
    {
        Debug.Log("Show Clipboard");
        GameObject.Find("GameManager").transform.Find("shopping cart").gameObject.SetActive(true);
        GameObject.Find("shopping cart").transform.Find("Clipboard").gameObject.SetActive(true);
    }

    public void showCenter() // 중앙에 나온 클립보드 보이게
    {
        Debug.Log("Show Center");
        GameObject.Find("GameManager").transform.Find("shopping cart").gameObject.SetActive(false);
        GameObject.Find("GameManager").transform.Find("ClipboardCenter").gameObject.SetActive(true);
        Invoke("hideCenter", 5f); // 나오고 5초 뒤에 가리게
    }

    public void hideCenter() // 중앙에 나온 클립보드 가리게
    {
        Debug.Log("Hide Center");
        GameObject.Find("GameManager").transform.Find("ClipboardCenter").gameObject.SetActive(false);
        showClipboard(); // 왼편에 다시 클립보드 뜨게
    }

}
