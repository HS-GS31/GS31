using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabFood : MonoBehaviour
{
    private Rigidbody _rigid;
    private bool checkCollision = false;
    public GameObject boardcheck;
    public GameObject foodcheck;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        checkCollision = false; 
    }

    // Update is called once per frame
    void Update()
    {
        // ������ ���� īƮ���
        if (checkCollision == true)
        {
            Debug.Log("checkCollision");
        }

        /*
        // ������ ���� īƮ�� �ƴϸ� �߷� �����ϰ� ����ġ�� (Ư����ġ���� �������ٰ�)
        else
        {
            _rigid.useGravity = false;
        }
        */
    }


    public void UnGrabVegetable()
    {
        // ó�� ��Ҵٰ� ������ �߷� ó���� ��������
        _rigid.useGravity = true;

        if(checkCollision == true)         
        {
            Debug.Log("īƮ�� ��2");
            boardcheck.SetActive(true);
            checkCollision = false;
        }
            
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("CheckCollision"))
        {
            Debug.Log("111111111111111111");
            // īƮ�� �������Ÿ� checkCollision = true
            checkCollision = true;
        }
    }

}
