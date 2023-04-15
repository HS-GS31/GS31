using UnityEngine;
using Oculus.Interaction;

public class IngredientController : MonoBehaviour
{
    private bool isStickTop = false;        //��ƽ�� ����⿡�� ���� �����ΰ� �Ǵ�. 
    private float constrainX = 0.3f;
    private float constrainZ = 0.3f;
    private Vector3 spawnPos;                   //ó����ġ
    private Quaternion spawnRot;
    Rigidbody rigid;
    Collider coll;

    void Start()
    {
        spawnPos = transform.position;
        spawnRot = transform.rotation;
        rigid = gameObject.GetComponent<Rigidbody>();

        if (this.gameObject.tag == "Shrimp" || this.gameObject.tag == "Meat")
            coll = gameObject.GetComponent<BoxCollider>();
        else
            coll = gameObject.GetComponent<MeshCollider>();
    }

    private void Update()
    {
        warn();
        //������ �ٴ����� ����������.
        if(transform.position.y < 1.1f)
        {
            //ó�� ���� �״�� �̵�.
            transform.position = spawnPos;                  
            transform.rotation = spawnRot;                  
            rigid.isKinematic = true;
            rigid.isKinematic = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "StickTop")
        {
            this.coll.isTrigger = true;
            this.rigid.useGravity = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "StickTop")
            isStickTop = true;

        if(other.gameObject.tag == "STICK" && isStickTop)
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
        if (other.gameObject.tag == "STICK" || isStickTop)
        {
            other.gameObject.GetComponent<MenuController>().pop();
            this.gameObject.transform.parent = null;
            isStickTop = false;
        }
    }

    public void warn()
    {
        //�θ�ü�� �ִ� ���¿��� ��ġ�� Ư���������� ��ġ�� ������.
        if (this.gameObject.transform.parent != null)
        {
            float y = transform.localPosition.y;
            if (transform.localPosition.x >= 0.025f || transform.localPosition.x <= -0.025f || transform.localPosition.z >= 0.025f || transform.localPosition.z <= -0.025f)
            {
                this.gameObject.GetComponent<Grabbable>().enabled = false;
                this.gameObject.GetComponent<Grabbable>().enabled = true;
                transform.localPosition = new Vector3(0, y, 0);
            }
        }
    }

    //������ �������
    public void Selete()
    {
        rigid.useGravity = false;
        coll.isTrigger = true;
    }

    //������ ��������
    public void UnSelect()
    {
        //������ ���Ҵµ�, ��ġ�� ������ ���¶��.
        if (transform.parent == null)
        {
            rigid.useGravity = true;
            coll.isTrigger = false;
        }
    }
}