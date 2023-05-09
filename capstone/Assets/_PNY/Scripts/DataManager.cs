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
    private int total;

    void calc()
    {
        total = 0;
        total += GameManager.Count.mushroom * 200;
        total += GameManager.Count.meat * 600;
        total += GameManager.Count.shrimp * 400;
        total += GameManager.Count.vegetable * 100;
        total += GameManager.Count.sausage * 500;
    }
    // Start is called before the first frame update
    void Start()
    {
        calc();
        mushText.GetComponent<TextMeshProUGUI>().text = GameManager.Count.mushroom + "�� �ȷȽ��ϴ�";
        meatText.GetComponent<TextMeshProUGUI>().text = GameManager.Count.meat + "�� �ȷȽ��ϴ�";
        vegetableText.GetComponent<TextMeshProUGUI>().text = GameManager.Count.vegetable + "�� �ȷȽ��ϴ�";
        sausageText.GetComponent<TextMeshProUGUI>().text = GameManager.Count.sausage + "�� �ȷȽ��ϴ�";
        shrimpText.GetComponent<TextMeshProUGUI>().text = GameManager.Count.shrimp + "�� �ȷȽ��ϴ�";

        btnText.GetComponent<TextMeshProUGUI>().text = "3. " + total;
    }
}
