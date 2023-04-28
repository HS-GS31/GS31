using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    GameObject customerManager;
    Rigidbody rigid;
    Collider coll;
    private GameObject stickTop;
    private string[] ingredients;
    int top;
    private Vector3 spawnPos;                 
    private Quaternion spawnRot;

    private void Start()
    {
        spawnPos = transform.position;
        spawnRot = transform.rotation;
        rigid = gameObject.GetComponent<Rigidbody>();
        coll = gameObject.GetComponent<CapsuleCollider>();
        customerManager = GameObject.Find("CustomerManager");
        stickTop = gameObject.transform.GetChild(0).gameObject;           
        ingredients = new string[4];
        top = -1;
    }
    private void Update()
    {
        if (transform.position.y < 1.1f)
        {
            ResPawnIngredient();
        }

        if (transform.childCount >= 6)
        {
            Debug.Log("자식의 수 : " + transform.childCount);
            setIngredient();
        }
        else
        {
            setIngredientNull();
        }
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
    public void UnSelect()
    {
        rigid.useGravity = true;
        coll.isTrigger = false;
    }
    private void setIngredient()
    {
        ingredients[0] = transform.GetChild(2).gameObject.tag;
        ingredients[1] = transform.GetChild(3).gameObject.tag;
        ingredients[2] = transform.GetChild(4).gameObject.tag;
        ingredients[3] = transform.GetChild(5).gameObject.tag;
        
        for(int i = 0; i < 4; i++)
        {
            Debug.Log(i +" : "+ingredients[i]);
        }
    }
    private void setIngredientNull()
    {
        ingredients[0] = "empty";
        ingredients[1] = "empty";
        ingredients[2] = "empty";
        ingredients[3] = "empty";
    }

    private void ResPawnIngredient()
    {
        transform.position = spawnPos;
        transform.rotation = spawnRot;
        rigid.isKinematic = true;
        rigid.isKinematic = false;
    }
}