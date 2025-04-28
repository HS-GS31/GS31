using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FoodCount
{
    public static FoodCount Instance { get; private set; }

    public static int sausage;
    public static int vegetable;
    public static int mushroom;
    public static int meat;
    public static int shrimp;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private FoodCount()
    {
        sausage = 0;
        vegetable = 0;
        mushroom = 0;
        shrimp = 0;
        meat = 0;
    }
}

public class GameManager : MonoBehaviour
{
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
    }

    public void Warn()
    {
        WarningText.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<WarnText>().setActive();
    }
    public void FullStick()
    {
        FullText.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<WarnText>().setActive();
    }
    public void ChapOut()
    {
        ChapOutro.SetActive(true);
    }

    public void setSelectedStick(GameObject stick)
    {
        selectedStick = stick;
    } 
    public bool checkIngred(GameObject ingredient)
    {
        if (top > 4)
        {
            FullStick();
            return false;
        }

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
        switch (menuCode)
        {
            case 0:
                FoodCount.Instance.meat += 3;
                FoodCount.Instance.mushroom++;
                break;
            case 1:
                FoodCount.Instance.shrimp += 3;
                FoodCount.Instance.vegetable++;
                break;
            case 2:
                FoodCount.Instance.meat++;
                FoodCount.Instance.vegetable++;
                FoodCount.Instance.mushroom++;
                FoodCount.Instance.sausage++;
                break;
            case 3:
                FoodCount.Instance.shrimp += 2;
                FoodCount.Instance.sausage++;
                FoodCount.Instance.mushroom++;
                break;
            case 4:
                FoodCount.Instance.shrimp += 2;
                FoodCount.Instance.meat += 2;
                break;
            default:
                break;
        }
    }
}