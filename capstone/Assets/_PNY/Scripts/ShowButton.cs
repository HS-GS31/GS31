using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowButton : MonoBehaviour
{
    public GameObject Menupan;
    public GameObject Sbutton;
    public GameObject Cbutton;
    public GameObject Ebutton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("ButtonTrue", 3.5f);
    }
    public void ButtonTrue()
    {
        Menupan.SetActive(true);
        Sbutton.SetActive(true);
        Cbutton.SetActive(true);
        Ebutton.SetActive(true);
    }
}
