using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public GameObject mushText;
    public GameObject meatText;
    public GameObject vegetableText;
    public GameObject sausageText;
    public GameObject shrimpText;

    public GameObject btnText;
    public GameObject btnText1;
    public GameObject btnText2;
    public GameObject btnText4;

    private int total;
    private int total1;
    private int total2;
    private int total4;
    void calc()
    {
        total = 0;
        total += GameManager.Count.mushroom * 200;
        total += GameManager.Count.meat * 600;
        total += GameManager.Count.shrimp * 400;
        total += GameManager.Count.vegetable * 100;
        total += GameManager.Count.sausage * 500;

        total1 = total + 1000;
        total2 = total + 500;
        total4 = total - 400;
    }
    // Start is called before the first frame update
    void Start()
    {
        calc();
        mushText.GetComponent<TextMeshProUGUI>().text = GameManager.Count.mushroom + "개 판매";
        meatText.GetComponent<TextMeshProUGUI>().text = GameManager.Count.meat + "개 판매";
        vegetableText.GetComponent<TextMeshProUGUI>().text = GameManager.Count.vegetable + "개 판매";
        sausageText.GetComponent<TextMeshProUGUI>().text = GameManager.Count.sausage + "개 판매";
        shrimpText.GetComponent<TextMeshProUGUI>().text = GameManager.Count.shrimp + "개 판매";

        btnText.GetComponent<TextMeshProUGUI>().text = "3. " + total;
        btnText1.GetComponent<TextMeshProUGUI>().text = "1. " + total1;
        btnText2.GetComponent<TextMeshProUGUI>().text = "2. " + total2;
        btnText4.GetComponent<TextMeshProUGUI>().text = "4. " + total4;
    }
}
