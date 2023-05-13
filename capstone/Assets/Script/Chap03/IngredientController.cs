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

    }

    //������ ��������
    public void UnSelect()
    {

    }
}