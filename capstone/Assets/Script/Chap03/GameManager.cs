using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //정산하기용
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
    public GameObject FullText;
    public GameObject ChapIntro;
    public GameObject ChapOutro;
    private GameObject selectedStick;
    private GameObject nowMenu;
    private int top;
    private void Start()
    {
        this.ChapOutro.SetActive(false);
        this.selectedStick = null;
        this.nowMenu = null;
        this.top = 1;
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
    public void FullStick()
    {        
        Debug.Log("꼬치가 꽉찼습니다.");
        FullText.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<WarnText>().setActive();
    }
    public void ChapOut()
    {
        ChapOutro.SetActive(true);
    }
    //스틱 선택 확인
    public void setSelectedStick(GameObject stick)
    {
        selectedStick = stick;
    } 
    public bool checkIngred(GameObject ingredient)
    {
        //음식물이 꽉찬경우.
        if (top > 4)
        {
            FullStick();
            return false;
        }
        //현재 필요한 재료와 같다면
        if (ingredient.tag == this.nowMenu.transform.GetChild(top).gameObject.tag)
        {
            top++;
            return true;
        }
        else
        {
            Warn();
            return false;
        }
    }
    public GameObject getSelectedStick()
    {
        return selectedStick;
    }
   
    public void setNowMenu(GameObject menu)
    {
        this.nowMenu = menu;
        top = 1;
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