using UnityEngine;
using Oculus.Interaction;

public class IngredientController : MonoBehaviour
{
    private bool isStickTop;                    //스틱의 꼭대기에서 들어온 음식인가 판단. 
    private Vector3 spawnPos;                   //처음위치
    private Quaternion spawnRot;
    private float stickTop;
    private GameObject gameManager;
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
        stickTop = 0.15f;
    }

    private void Update()
    {
        //warn();
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
        if (other.gameObject.tag == "StickTop")
        {
            this.gameObject.transform.parent = other.gameObject.transform.parent.transform;       //일단 꼬치의 자식으로 들어간다.
            isStickTop = true;
        }
        /*
        if (other.gameObject.tag == "STICK" && isStickTop)
        {
            this.gameObject.transform.parent = other.gameObject.transform;
        }
        */
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "STICK" && isStickTop)
        {
            this.gameObject.transform.parent = other.gameObject.transform;
            this.rigid.useGravity = false;
            this.coll.isTrigger = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "StickTop")
        {
            //상단으로 빠진경우, 부모 오브젝트 null
            if (other.transform.localPosition.y <= transform.localPosition.y)
            {
                isStickTop = false;
                transform.parent = null;
                Debug.Log("빠졌다!");
            }
            //하단으로 빠진경우, 부모 오브젝트 그대로..
        }

        //꼬치에서 빠졌을 경우.
        if (other.gameObject.tag == "STICK")
        {
            //상단을 통해 빠진게 아닌경우.
            if (transform.localPosition.y < stickTop)
            {
                this.gameObject.GetComponent<Grabbable>().enabled = false;
                this.gameObject.GetComponent<Grabbable>().enabled = true;
            
                transform.localPosition = new Vector3(0, transform.localPosition.y, 0);

                //경고문구 띄우기.
                gameManager.GetComponent<GameManager>().Warn();
            }
            else  //상단을 통해 꼬치가 빠진 경우.
            {
                this.gameObject.transform.parent = null;
                isStickTop = false;
            }
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