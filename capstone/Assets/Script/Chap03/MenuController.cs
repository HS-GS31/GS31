using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    GameObject customerManager;
    GameObject[] ingredients;
    int top;
    private void Start()
    {
        customerManager = GameObject.Find("CustomerManager");
        top = -1;
    }

    public void push(GameObject ingredient)
    {
        top++;
        ingredients[top] = ingredient;
        Debug.Log(ingredient + "�� ���Դ�.");
        Debug.Log("���� ��ġ ���� " + ingredients);
    }

    public void pop(GameObject ingredient)
    {
        Debug.Log(ingredient + "�� ������.");
        ingredients[top] = null;
        top--;
        Debug.Log("���� ��ġ ���� " + ingredients);
    }
}
