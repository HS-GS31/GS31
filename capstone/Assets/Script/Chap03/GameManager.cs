using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //정상하기용 구조체
    public struct Count
    {
        //음식의 개수
        public int sausage;
        public int vegetable;
        public int mushroom;
        public int meat;
        public int shrimp;
    }

    public GameObject WarningText;
    public GameObject ChapIntro;

    public static Count count;
    
    private void Start()
    {
        WarningText = this.gameObject.transform.GetChild(0).gameObject;
        count.sausage = 0;
        count.vegetable = 0;
        count.mushroom = 0;
        count.meat = 0;
        count.shrimp = 0;
    }

    public void Warn()
    {
        WarningText.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<WarnText>().setActive();
    }
    public void addCount(int menuCode)
    {
        if (menuCode == 0)
        {
            count.meat += 3;
            count.mushroom++;
        }
        else if (menuCode == 1)
        {
            count.shrimp += 3;
            count.vegetable++;
        }
        else if (menuCode == 2)
        {
            count.meat++;
            count.vegetable++;
            count.mushroom++;
            count.sausage++;
        }
        else if (menuCode == 3)
        {
            count.shrimp += 2;
            count.sausage++;
            count.mushroom++;
        }
        else if (menuCode == 4)
        {
            count.shrimp += 2;
            count.meat += 2;
        }
        else
            return;
    }
}
