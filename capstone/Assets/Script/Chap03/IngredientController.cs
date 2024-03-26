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

    //음식을 잡았을때
    public void Select()
    {
        //만약 선택된 스틱이 없다면...
        if(gameManager.GetComponent<GameManager>().getSelectedStick() == null)
        {
            return;
        }
        else
        {
            //꼬치에 꽂혀있는 음식이 아닐때.
            if(this.transform.parent.gameObject.tag != "STICK")
            {
                //만약 선택한 음식이 올바른 음식라면
                if (gameManager.GetComponent<GameManager>().checkIngred(this.gameObject))
                {
                    //현재 선택된 스틱에 넣기 push.
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
        instance.transform.parent = this.parent;
    }
}