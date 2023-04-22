using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarnText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);               //처음에는 문구 비활성화.   
    }

    public void setActive()
    {
        this.gameObject.SetActive(true);
        Invoke("Hide", 1.5f);
    }
    private void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
