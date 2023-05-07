using UnityEngine;
using Oculus.Interaction;

public class IngredientController : MonoBehaviour
{
    private bool isStickTop;                    //스틱의 꼭대기에서 들어온 음식인가 판단. 
    private Vector3 spawnPos;                   //처음위치
    private Quaternion spawnRot;
    private GameObject gameManager;
    private Vector3 center;
    private float con_x;
    private float con_z;
    Rigidbody rigid;
    Collider coll;

    void Start()
    {
        spawnPos = transform.position;
        spawnRot = transform.rotation;
        rigid = gameObject.GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager");
        coll = gameObject.GetComponent<Collider>();
        isStickTop = false;
        setCenter();
        setConst();
    }

    private void Update()
    {
        warn();
        //음식이 바닥으로 떨어졌을때.
        if(transform.position.y < 1.1f)
        {
            ResPawnIngredient();
        }

        //음식이 꼬치에 꽂혀있으면..
        if(transform.parent != null)
        {
            if (transform.parent.gameObject.tag == "STICK")
                rigid.isKinematic = true;
            else
                rigid.isKinematic = false;
        }
        else
            rigid.isKinematic = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "STICK")
        {
            this.gameObject.transform.parent = other.gameObject.transform;
            Debug.Log(gameObject.tag + "  const : " + con_x + ", " + con_z);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "STICK")
        {
            this.gameObject.transform.parent = other.gameObject.transform;
            this.rigid.useGravity = false;
            this.coll.isTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //꼬치에서 빠졌을 경우.
        if (other.gameObject.tag == "STICK")
        {
            this.gameObject.transform.parent = null;
            isStickTop = false;
        }
    }
    public void warn()
    {
        //부모객체가 있는 상태에서 꼬치에 특정범위에서 꼬치가 빠지면.
        if (transform.parent != null && transform.parent.gameObject.tag == "STICK")
        {
            float y = transform.localPosition.y;
            if (transform.localPosition.x >= center.x + con_x || transform.localPosition.x <= center.x - con_x || transform.localPosition.z >= center.z + con_z || transform.localPosition.z <= center.z - con_z)
            {
                this.gameObject.GetComponent<Grabbable>().enabled = false;
                this.gameObject.GetComponent<Grabbable>().enabled = true;
                transform.localPosition = new Vector3(center.x, y, center.z);
                
                //경고문구 띄우기.
                gameManager.GetComponent<GameManager>().Warn();
            }
        }
    }

    private void setCenter()
    {
        if(gameObject.tag == "Mushroom")
            this.center = new Vector3(-0.036f, 0, -0.02f);
        else if (gameObject.tag == "Meat")
            this.center = new Vector3(-0.033f, 0, 0.015f);     
        else
            this.center = new Vector3(0, 0, 0);
    }
    private void setConst()
    {
        if (gameObject.tag == "Sausage")
        {
            con_x = 0.05f;
            con_z = 0.02f;
        }
        else if (gameObject.tag == "Vegetable")
        {
            con_x = 0.03f;
            con_z = 0.018f;
        }
        else
        {
            con_x = 0.02f;
            con_z = 0.02f;
        }
    }
    private void ResPawnIngredient()
    {
        transform.position = spawnPos;
        transform.rotation = spawnRot;
        rigid.isKinematic = true;
        rigid.isKinematic = false;
    }

    //음식을 잡았을때
    public void Selete()
    {
        transform.parent = null;
        rigid.useGravity = false;
        coll.isTrigger = true;
    }

    //음식을 놓았을때
    public void UnSelect()
    {
        //음식을 놓았는데, 꼬치에 끼워진 상태라면.
        if (transform.parent != null)
        {
            if(transform.parent.gameObject.tag == "STICK")
            {
                rigid.useGravity = false;
                coll.isTrigger = true;
            }
        }
        else
        {
            rigid.useGravity = true;
            coll.isTrigger = false;
        }
    }
    public void setIsSticTop()
    {
        isStickTop = true;
    }
}