using UnityEngine;
using Oculus.Interaction;

public class IngredientController : MonoBehaviour
{
    private bool isStickTop = false;        //스틱의 꼭대기에서 들어온 음식인가 판단. 
    private float constrainX = 0.3f;
    private float constrainZ = 0.3f;
    private Vector3 spawnPos;                   //처음위치
    private Quaternion spawnRot;
    private GameObject warnText;
    Rigidbody rigid;
    Collider coll;

    void Start()
    {
        spawnPos = transform.position;
        spawnRot = transform.rotation;
        rigid = gameObject.GetComponent<Rigidbody>();
        warnText = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        if (this.gameObject.tag == "Shrimp" || this.gameObject.tag == "Meat")
            coll = gameObject.GetComponent<BoxCollider>();
        else
            coll = gameObject.GetComponent<MeshCollider>();
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
        if (transform.parent != null)
            rigid.isKinematic = true;
        else
            rigid.isKinematic = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "STICK")
        {
            this.gameObject.transform.parent = other.gameObject.transform;
            other.gameObject.GetComponent<MenuController>().push(this.gameObject);
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
        if (other.gameObject.tag == "STICK")
        {
            other.gameObject.GetComponent<MenuController>().pop();
            this.gameObject.transform.parent = null;
            isStickTop = false;
        }
    }
    public void warn()
    {
        //부모객체가 있는 상태에서 꼬치에 특정범위에서 꼬치가 빠지면.
        if (this.gameObject.transform.parent != null)
        {
            float y = transform.localPosition.y;
            if (transform.localPosition.x >= 0.025f || transform.localPosition.x <= -0.025f || transform.localPosition.z >= 0.025f || transform.localPosition.z <= -0.025f)
            {
                this.gameObject.GetComponent<Grabbable>().enabled = false;
                this.gameObject.GetComponent<Grabbable>().enabled = true;
                transform.localPosition = new Vector3(0, y, 0);

                //경고문구 띄우기.
                warnText.GetComponent<WarnText>().setActive();
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
        rigid.useGravity = false;
        coll.isTrigger = true;
    }

    //음식을 놓았을때
    public void UnSelect()
    {
        //음식을 놓았는데, 꼬치에 끼워진 상태라면.
        if (transform.parent == null)
        {
            rigid.useGravity = true;
            coll.isTrigger = false;
        }
    }
}