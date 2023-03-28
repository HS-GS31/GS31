using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MenuManager : MonoBehaviour
{
    GameObject parent;
    int now_menu_code;
    public GameObject[] menus;
    // Start is called before the first frame update

    public GameObject getRandomFood()
    {
        int rnd = Random.Range(0, 2);               //1~5 ·£´ý°ª »ý¼º.
        now_menu_code = rnd;
        GameObject order = Instantiate(menus[rnd]);
        return order;
    }

    public bool checkMenu()
    {
        return false;
    }
}