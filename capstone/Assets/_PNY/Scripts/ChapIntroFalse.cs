using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapIntroFalse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UICoroutine());
        Invoke("UIFalse", 7f);
    }
    IEnumerator UICoroutine()
    {
        while(true)
        {
            yield return null;
        }
    }
    void UIFalse()
    {
        this.gameObject.SetActive(false);
    }
}
