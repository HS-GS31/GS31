using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //정상하기용 구조체
    public static class Count
    {
        //음식의 개수
        public static int sausage;
        public static int vegetable;
        public static int mushroom;
        public static int meat;
        public static int shrimp;
    }

    public GameObject WarningText;
    public GameObject ChapIntro;
    public GameObject ChapOutro;

    private void Start()
    {
        WarningText = this.gameObject.transform.GetChild(0).gameObject;
        ChapOutro.SetActive(false);
        Count.sausage = 0;
        Count.vegetable = 0;
        Count.mushroom = 0;
        Count.meat = 0;
        Count.shrimp = 0;
    }

    public void Warn()
    {
        WarningText.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<WarnText>().setActive();
    }

    public void ChapOut()
    {
        ChapOutro.SetActive(true);
    }
    public void addCount(int menuCode)
    {
        if (menuCode == 0)
        {
            Count.meat += 3;
            Count.mushroom++;
        }
        else if (menuCode == 1)
        {
            Count.shrimp += 3;
            Count.vegetable++;
        }
        else if (menuCode == 2)
        {
            Count.meat++;
            Count.vegetable++;
            Count.mushroom++;
            Count.sausage++;
        }
        else if (menuCode == 3)
        {
            Count.shrimp += 2;
            Count.sausage++;
            Count.mushroom++;
        }
        else if (menuCode == 4)
        {
            Count.shrimp += 2;
            Count.meat += 2;
        }
        else
            return;
    }
}
