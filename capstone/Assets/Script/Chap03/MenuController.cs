using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    GameObject customerManager;
    Rigidbody rigid;
    Collider coll;
    private GameObject stickTop;
    //현재 꼬치 상태.
    private string[] ingredients;
    //꼬치 스택 인덱스
    int top;

    private void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
        coll = gameObject.GetComponent<CapsuleCollider>();
        customerManager = GameObject.Find("CustomerManager");
        stickTop = gameObject.transform.GetChild(0).gameObject;             //자식의 0번째는 sticktop
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
            Debug.Log("꼬치들:");
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

    public void Selete()
    {
        rigid.useGravity = false;
        coll.isTrigger = true;
    }
    //음식을 놓았을때
    public void UnSelect()
    {
        rigid.useGravity = true;
        coll.isTrigger = false;
    }
}