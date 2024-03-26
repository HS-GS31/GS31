using UnityEngine;
using Oculus.Interaction;

public class IngredientController : MonoBehaviour
{
    private Vector3 spawnPos;                  
    private Quaternion spawnRot;
    private GameObject gameManager;
    private Transform parent;
    public GameObject prefab;
    Rigidbody rigid;
    Collider coll;
    
    void Start()
    {
        spawnPos = transform.position;
        spawnRot = transform.rotation;
        rigid = gameObject.GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager");
        coll = gameObject.GetComponent<Collider>();
        parent = transform.parent;
    }

    private void Update()
    {
        if (this.gameObject.transform.parent != null && this.gameObject.transform.parent.tag == "STICK")
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

    //������ �������
    public void Select()
    {
        //���� ���õ� ��ƽ�� ���ٸ�...
        if(gameManager.GetComponent<GameManager>().getSelectedStick() == null)
        {
            return;
        }
        else
        {
            //��ġ�� �����ִ� ������ �ƴҶ�.
            if(this.transform.parent.gameObject.tag != "STICK")
            {
                //���� ������ ������ �ùٸ� ���Ķ��
                if (gameManager.GetComponent<GameManager>().checkIngred(this.gameObject))
                {
                    //���� ���õ� ��ƽ�� �ֱ� push.
                    gameManager.GetComponent<GameManager>().getSelectedStick().GetComponent<MenuController>().push(this.gameObject);
                    Invoke("SpawnObj", 0.7f);
                }
                else
                {
                    handOut(this.gameObject);
                    return;
                }
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
        instance.transform.parent = this.parent;
    }
}