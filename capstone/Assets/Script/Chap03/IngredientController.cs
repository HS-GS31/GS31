using UnityEngine;
using Oculus.Interaction;

public class IngredientController : MonoBehaviour
{
    private Vector3 spawnPos;                  
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
    }

    private void Update()
    {
        if (this.gameObject.transform.parent.tag == "STICK")
        {
            coll.isTrigger = true;
            rigid.isKinematic = true;
        }
        else
        {
            coll.isTrigger = false;
            rigid.isKinematic = false;
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
        //���� ���õ� ��ƽ�� ���ٸ�...
        if(gameManager.GetComponent<GameManager>().getSelectedStick() == null)
        {
            return;     //����.
        }
        else
        {
            //���� ������ ������ �ùٸ� ������ �ƴ϶��..
            if (gameManager.GetComponent<GameManager>().checkIngred(this.gameObject))
            {
                //���� ���õ� ��ƽ�� push.
                gameManager.GetComponent<GameManager>().getSelectedStick().GetComponent<MenuController>().push(this.gameObject);
                Invoke("SpawnObj", 0.7f); 
            }
            else
            {
                handOut(this.gameObject);
                return;     //�ƴϸ� ����
            }
        }
    }

    //������ ��������
    public void UnSelect()
    {
        //�Ѽ����� ��� ���� ����
        rigid.useGravity = true;
        coll.isTrigger = false;
    }
    private void handOut(GameObject obj)
    {
        obj.GetComponent<Grabbable>().enabled = false;
        obj.GetComponent<Grabbable>().enabled = true;
    }
    private void SpawnObj()
    {
        GameObject instance = Instantiate(this.gameObject);
        instance.transform.position = spawnPos;
        instance.transform.rotation = spawnRot;
    }
}