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
    Food[] f;
    public GameObject[] menus;
    // Start is called before the first frame update
    void Start()
    {
               
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject getRandomFood()
    {
        int rnd = Random.Range(0, 5);           //1~5 ������ ����.
        GameObject order = Instantiate(menus[rnd]);
        return order;
    }
}