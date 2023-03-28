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
        Debug.Log(ingredient + "가 들어왔다.");
        Debug.Log("현재 꼬치 상태 " + ingredients);
    }

    public void pop(GameObject ingredient)
    {
        Debug.Log(ingredient + "가 빠졌다.");
        ingredients[top] = null;
        top--;
        Debug.Log("현재 꼬치 상태 " + ingredients);
    }
}
