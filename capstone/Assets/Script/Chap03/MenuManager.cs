using System.Collections;
using System.Collections.Generic;
using UnityEngine;
struct Food
{
    GameObject menu;        //메뉴 비주얼
    int food_code;          //푸드 코드
    string[] ingredient;    //재료
}

public class MenuManager : MonoBehaviour
{
    GameObject parent;
    Food[] f;
    public GameObject[] menus;
    // Start is called before the first frame update


    public GameObject getRandomFood()
    {
        int rnd = Random.Range(0, 2);           //1~5 랜덤값 생성.
        GameObject order = Instantiate(menus[rnd]);
        return order;
    }
}