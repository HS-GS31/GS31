using UnityEngine;
using Oculus.Interaction;

public class IngredientController : MonoBehaviour
{
    private bool isStickTop;                    //��ƽ�� ����⿡�� ���� �����ΰ� �Ǵ�. 
    private Vector3 spawnPos;                   //ó����ġ
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
        if (other.gameObject.tag == "StickTop")
        {
            this.gameObject.transform.parent = other.gameObject.transform.parent.transform;       //�ϴ� ��ġ�� �ڽ����� ����.
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
            //������� �������, �θ� ������Ʈ null
            if (other.transform.localPosition.y <= transform.localPosition.y)
            {
                isStickTop = false;
                transform.parent = null;
                Debug.Log("������!");
            }
            //�ϴ����� �������, �θ� ������Ʈ �״��..
        }

        //��ġ���� ������ ���.
        if (other.gameObject.tag == "STICK")
        {
            //����� ���� ������ �ƴѰ��.
            if (transform.localPosition.y < stickTop)
            {
                this.gameObject.GetComponent<Grabbable>().enabled = false;
                this.gameObject.GetComponent<Grabbable>().enabled = true;
            
                transform.localPosition = new Vector3(0, transform.localPosition.y, 0);

                //����� ����.
                gameManager.GetComponent<GameManager>().Warn();
            }
            else  //����� ���� ��ġ�� ���� ���.
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