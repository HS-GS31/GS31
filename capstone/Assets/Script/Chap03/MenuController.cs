using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    GameObject customerManager;
    private string[] ingredients;
    int top;
    private void Start()
    {
        customerManager = GameObject.Find("CustomerManager");
        ingredients = new string[4];
        top = -1;
    }

    public void push(GameObject ingredient)
    {
        top++;
        ingredients[top] = ingredient.tag;
        Debug.Log("top :" + ingredients[top]);

        if (top == 3)
        {
            Debug.Log("²¿Ä¡µé:");
            foreach(string obj in ingredients)
            {
                Debug.Log(obj);
            }
        }
    }

    public void pop()
    {
        ingredients[top] = null;
        top--;
    }

    public string[] getMenu()
    {
        return ingredients;
    }
}