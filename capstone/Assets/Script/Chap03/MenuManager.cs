using System.Collections;
using System.Collections.Generic;
using UnityEngine;
struct Food
{
    GameObject menu;        //�޴� ���־�
    int food_code;          //Ǫ�� �ڵ�
    string[] ingredient;    //���
}

public class MenuManager : MonoBehaviour
{
    GameObject parent;
    Food[] f;
    public GameObject[] menus;
    // Start is called before the first frame update


    public GameObject getRandomFood()
    {
        int rnd = Random.Range(0, 2);           //1~5 ������ ����.
        GameObject order = Instantiate(menus[rnd]);
        return order;
    }
}