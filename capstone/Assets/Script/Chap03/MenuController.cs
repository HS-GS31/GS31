using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    GameObject customerManager;
    Rigidbody rigid;
    Collider coll;
    private GameObject stickTop;
    //���� ��ġ ����.
    private string[] ingredients;
    //��ġ ���� �ε���
    int top;

    private void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
        coll = gameObject.GetComponent<CapsuleCollider>();
        customerManager = GameObject.Find("CustomerManager");
        stickTop = gameObject.transform.GetChild(0).gameObject;             //�ڽ��� 0��°�� sticktop
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
            Debug.Log("��ġ��:");
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
    //������ ��������
    public void UnSelect()
    {
        rigid.useGravity = true;
        coll.isTrigger = false;
    }
}