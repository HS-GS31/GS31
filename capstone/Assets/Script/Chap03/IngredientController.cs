using UnityEngine;
using Oculus.Interaction;

public class IngredientController : MonoBehaviour
{
    private bool isStickTop;                    //��ƽ�� ����⿡�� ���� �����ΰ� �Ǵ�. 
    private Vector3 spawnPos;                   //ó����ġ
    private Quaternion spawnRot;
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
        if (transform.parent.tag == "STICK")
            rigid.isKinematic = true;
        else
            rigid.isKinematic = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "STICK" && isStickTop)
        {
            this.gameObject.transform.parent = other.gameObject.transform;
        }
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
        if (this.gameObject.transform.parent.tag == "STICK")
        {
            float y = transform.localPosition.y;
            if (transform.localPosition.x >= 0.03f || transform.localPosition.x <= -0.03f || transform.localPosition.z >= 0.03f || transform.localPosition.z <= -0.03f)
            {
                this.gameObject.GetComponent<Grabbable>().enabled = false;
                this.gameObject.GetComponent<Grabbable>().enabled = true;
                transform.localPosition = new Vector3(0, y, 0);
                
                //����� ����.
                gameManager.GetComponent<GameManager>().Warn();
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
        if (transform.parent.tag == "STICK")
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