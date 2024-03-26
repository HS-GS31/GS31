using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class MenuController : MonoBehaviour
{
    GameObject customerManager;
    GameObject gameManager;
    Rigidbody rigid;
    Collider coll;

    private string[] ingredients;
    private int hand_count;
    private Vector3 spawnPos;                 
    private Quaternion spawnRot;
    private bool isSelected;            //isSelected가 true시 카메라 앞으로 이동.
    public GameObject StickPos;
    private Vector3[] ingredientPos;

    private GameObject stickTop;
    private int top;
    
    private void Start()
    {
        spawnPos = transform.position;
        spawnRot = transform.rotation;
        rigid = gameObject.GetComponent<Rigidbody>();
        coll = gameObject.GetComponent<CapsuleCollider>();
        customerManager = GameObject.Find("CustomerManager");
        gameManager = GameObject.Find("GameManager");

        stickTop = gameObject.transform.GetChild(0).gameObject;
        
        top = -1;
        ingredients = new string[4];
        
        ingredientPos = new Vector3[4];
        isSelected = false;
        setPos();
        hand_count = 0;

    }
    private void Update()
    {
        if (transform.position.y < 1.1f)
        {
            ResPawnIngredient();
        }
        if (transform.childCount >= 6)
        {
            setIngredient();
        }
        else
        {
            setIngredientNull();
        }

        //만약 이 스틱이 선택된 스틱인 경우.
        if(gameManager.GetComponent<GameManager>().getSelectedStick() == this.gameObject)
        {
            Debug.Log("현재 선택된 스틱입니다.");
            this.gameObject.transform.position = StickPos.transform.position;
            this.gameObject.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            rigid.useGravity = false;
            coll.isTrigger = true;
        }
    }
    public string[] getMenu()
    {
        return ingredients;
    }
    public void Select()
    {

        if (gameManager.GetComponent<GameManager>().getSelectedStick() == null)
        {   
            //선택된 스틱이 없는 상태에서 스틱을 잡으면 이 스틱을 gameManager의 선택된 스틱으로 세팅
            handOut(this.gameObject);
            gameManager.GetComponent<GameManager>().setSelectedStick(this.gameObject);
        }       
        else if (gameManager.GetComponent<GameManager>().getSelectedStick() == this.gameObject)
        {
            //현재 오브젝트가 gameManager에서 인지하고 있는 오브젝트라면 다시 선택 되었을때, 손으로 집을 수 있도록...
            Debug.Log("현재 선택된 스틱입니다.");
            rigid.useGravity = false;
            coll.isTrigger = true;
            hand_count++;
        }
        else
        {       //선택된 스틱이 없는 경우.
            rigid.useGravity = false;
            coll.isTrigger = true;

            //양손으로 잡았을 때 오류 발생 방지용 코드.
            hand_count++;
        }
    }
    public void UnSelect()
    {
        //한손으로 잡고 놓은 상태
        if (hand_count < 2)
        {
            rigid.useGravity = true;
            coll.isTrigger = false;
            hand_count--;
        }
        else    //두손으로 잡은 상태.
        {
            hand_count--;
        }
    }
    private void setIngredient()
    {
        ingredients[0] = transform.GetChild(2).gameObject.tag;
        ingredients[1] = transform.GetChild(3).gameObject.tag;
        ingredients[2] = transform.GetChild(4).gameObject.tag;
        ingredients[3] = transform.GetChild(5).gameObject.tag;
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
    private void setPos()
    {
        ingredientPos[0] = new Vector3(0.0f, -0.072f, 0.0f);
        ingredientPos[1] = new Vector3(0.0f, -0.009f, 0.0f);
        ingredientPos[2] = new Vector3(0.0f, 0.057f, 0.0f);
        ingredientPos[3] = new Vector3(0.0f, 0.122f, 0.0f);
    }
    
    public void push(GameObject ingredient)
    {
        //오브젝트를 자식으로 넣기.
        top++;
        ingredient.transform.SetParent(this.transform);
        if(ingredient.tag == "Mushroom")
        {
            float y = 0.0002f;
            ingredient.transform.localPosition = new Vector3(0.018f, ingredientPos[top].y - y, -0.022f);
            ingredient.transform.eulerAngles = new Vector3(75.0f, 0.0f, 37.5f);
        }
        else
        {
            ingredient.transform.localPosition = ingredientPos[top];
            ingredient.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        }
        ingredients[top] = ingredient.tag;
        handOut(ingredient);
    }

    //중복으로 잡히는 오류를 방지하는 함수.
    private void handOut(GameObject obj)
    {
        obj.GetComponent<Grabbable>().enabled = false;
        obj.GetComponent<Grabbable>().enabled = true;
    }
}