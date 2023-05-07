using UnityEngine;
using Oculus.Interaction;

public class IngredientController : MonoBehaviour
{
    private bool isStickTop;                    //��ƽ�� ����⿡�� ���� �����ΰ� �Ǵ�. 
    private Vector3 spawnPos;                   //ó����ġ
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
        //������ �ٴ����� ����������.
        if(transform.position.y < 1.1f)
        {
            ResPawnIngredient();
        }

        //������ ��ġ�� ����������..
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
        //��ġ���� ������ ���.
        if (other.gameObject.tag == "STICK")
        {
            this.gameObject.transform.parent = null;
            isStickTop = false;
        }
    }
    public void warn()
    {
        //�θ�ü�� �ִ� ���¿��� ��ġ�� Ư���������� ��ġ�� ������.
        if (transform.parent != null && transform.parent.gameObject.tag == "STICK")
        {
            float y = transform.localPosition.y;
            if (transform.localPosition.x >= center.x + con_x || transform.localPosition.x <= center.x - con_x || transform.localPosition.z >= center.z + con_z || transform.localPosition.z <= center.z - con_z)
            {
                this.gameObject.GetComponent<Grabbable>().enabled = false;
                this.gameObject.GetComponent<Grabbable>().enabled = true;
                transform.localPosition = new Vector3(center.x, y, center.z);
                
                //����� ����.
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

    //������ �������
    public void Selete()
    {
        transform.parent = null;
        rigid.useGravity = false;
        coll.isTrigger = true;
    }

    //������ ��������
    public void UnSelect()
    {
        //������ ���Ҵµ�, ��ġ�� ������ ���¶��.
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