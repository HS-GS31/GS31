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

    //음식을 잡았을때
    public void Selete()
    {
        //만약 선택된 스틱이 없다면...
        if(gameManager.GetComponent<GameManager>().getSelectedStick() == null)
        {
            return;     //무시.
        }
        else
        {
            //만약 선택한 음식이 올바른 음식이 아니라면..
            if (gameManager.GetComponent<GameManager>().checkIngred(this.gameObject))
            {
                //현재 선택된 스틱에 push.
                gameManager.GetComponent<GameManager>().getSelectedStick().GetComponent<MenuController>().push(this.gameObject);
                Invoke("SpawnObj", 0.7f); 
            }
            else
            {
                handOut(this.gameObject);
                return;     //아니면 무시
            }
        }
    }

    //음식을 놓았을때
    public void UnSelect()
    {
        //한손으로 잡고 놓은 상태
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